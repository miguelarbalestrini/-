using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Creature : MonoBehaviour
{
    #region Fields

    [SerializeField] protected enum CharClass {Warrior, Mage, Archer};
    [SerializeField] protected float health;
    [SerializeField] protected float atkCooldown;
    [SerializeField] private Animator animationControler;
    [SerializeField] protected TextMesh lifeText = null;
    protected Timer attackCDTimer;
    protected Timer movementCDTimer;
    /*[SerializeField] private GameObject orbs;
    [SerializeField] private int points = 10;
    private Orb orb;*/
    private float remainingCD;
    public GameEvent dead;


    #endregion

    #region Properties 

    public float Health
    {
        get { return health; }
    }

    public Animator AnimationController
    {
        get { return animationControler; }
    }

    #endregion

    #region UnityMethods

    protected virtual void Awake()
    {
        this.attackCDTimer = gameObject.AddComponent<Timer>();
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.attackCDTimer.Duration = this.atkCooldown;
        EventManager.StartListening("onDamaged", this.GetDamaged);
        /*OrbsSpawn Orbs = orbs.GetComponent<OrbsSpawn>();
        orb = Orbs.GetComponent<Orb>();
        Orbs.NumOrbsToSpawn = pointToOrbs();*/
    }

    #endregion

    #region PrivateMethods

    private void OnDead()
    {
        if (dead != null)
        {
            dead.Raise();
        }
    }

    /*private int pointToOrbs()
{
    int totalOrbs = points / orb.OrbPoints;
    return totalOrbs;
}*/

    #endregion

    #region ProtectedMethods

    protected virtual void Atack() 
    {
        EventManager.RaiseEvent("onAtack");
    }

    protected void Die()
    {
        this.OnDead();
        gameObject.SetActive(false);
    }

    protected virtual void GetDamaged(EventParam eventParam)
    {
        if (GameObject.ReferenceEquals(eventParam.gameObjParam, this.gameObject))
        {
            float damage = eventParam.floatParam;
            this.health -= damage;
            if (this.health <= 0)
            {
                this.Die();
            }
        }
    }

    #endregion
}