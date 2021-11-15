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
    private int waypointIndex = 0;
    bool onRange;
    private bool goBack = false;
    private bool canFollow = false;

    // Start is called before the first frame update
    void Start()
    {
        onRange = false;
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
            transform.forward = Vector3.Lerp(transform.forward, direction.normalized, rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * speed * Time.deltaTime;
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
        bool warrior = true;
  
        if (warrior)
        {
            Vector3 vectorDir = player.transform.position - transform.position;

            if (Vector3.Distance(player.transform.position, transform.position) < 10f)
            {
                canFollow = true;
                Quaternion newRotation = Quaternion.LookRotation(vectorDir);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, speed * Time.deltaTime);
                transform.position += vectorDir.normalized * Time.deltaTime * speed;
            }
            else
            {
                canFollow = false;
            }
        }
    }
}

