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
    Collider weapon;


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
        EventManager.StartListening("onAnimationAtkFinished", DeactivateWeapon);
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
        Debug.Log($"ENCONTRADO: {other.gameObject.name}");
        if (other.gameObject != null)
        {
            Debug.Log("COLISION");
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
        AudioManager.Play(AudioClipName.AtkSwing);
        this.gameObject.GetComponent<Collider>().isTrigger = true;
        gameObject.SetActive(true);
        Debug.Log("TE PEGO");
        //StartCoroutine(DeactivateWeapon());

    }
    private void DeactivateWeapon(EventParam eventParam)
    {
       // yield return new WaitForSeconds(0.7f);
       if( gameObject.TryGetComponent(out Collider meleeWeapon))
        {
            Debug.Log("SE DESACTIVO");
            meleeWeapon.isTrigger = false;
        }
    }
    #endregion
}
