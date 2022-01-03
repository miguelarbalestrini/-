using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        checkCurrentEscene();
    }

    public void checkCurrentEscene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MainMenu")
        {
            AudioManager.PlayLoop(AudioClipName.BackgroundSurvival);
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadSceneAsync("Level1");
        AudioManager.Stop();
        AudioManager.PlayLoop(AudioClipName.BackgroundBoss1);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
        AudioManager.Stop();
        AudioManager.PlayLoop(AudioClipName.BackgroundBoss2);
    }
}
