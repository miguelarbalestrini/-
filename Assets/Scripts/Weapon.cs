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
    [SerializeField] 
    private GameObject projectilePrefab;
    protected Vector3 targetPosition;

    public Vector3 TargetPosition
    {
        set { this.targetPosition = value; }
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
            //Debug.Log($"DAMAGE:  {this.Damage}");
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

    public void MakeLongDamage(float maxRange)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxRange))
        {
            if (hit.transform.gameObject.TryGetComponent(out Kingslayer player))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 10f, ForceMode.Impulse);
                this.makeDamage(player);
            }
        }
    }

    #endregion
}
