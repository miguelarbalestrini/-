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
    [SerializeField] Transform handposition;
    [SerializeField] float arrowSpeed = 1;

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
            Debug.Log($"Score {GameManager.GetScore()}");
        }
        else if (other.gameObject.TryGetComponent(out Kingslayer player))
        {
            makeDamage(player);
            if (!IsGrounded)
            {
                Debug.Log($"Score {GameManager.GetScore()}");
            }
        }
    }

    #endregion

    #region PublicMethod

    public void MakeLongDamage(float maxRange)
    {
        RaycastHit hit;
        if (Physics.Raycast(handposition.transform.position, transform.TransformDirection(Vector3.forward), out hit, maxRange))
        {
            Debug.DrawRay(handposition.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            GameObject projectile = Instantiate(projectilePrefab, handposition.transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * arrowSpeed, ForceMode.Impulse);
            if (hit.transform.gameObject.TryGetComponent(out Kingslayer player))
            {
                this.makeDamage(player);
            }
            if (hit.transform.gameObject.TryGetComponent(out EnemyController enemy))
            {
                this.makeDamage(enemy);
            }
        }
    }

    #endregion
}
