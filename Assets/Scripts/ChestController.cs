using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChestController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private OrbsSpawn spawn;
    [SerializeField] private int orbPoints;

    void Awake()
    {
        text.gameObject.SetActive(false);
    }

    private void Start()
    {
        EventManager.StartListening("OnOpenChest", OpenChest);
    }
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            text.gameObject.SetActive(false);
    }

    public void OpenChest(EventParam eventparam)
    {
        SetSpawner();
        Destroy(gameObject);
    }

    private void SetSpawner()
    {
        int numOrbs = spawn.pointsToOrbs(orbPoints);
        spawn.SpawnOrbs(numOrbs);
    }
}
