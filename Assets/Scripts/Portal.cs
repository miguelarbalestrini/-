using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string levelName = "Level1";
    [SerializeField] GameObject finishScreen;

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Stop();
        AudioManager.Play(AudioClipName.ActivatePortal);
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level1" && finishScreen)
        {
            Cursor.visible = true;
            finishScreen.SetActive(true);
        }
        else
        {
            SceneManager.LoadSceneAsync(levelName);
            AudioManager.PlayLoopDelayed(AudioClipName.BackgroundBoss1, 1.5f);
        }
    }
}
