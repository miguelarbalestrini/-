using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kingslayer : Creature
{
    #region Fields

    [SerializeField]
    private Weapon weapon;

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        atack();
    }

    #endregion

    #region ProtectedMethods

    protected override void atack()
    {
        base.atack();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"atack");
        }
    }


    #endregion

    #region PrivateMethods

    private void pickWeapon(Weapon newWeapon)
    {
        this.weapon = newWeapon;
    }

    #endregion
}
