using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsSpawn : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject orbPrefab;
    [SerializeField] private int orbValue;
    
    private void SetOrb()
    {
        GameObject orbs = Instantiate(orbPrefab);
        
        Orb orb = orbs.GetComponent<Orb>();

        orb.Target = target.transform;
        orb.transform.position = transform.position;
        orb.SetColor(orb.Color);
        orb.OrbValue = orbValue;
    }

    public void SpawnOrbs( int numOrbs)
    {
        for (int i = 0; i < numOrbs; i++)
        {
            SetOrb();
        }
    }

    public int pointsToOrbs(int points)
    {
        int orbsToSpawn = points / orbValue;
        return orbsToSpawn;
    }
}
