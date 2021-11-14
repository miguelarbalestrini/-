using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private float damage;
    [SerializeField]
    private bool grounded;
        

    #endregion

    #region Properties 

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public bool Grounded
    {
        get { return grounded; }
    }

    #endregion
}
