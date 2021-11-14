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
    private float atkCooldown;
    [SerializeField]
    private bool isAlive = true;
    [SerializeField]
    private float respawnTime;
    [SerializeField]
    private Animator animationControler;

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

    public float RespawnTime
    {
        set { respawnTime = value; }
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

    #endregion

    #region ProtectedMethods

    protected virtual void Atack() {}

    protected void GetDamaged(int damage)
    {
        this.health = this.health - damage;
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