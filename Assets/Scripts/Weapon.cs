using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private float damage;
    [SerializeField]
    private bool grounded = true;
    [SerializeField]
    private TextMesh pickText;        

    #endregion

    #region Properties 

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public bool IsGrounded
    {
        get { return grounded; }
    }

    public TextMesh PickText
    {
        get { return pickText; }
    }
    #endregion

    #region PublicMethod

    public void DestroyWeapon()
    {
        Destroy(gameObject);
    }

    #endregion
}
