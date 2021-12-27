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
        //player.onHit += SubsScore;
        //player.onAtack += AddScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        instance.scoreInstanciado += 1;
    }

    public void SubsScore()
    {
        instance.scoreInstanciado -= 1;  
    }

    public static int GetScore()
    {
        return instance.scoreInstanciado;
    }
}
