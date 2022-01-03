using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager s; // Singleton

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Kingslayer player;
    [SerializeField] private Image lockIcon;
    private EnemyController lockedEnemy;

    private void Awake()
    {
        if (!s) s = this;
        else Destroy(gameObject);
        lockIcon.enabled = false;
       
    }

    public Camera  MainCamera
    {
        set { this.mainCamera = value; }
    }

    void Update()
    {
        if (lockedEnemy && mainCamera != null)
        {
            gameObject.transform.position = lockedEnemy.Chest.position;
            //hpBar.ColorChanger();
            //hpBar.HealtBarFiller();
        }
        checkStatus();
    }

    public void LockEnemy(EnemyController enemy)
    {
        if (enemy)
        {
            lockedEnemy = enemy;
            lockIcon.enabled = true;
            EventParam eventParam = new EventParam();
            eventParam.enemyControllerParam = lockedEnemy;
            EventManager.RaiseEvent("onLock", eventParam);
        }
     
    }

    public void UnlockEnemy()
    {
        lockedEnemy = null;
        lockIcon.enabled = false;
    }

    private void checkStatus()
    {
        if (lockedEnemy && lockedEnemy.Health <= 0)
        {
            lockIcon.enabled = false;
        }
    }
}