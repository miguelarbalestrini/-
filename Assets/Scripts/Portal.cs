using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string levelName = "level1";
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadSceneAsync(levelName);
        AudioManager.Stop();
        AudioManager.PlayLoop(AudioClipName.BackgroundBoss1);
    }
}
