using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsSpawn : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject orbPrefab;
    [SerializeField] private int numSpawnOrbs = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void SpawnOrbs()
    {
        GameObject orbs = Instantiate(orbPrefab);
        Orb orb = orbs.GetComponent<Orb>();

        orb.Target = target;
        orb.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < numSpawnOrbs; i++)
            {
                SpawnOrbs();
            }
        }
    }
}
