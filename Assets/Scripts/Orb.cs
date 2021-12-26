using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Orb : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private MeshRenderer orb;

    [SerializeField] private float acceleration = 0f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private float minRangeMove = 2f;
    [SerializeField] private float maxRangeMove = 4f;
    [SerializeField] private float destroyDistance = 0f;
    private Vector3 movement;
    private float speed;

    #region Properties 

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        movement = movement.normalized * Random.Range(minRangeMove, maxRangeMove);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            speed += acceleration * Time.deltaTime;
            speed = Mathf.Min(speed, maxSpeed);

            turnSpeed += turnSpeed * Time.deltaTime;

            Vector3 delta = target.transform.position - transform.position;
            Vector3 direction = delta.normalized;

            movement = Vector3.Lerp(movement, direction * speed, turnSpeed * Time.deltaTime);
            
            if(delta.magnitude < destroyDistance)
            {
                target = null;
                movement = delta * 2f;
                orb.transform.DOScale(Vector3.zero, .5f);
                trail.DOTime(.1f, .5f).onComplete =
                    delegate ()
                    {
                        Destroy(gameObject);
                    };
            }
        }

        transform.position += movement * Time.deltaTime;
    }
}
