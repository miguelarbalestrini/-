using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsSpawn : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject orbPrefab;
    [SerializeField] private TestActiveOrbs containerPrefab;
    
    void Start()
    {
        
    }

    private void SetOrb()
    {
        GameObject orbs = Instantiate(orbPrefab);
        
        Orb orb = orbs.GetComponent<Orb>();

        //transform.position = container.transform.position;

        orb.Target = target;
        orb.transform.position = transform.position;
        orb.SetColor(orb.Color);
        orb.OrbValue = containerPrefab.OrbValue;
    }

    public void SpawnOrbs()
    {
        int numOrbs = pointsToOrbs(containerPrefab.OrbValue);
        for (int i = 0; i < numOrbs; i++)
        {
            SetOrb();
        }
    }

    private int pointsToOrbs(int value)
    {
        int orbsToSpawn = containerPrefab.Points / value;
        return orbsToSpawn;
    }
        // Update is called once per frame
        void Update()
    {

    }
}
