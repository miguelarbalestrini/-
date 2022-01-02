using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private Image panel;
    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
