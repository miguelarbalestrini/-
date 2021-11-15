using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Creature
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float minDistance;
    //[SerializeField] private int waitTime = 0;
    private int waypointIndex = 0;
    private bool goBack = false;

    // Start is called before the first frame update
    void Start()
    {
        this.RemainingCD = this.atkCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        this.AtackCooldown();
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
}
