using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Orb : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private MeshRenderer orb;
    public enum typesOrbs { HP, MP, EnemyOrbs, SpPoints };
    [SerializeField] private typesOrbs orbType;
    [SerializeField] private Color color;

    [SerializeField] private float acceleration = 0f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private float minRangeMove = 2f;
    [SerializeField] private float maxRangeMove = 4f;
    [SerializeField] private float destroyDistance = 0f;

    private Vector3 movement;
    private float speed;
    private int orbValue;

    #region Properties 

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    public int OrbValue
    {
        get { return orbValue; }
        set { orbValue = value; }
    }

    public Color Color
    {
        get { return color; }
    }

    public typesOrbs GetTypesOrbs
    {
        get { return orbType; }
    }

    #endregion
    void Start()
    {
        movement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        movement = movement.normalized * Random.Range(minRangeMove, maxRangeMove);
        SetColor(color);
    }

    void Update()
    {
        if(orbType != typesOrbs.SpPoints)
        {
            
            orbMovement();
        }
        
    }
    private void orbMovement()
    {
        if (target)
        {
            speed += acceleration * Time.deltaTime;
            speed = Mathf.Min(speed, maxSpeed);

            turnSpeed += turnSpeed * Time.deltaTime;

            Vector3 delta = target.transform.position - transform.position;
            Vector3 direction = delta.normalized;

            movement = Vector3.Lerp(movement, direction * speed, turnSpeed * Time.deltaTime);

            if (delta.magnitude <= destroyDistance)
            {
                target = null;
                movement = delta * 2f;
                orb.transform.DOScale(Vector3.zero, .5f);
                if (!trail)
                {
                    trail.DOTime(.0f, .5f).onComplete = 
                        delegate ()
                        {
                            Destroy(gameObject);
                        };
                }
            }
            transform.position += movement * Time.deltaTime;
        }
    }

    public void SetColor(Color pColor)
    {
        trail.startColor = pColor;
        trail.endColor = pColor;

        orb.material.color = pColor;
    }
    private void OnTriggerStay(Collider other)
    {
        if(orbType == typesOrbs.SpPoints)
        {
            orbMovement();
        }
    }
 }
