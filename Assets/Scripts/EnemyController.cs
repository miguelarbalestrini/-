using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Creature
{
    #region Fields

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private GameObject player;
    [SerializeField] private float atackRange = 6f;
    [SerializeField] private float followRange = 10f;
    [SerializeField] private CharClass enemyClass;
    [SerializeField] private Weapon weapon;
    private bool onRange = false;
    private int waypointIndex = 0;
    private bool goBack = false;
    private bool canFollow = false;

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        this.RemainingCD = this.atkCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFollow)
        {
          MoveEnemy(); 
        }
        this.AtackCooldown();
        Aggro();
    }

    #endregion

    #region PrivateMethods

    private void MoveEnemy()
    {
        Vector3 direction = waypoints[waypointIndex].position - transform.position;

        if (!this.atkInCooldown)
        {
            if (!onRange)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward, direction.normalized, rotationSpeed * Time.deltaTime);
            }

        }

        if (direction.magnitude < minDistance)
        {
            this.AtkInCooldown = true;
            this.RemainingCD = this.atkCooldown;

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

            if (distance < followRange)
            {
                if (!onRange)
                {
                    this.canFollow = true;
                    Quaternion newRotation = Quaternion.LookRotation(vectorDir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speed * Time.deltaTime);
                    transform.position += vectorDir.normalized * Time.deltaTime * speed;
                }
                if (distance <= atackRange)
                {
                   transform.LookAt(player.transform.position);
                    this.onRange = true;
                    this.canFollow = false;
                    Atack();
                }
                if (distance > atackRange)
                {
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
        
        if (!this.AtkInCooldown)
        {
            Debug.Log($"Warrior atack");
            this.weapon.gameObject.GetComponent<Collider>().isTrigger = true;
            this.AtkInCooldown = true;
            //AnimationController.SetBool("isAttacking", true);
            this.RemainingCD = this.atkCooldown;
        }
        else if (RemainingCD < 1f)
        {
            //AnimationController.SetBool("isAttacking", false);
            this.weapon.gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }

    private void LongRangeAtack()
    {
        //Debug.Log($"Archer atack");
        this.weapon.Range = this.atackRange;

    }

    #endregion

    #region ProtectedMethods

    protected override void Atack()
    {
        base.Atack();
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

