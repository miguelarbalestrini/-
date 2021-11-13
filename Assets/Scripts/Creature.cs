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

    #endregion

    #region Atributtes 

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


    private void die()
    {
        this.isAlive = false;
        Destroy(this);
    }


    #endregion

    #region PublicMethods

    //atack()
    //getHealth() 
    //respawn()
    //setAttkCooldown
    public void getDamage(int damage)
    {
        this.health = this.health - damage; 
        if (this.health >= 0)
        {
            this.die();
        }
    }

    #endregion
}
