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
    private TextMesh pickText = null;
    protected Vector3 targetPosition;
    [SerializeField]
    protected float range;

    public Vector3 TargetPosition
    {
        set { this.targetPosition = value; }
    }

    public float Range
    {
        set { this.range = value; }
    }

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

    #region UnityMethods

    protected void makeDamage(Creature target)
    {
        if (target != null)
        {
            target.GetDamaged(this.Damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyController enemy))
        {
            Debug.Log($"DAMAGE:  {this.Damage}");
            makeDamage(enemy);
        }
        else if (other.gameObject.TryGetComponent(out Kingslayer player))
        {
            makeDamage(player);
        }
    }

    #endregion

    #region PublicMethod

    public void DestroyWeapon()
    {
        Destroy(gameObject);
    }

    #endregion
}
