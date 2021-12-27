using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActiveOrbs : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float damage;
    [SerializeField] private GameObject orbs;
    [SerializeField] private int points = 10;
    [SerializeField] private int orbValue = 2;
    private OrbsSpawn Orbs;

    public int Points
    {
        get { return points; }
    }

    public int OrbValue
    {
        get { return orbValue; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Orbs = orbs.GetComponent<OrbsSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hp);
        if(hp > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hp -= damage;
            }
        }
        if(hp <= 0)
        {
            Orbs.SpawnOrbs();
            Destroy(gameObject);
        }
    }
}
