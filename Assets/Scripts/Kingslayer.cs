using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kingslayer : Creature
{
    #region Fields

    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private Weapon hiddenWeapon;
    [SerializeField] private ItemController item;
    [SerializeField] private Weapon[] ArrayWeapons;
    [SerializeField] private HealthBar healtBar;
    private float maxHealt;
    //[SerializeField] private List <GameObject> listOfWeapons = new List<GameObject>();

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        this.RemainingCD = this.atkCooldown;
        maxHealt = this.Health;
        healtBar.SetMaxHealth(maxHealt);
        EventManager.StartListening("onDamaged", this.GetDamaged);
    }

    // Update is called once per frame
    void Update()
    {
        Atack();
        this.AtackCooldown();
        //this.RenderHP();
        UpdateHealtBar();
        this.ChangeWeapon();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && item.ItemIsGrounded())
        {
            this.transform.GetChild(3).gameObject.SetActive(true);
            hiddenWeapon.gameObject.SetActive(true);
            item.DestroyItem();
        }
    }

    #endregion

    #region ProtectedMethods

    protected override void Atack()
    {
        if (Input.GetMouseButtonDown(0) && !this.AtkInCooldown)
        {
            base.Atack();
            //Debug.Log($"atack");
            this.hiddenWeapon.gameObject.GetComponent<Collider>().isTrigger = true;
            //this.weapon.MakeLongDamage(6f);
            this.AtkInCooldown = true;
            AnimationController.SetBool("isAttacking", true);
            this.RemainingCD = this.atkCooldown;
        } else if (RemainingCD < 1f){
            //this.weapon.MakeLongDamage(6f);
            AnimationController.SetBool("isAttacking", false);
            this.hiddenWeapon.gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }

    private void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.transform.GetChild(3).gameObject.SetActive(true);
            ArrayWeapons[1].gameObject.SetActive(true);
            ArrayWeapons[2].gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.transform.GetChild(3).gameObject.SetActive(false);
            ArrayWeapons[1].gameObject.SetActive(false);
            ArrayWeapons[2].gameObject.SetActive(true);
        }
    }
    private void UpdateHealtBar()
    {
        healtBar.SetHealth(this.Health);
    }
    #endregion

    protected override void GetDamaged(EventParam eventParam)
    {
        base.GetDamaged(eventParam);
        if (GameObject.ReferenceEquals(eventParam.gameObjParam, this.gameObject))
        {
            //Debug.Log($"DAMAGE:  {this.Damage}");
            //Debug.Log($"Score {GameManager.GetScore()}");
            EventManager.RaiseEvent("onHit");
        }
    }
}
