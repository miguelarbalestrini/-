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
        //lockIcon = GetComponent<Image>();
        lockIcon.enabled = false;
    }

    public Camera  MainCamera
    {
        set { this.mainCamera = value; }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lockedEnemy && mainCamera != null)
        {
            gameObject.transform.position = lockedEnemy.Chest.position;
            //player.transform.LookAt(lockedEnemy.transform);
        }
    }

    public void LockEnemy(EnemyController enemy)
    {
        lockedEnemy = enemy;
        lockIcon.enabled = true;
    }

    public void UnlockEnemy()
    {
        lockedEnemy = null;
        lockIcon.enabled = false;
    }
}