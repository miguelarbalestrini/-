using UnityEngine;
using System.Collections.Generic;

namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables       

        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;
        public KeyCode setTargetInput = KeyCode.X;
        public KeyCode rollInput = KeyCode.C;
        public KeyCode pauseInput = KeyCode.Escape;

        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
        [HideInInspector] public Camera cameraMain;

        private bool lockingEnemy = false;
        private EnemyController lockedEnemy;
        private bool strafing = false;
        private Vector3 move;

        #region Rolling
        private bool rolling = false;
        public AnimationCurve rollSpeedCurve;
        private float rollTimeSeconds = 1.15f;
        private float rollCurrentTime = 0;
        private Vector3 rollMove;

        [Range(0.1f, 1.5f)]
        public float rotationSpeed;

        #endregion

        #endregion

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();
            Cursor.visible = false;
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            MoveInput();
            CameraInput();
            LockEnemyInput();
            SprintInput();
            StrafeInput();
            JumpInput();
            OpenChestInput();
            RollInput();
            PauseInput();
        }

        public virtual void MoveInput()
        {
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = Input.GetAxis(verticallInput);
            if (cc.input.x != 0 || cc.input.z != 0)
            {
                AudioManager.PlayIfNotPlaying(AudioClipName.Walk);
            } 
            else
            {
                AudioManager.Pause(AudioClipName.Walk);
            }
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                    UIManager.s.MainCamera = cameraMain;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput))
                cc.Sprint(true);
            else if (Input.GetKeyUp(sprintInput))
                cc.Sprint(false);
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput) && JumpConditions())
                cc.Jump();
        }

        void LockEnemyInput()
        {
            if (Input.GetKeyDown(setTargetInput))
            {
                if (!lockingEnemy) // Lock
                {
                    lockedEnemy = FindClosestEnemy();
                    if (lockedEnemy)
                    {
                        lockingEnemy = true;
                        UIManager.s.LockEnemy(lockedEnemy);
                    }
                }
                else
                {  // Unlock 
                    lockingEnemy = false;
                    strafing = false;
                    lockedEnemy = null;
                    UIManager.s.UnlockEnemy();
                }
            }
        }

        private EnemyController FindClosestEnemy()
        {
            EnemyController[] enemies = FindObjectsOfType<EnemyController>();
            EnemyController result = null;
            if (enemies.Length > 0)
            {
                float distanceToPlayer = Vector3.Distance(enemies[0].transform.position, transform.position);
                float currentBest = distanceToPlayer;
                int bestIndex = 0;
                for (int i = 1; i < enemies.Length; ++i)
                {
                    distanceToPlayer = Vector3.Distance(enemies[i].transform.position, transform.position);
                    if (distanceToPlayer < currentBest)
                    {
                        bestIndex = i;
                        currentBest = distanceToPlayer;
                    }
                }
                result = enemies[bestIndex];
            }
            return result;
        }

        private  void RollInput()
        {
            if (!rolling)
            {
                rolling = true;

                rollMove = move;
                cc.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(cc.transform.forward, move, rotationSpeed, 0.0f));
                //cc.GetFullBodyAnimator().Play("Roll");
                Invoke("EnableActions", rollTimeSeconds + 0.15f);
            }
            else
            {
                rollCurrentTime += Time.deltaTime;
                float speed = rollSpeedCurve.Evaluate(rollCurrentTime / rollTimeSeconds) * 5.0f;

                cc.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(cc.transform.forward, move, rotationSpeed, 0.0f));
                transform.position += rollMove.normalized * speed * Time.deltaTime;
                tpCamera.transform.position += rollMove.normalized * speed * Time.deltaTime;
            }
        }
        protected virtual void OpenChestInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("anda");
                EventManager.RaiseEvent("OnOpenChest");
            }
        }


        private void EnableActions()
        {
            rolling = false;
            rollCurrentTime = 0;
        }

        private void PauseInput()
        {
            if (Input.GetKeyDown(pauseInput))
            {
                EventManager.RaiseEvent("onPause");
            }
        }
     
        public void InitAtkAnimationEvent()
        {
            EventManager.RaiseEvent("onAnimationAtkInitPlayer");
        }

        public void FinishAtkAnimationEvent()
        {
            EventManager.RaiseEvent("onAnimationAtkFinishedPlayer");
        }
        #endregion       
    }
}