using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActiveOrbs : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float damage;
    [SerializeField] private GameObject orbs;
    [SerializeField] private int points = 10;
    private OrbsSpawn Orbs;
    private float hp;

    public int Points
    {
        get { return points; }
    }

    public float MaxHp
    {
        get { return maxHp; }
    }

    public float Hp
    {
        get { return hp; }
    }

    void Start()
    {
        Orbs = orbs.GetComponent<OrbsSpawn>();
        hp = maxHp;
    }

    void Update()
    {
        if(hp > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(hp);
                hp -= damage;
            }
        }
        if(hp <= 0)
        {
            Orbs.SpawnOrbs(Orbs.pointsToOrbs(points));
            gameObject.SetActive(false);
        }
    }
}
