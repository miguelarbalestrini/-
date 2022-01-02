using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActiveOrbs : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float damage;
    [SerializeField] private GameObject orbs;
    [SerializeField] private int points = 10;
    //[SerializeField] private int orbValue = 2;
    private OrbsSpawn Orbs;
    private float hp;

    public int Points
    {
        get { return points; }
    }

    /*public int OrbValue
    {
        get { return orbValue; }
    }*/

    public float MaxHp
    {
        get { return maxHp; }
    }

    public float Hp
    {
        get { return hp; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Orbs = orbs.GetComponent<OrbsSpawn>();
        hp = maxHp;
    }

    // Update is called once per frame
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
