using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Creature : MonoBehaviour
{
    #region Fields

    [SerializeField]
    protected float health;
    [SerializeField]
    protected enum CharClass {Warrior, Mage, Archer};
    [SerializeField]
    protected float atkCooldown;
    [SerializeField]
    protected bool atkInCooldown = false;
    [SerializeField]
    private bool isAlive = true;
    [SerializeField]
    private float respawnTime;
    [SerializeField]
    private Animator animationControler;
    [SerializeField]
    protected TextMesh lifeText = null;
    private float remainingCD;
    public GameEvent dead;
    public event Action onHit;
    public event Action onAtack;


    #endregion

    #region Properties 

    public float Health
    {
        get { return health; }
    }

    public float AtkCooldown
    {
        get { return atkCooldown; }
        set { atkCooldown = value; }
    }

    public bool AtkInCooldown
    {
        get { return atkInCooldown; }
        set { atkInCooldown = value; }
    }


    public float RespawnTime
    {
        set { respawnTime = value; }
    }

    public float RemainingCD
    {
        get { return remainingCD; }
        set { remainingCD = value; }
    }

    public Animator AnimationController
    {
        get { return animationControler; }
    }
    
    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        this.remainingCD = this.atkCooldown;
    }

    // Update is called once per frame
    void Update()
    {
    }

    #endregion

    #region PrivateMethods

    private void Respawn()
    {
        this.isAlive = true;
    }

    protected void AtackCooldown()
    {
        if(this.atkInCooldown && this.remainingCD >= 0)
        {
            this.remainingCD -= Time.deltaTime;
            //Debug.Log($"creature log cd remaining {remainingCD}");
        }
        if(this.remainingCD <= 0)
        {
            this.atkInCooldown = false;
        }
    }

    #endregion

    void onDead()
    {
        if (dead != null) {
            Debug.Log(dead);
            dead.Raise();
            Debug.Log("soy un unity event");
        }

        Debug.Log(dead);
        Debug.Log("soy un unity event null");
    }

    #region ProtectedMethods

    protected virtual void Atack() 
    {
        this.onAtack();
    }

    protected void Die()
    {
        onDead();
        this.isAlive = false;
        //Debug.Log($"Dead: {this.health}");
        //Destroy(gameObject);
        Respawn();
    }

    #endregion

    #region PublicMethods

    public void GetDamaged(float damage)
    {
        
        this.health -= damage;
        if (gameObject.TryGetComponent(out Kingslayer player))
        {
            player.onHit();
        }
           
        if (this.health <= 0)
        {
            //Debug.Log($"Health: {this.health}");
            this.Die();
            this.Respawn();
        }
    }

    public void RenderHP()
    {
        if (this.lifeText != null)
        {
            this.lifeText.text = $"HP: {this.health.ToString()}";
        }
    }
    #endregion
}