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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemy))
        {
            Debug.Log($"DAMAGE:  {this.Damage}");
            if (enemy != null)
            {
                enemy.GetDamaged(this.Damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EnemyController enemy))
        {
            Debug.Log($"DAMAGE:  {this.Damage}");
            if (enemy != null)
            {
                enemy.GetDamaged(this.Damage);
            }
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
