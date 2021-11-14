using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private int health;
    [SerializeField]
    private enum charClass {Warrior, Mage};
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
    private float remainingCD;

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

    public bool AstkInCooldown
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
        set { remainingCD = value; }
    }

    public Animator AnimationController
    {
        get { return animationControler; }
        //set { animationControler = value; }
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
        if(this.atkInCooldown && this.remainingCD > 0)
        {
            this.remainingCD -= Time.deltaTime;
            Debug.Log($"creature log cd remaining {remainingCD}");
        }
        if(this.remainingCD <= 0)
        {
            this.atkInCooldown = false;
        }
    }

    #endregion

    #region ProtectedMethods

    protected virtual void Atack() {}

    protected void GetDamaged(int damage)
    {
        this.health -= damage;
        if (this.health >= 0)
        {
            this.Die();
            this.Respawn();
        }
    }

    protected void Die()
    {
        this.isAlive = false;
        Destroy(this);
        Respawn();
    }

    #endregion

    #region PublicMethods

    #endregion
}