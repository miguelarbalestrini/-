using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChestController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private OrbsSpawn spawn;
    [SerializeField] private int orbPoints;
    private GameObject currentChest;
    private bool canBeOpened = false;

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
            currentChest = this.gameObject;
            canBeOpened = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
            canBeOpened = false;
        }
    }

    public void OpenChest(EventParam eventparam)
    {
        if (GameObject.ReferenceEquals(currentChest, this.gameObject) && canBeOpened)
        {
            SetSpawner();
            gameObject.SetActive(false);
        }
    }

    private void SetSpawner()
    {
        if (gameObject.activeSelf)
        {
            AudioManager.Play(AudioClipName.OpenChest2);
            int numOrbs = spawn.pointsToOrbs(orbPoints);
            spawn.SpawnOrbs(numOrbs);
        }
    }
}
