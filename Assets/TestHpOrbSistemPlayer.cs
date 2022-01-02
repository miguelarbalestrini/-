using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHpOrbSistemPlayer : MonoBehaviour
{

    private int hp;
    private int mp;
    
    [SerializeField] private int maxHP;
    [SerializeField] private int maxMP;
    [SerializeField] private int playerOrb = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        hp += maxHP;
        mp += maxMP;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Orb"))
        {
            GameObject orbPrefab = other.gameObject;
            Orb orb = orbPrefab.GetComponent<Orb>();

            switch (orb.GetTypesOrbs)
            {
                case Orb.typesOrbs.HP:
                    if (hp < maxHP)
                        hp += orb.OrbValue;
                    else
                        Debug.Log("HP: "+ hp);
                    break;
                case Orb.typesOrbs.MP:
                    if (hp < maxMP)
                        mp += orb.OrbValue;
                    else
                        Debug.Log("MP: " + mp);
                    break;
                case Orb.typesOrbs.EnemyOrbs:
                    playerOrb += orb.OrbValue;
                    Debug.Log(orb.OrbValue);
                    Debug.Log("Orbs: " + playerOrb);
                    break;
                case Orb.typesOrbs.SpPoints:
                    break;
                default:
                    break;
            }
        }
    }

}
