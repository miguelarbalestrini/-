using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Kingslayer player;
    // Start is called before the first frame update

    //SCORE
    public static int score;

    private int scoreInstanciado;
    private bool isPaused = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            score = 0;
            scoreInstanciado = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        EventManager.StartListening("onHit", SubsScore);
        EventManager.StartListening("onAtack", AddScore);
        EventManager.StartListening("onPause", PauseGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(EventParam eventParam)
    {
        instance.scoreInstanciado += 1;
    }

    public void SubsScore(EventParam eventParam)
    {
        instance.scoreInstanciado -= 1;  
    }

    public static int GetScore()
    {
        return instance.scoreInstanciado;
    }

    private void PauseGame(EventParam eventParam)
    {
        if(!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            ResumeGame();
        }
    }

    private void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}
