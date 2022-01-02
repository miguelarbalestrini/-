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
    [SerializeField] bool isMelee = true;
    Collider currentWeapon;


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

    public bool IsMelee
    {
        get { return isMelee; }
    }

    public TextMesh PickText
    {
        get { return pickText; }
    }
    #endregion

    #region UnityMethods
    private void Start()
    {
        if (isMelee && this.gameObject.TryGetComponent(out Collider meleeWeapon))
        {
            meleeWeapon.isTrigger = false;
        }
        EventManager.StartListening("onAnimationAtkInitEnemy", ActivateMeleeWeaponAtk);
        EventManager.StartListening("onAnimationAtkFinishedEnemy", DeactivateMeleeWeaponAtk);
        EventManager.StartListening("onAnimationAtkInitPlayer", ActivateMeleeWeaponAtk);
        EventManager.StartListening("onAnimationAtkFinishedPlayer", DeactivateMeleeWeaponAtk);
    }

    protected void makeDamage(GameObject target)
    {
        EventParam eventParam = new EventParam();
        if (target != null)
        {
            eventParam.gameObjParam = target;
            eventParam.floatParam = this.damage;
            EventManager.RaiseEvent("onDamaged", eventParam);
        }
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {
            this.makeDamage(other.gameObject);
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
            this.makeDamage(hit.transform.gameObject);
        }
    }

    public void MakeMeleeDamage()
    {
        if (gameObject.TryGetComponent(out Collider meleeWeapon))
        {
            currentWeapon = meleeWeapon;
        };
    }

    private void ActivateMeleeWeaponAtk(EventParam eventParam)
    {
        if (gameObject.TryGetComponent(out Collider meleeWeapon))
        {
            AudioManager.Play(AudioClipName.AtkSwing);
            if (GameObject.ReferenceEquals(currentWeapon, meleeWeapon) && currentWeapon != null)
            {
                meleeWeapon.isTrigger = true;
            }
        }
    }

    private void DeactivateMeleeWeaponAtk(EventParam eventParam)
    {
        if (gameObject.TryGetComponent(out Collider meleeWeapon))
        {
            if (GameObject.ReferenceEquals(currentWeapon, meleeWeapon) && currentWeapon)
            {
                meleeWeapon.isTrigger = false;
            }
        }
    }
    #endregion
}
