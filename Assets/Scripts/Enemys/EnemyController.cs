using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Creature
{
    #region Fields

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private GameObject player;
    [SerializeField] private CharClass enemyClass;
    [SerializeField] private Weapon weapon;
    [SerializeField] protected EnemyData myEnemyData;
    private bool onRange = false;
    private int waypointIndex = 0;
    private bool goBack = false;
    private bool canFollow = false;

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        movementCDTimer = gameObject.AddComponent<Timer>();
    }

    protected override void Start()
    {
        base.Start();
        movementCDTimer.Duration = myEnemyData.MovementCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFollow)
        {
            MoveEnemy();
            AnimationController.SetBool("isWalking", true);
        }
        this.Aggro();
    }

    #endregion

    #region PrivateMethods

    private void MoveEnemy()
    {
        Vector3 direction = waypoints[waypointIndex].position - transform.position;

        if (!movementCDTimer.Running)
        {
            movementCDTimer.Run();
            if (!onRange)
            {
                transform.position += transform.forward * myEnemyData.Speed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward, direction.normalized, rotationSpeed * Time.deltaTime);
            }
        }
        if (direction.magnitude < minDistance)
        {
            movementCDTimer.Duration = 1f;

            if (waypointIndex >= waypoints.Length - 1)
            {
                goBack = true;
            }
            else if (waypointIndex <= 0)
            {
                goBack = false;
            }
            if (!goBack)
            {
                waypointIndex++;
            }
            else
            {
                waypointIndex--;
            }
        }
    }

    private void Aggro()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            Vector3 vectorDir = player.transform.position - transform.position;

            if (distance < myEnemyData.FollowRange)
            {
                if (!onRange)
                {
                    this.canFollow = true;
                    Quaternion newRotation = Quaternion.LookRotation(vectorDir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, myEnemyData.Speed * Time.deltaTime);
                    transform.position += vectorDir.normalized * Time.deltaTime * myEnemyData.Speed;
                }
                if (distance <= myEnemyData.AttackRange)
                {
                   transform.LookAt(player.transform.position);
                    AnimationController.SetBool("isWalking", false);
                    this.onRange = true;
                    this.canFollow = false;
                    Atack();
                }
                if (distance > myEnemyData.AttackRange)
                {
                    AnimationController.SetBool("isWalking", true);
                    this.onRange = false;
                    this.canFollow = true;
                }
            }
            else
            {
                canFollow = false;
            }
        }
    }

    private void MeleeAtack()
    {
        if (!this.attackCDTimer.Running)
        {
            this.attackCDTimer.Run();
            this.weapon.MakeMeleeDamage();
            AnimationController.SetBool("isAttacking", true);
        }
        else if (this.attackCDTimer.SecondsLeft < 1f)
        {
            this.attackCDTimer.Duration = this.atkCooldown;
            AnimationController.SetBool("isArcher", false);
        }
    }

    private void LongRangeAtack()
    {
        
        if (!this.attackCDTimer.Running)
        {
            this.attackCDTimer.Run();
            weapon.MakeLongDamage(myEnemyData.AttackRange);
            AnimationController.SetBool("isArcher", true);
        }
        else if (this.attackCDTimer.SecondsLeft <= 1f)
        {
            this.attackCDTimer.Duration = atkCooldown;
            AnimationController.SetBool("isArcher", false);
        }
    }

    #endregion

    #region ProtectedMethods

    protected override void Atack()
    {
        switch (enemyClass)
        {
            case CharClass.Warrior:
                MeleeAtack();
                break;
            case CharClass.Archer:
                LongRangeAtack();
                break;
            default:
                break;
        }
    }

    #endregion
}

