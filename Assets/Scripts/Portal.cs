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
        AudioManager.Stop();
        AudioManager.Play(AudioClipName.ActivatePortal);
        SceneManager.LoadSceneAsync(levelName);
        AudioManager.PlayLoopDelayed(AudioClipName.BackgroundBoss1, 1.5f);
    }
}
