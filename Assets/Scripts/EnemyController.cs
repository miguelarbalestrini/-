using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Creature
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float minDistance;
    [SerializeField] private GameObject player;
    [SerializeField] private float atackRange = 6f;
    [SerializeField] private float followRange = 10f;
    [SerializeField] private bool warrior = false;
    [SerializeField] private bool archer = true;
    [SerializeField] private CharClass EnemyClass;
    private bool onRange = false;
    private int waypointIndex = 0;
    private bool goBack = false;
    private bool canFollow = false;


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

    private void MoveEnemy()
    {
         Vector3 direction = waypoints[waypointIndex].position - transform.position;

        if (!this.atkInCooldown)
        {
            if (!onRange)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward, direction.normalized, rotationSpeed * Time.deltaTime);
                Debug.Log("ACA TOY");
            }
          
        }

        if ( direction.magnitude < minDistance)
        {
            this.AtkInCooldown = true;
            this.RemainingCD = this.atkCooldown;

            if (waypointIndex >= waypoints.Length - 1 )
            {
                
                goBack = true;
            }
            else if (waypointIndex <= 0 )
            {
                
                goBack = false;
            }
            if (!goBack)
            {
                waypointIndex++;
            }
            else {             
                waypointIndex--;
            }
        }
    }

    private void Aggro()
    {
       
        float distance = Vector3.Distance(player.transform.position, transform.position);
        Vector3 vectorDir = player.transform.position - transform.position;
        
        switch (EnemyClass)
        {
            case CharClass.Warrior:
                if (distance < followRange)
                {
                    this.canFollow = true;
                    Quaternion newRotation = Quaternion.LookRotation(vectorDir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speed * Time.deltaTime);
                    transform.position += vectorDir.normalized * Time.deltaTime * speed;
                }
                else
                {
                    canFollow = false;
                }
                break;
            case CharClass.Archer:
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
                        //transform.LookAt(-2 * transform.position - player.transform.position);
                        //Quaternion.LookRotation(player.transform.position, transform.position);
                        //transform.rotation = Quaternion.Euler(Vector3.up * 180f);
                        this.onRange = true;
                        this.canFollow = false;
                        Debug.Log("ATACKING");
                    }
                    if (distance > atackRange)
                    {
                        this.onRange = false;
                        this.canFollow = true;
                    }
                }
                else
                {
                    this.canFollow = false;
                }
                break;
        }
    }
}

