using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrbUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textOrb;
    [SerializeField] private GameObject player;
    private Kingslayer playerData;
    // Start is called before the first frame update
    void Start()
    {
        playerData = player.GetComponent<Kingslayer>();
        textOrb.text = $"{playerData.Orbs}" ;
    }

    // Update is called once per frame
    void Update()
    {
        textOrb.text = $"{playerData.Orbs}";
    }
}
