using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private Image panel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject enemiesConteiner;
    [SerializeField] private GameObject vanishedWalls;
    [SerializeField] private Kingslayer player;

    private void Update()
    {
        disableWalls();
    }

    private void OnTriggerEnter(Collider other)
    {
        panel.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        panel.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        if (player)
        {
            player.enabled = true;
        }
        ActiveEnemies();
    }

    private void disableWalls()
    {
        if (vanishedWalls && GameManager.GetScore() >= 3)
        {
            vanishedWalls.SetActive(false);
        }
    }

    private void ActiveEnemies()
    {
        if (enemiesConteiner)
        {
            foreach (Transform children in enemiesConteiner.transform)
            {
                children.gameObject.SetActive(true);
            }
        }
    }
}
