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
    private bool isMelee = false;
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
        EventManager.StartListening("onLeftClick", this.Atack);
    }

    // Update is called once per frame
    void Update()
    {
        //Atack(new EventParam());
        //this.resetEstance();
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

    protected override void Atack(EventParam eventParam)
    {
        Debug.Log($"cooldown: {AtkInCooldown}");
        if (!this.AtkInCooldown)
        {
            Debug.Log("No Tengo cooldown");
            base.Atack(new EventParam());
            //Debug.Log($"atack");
            if (isMelee)
            {
                this.hiddenWeapon.MakeMeleeDamage();
                AnimationController.SetBool("isAttacking", true);
            }
            else
            {
                this.weapon.MakeLongDamage(6f);
            }
            this.AtkInCooldown = true;
            this.RemainingCD = this.atkCooldown;
        } 
        else
        {
            AnimationController.SetBool("isAttacking", false);
        }
        Debug.Log($"REMAINING CD: {RemainingCD}");
    }

    private void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isMelee = true;
            Debug.Log($"Melee: {isMelee}");
            this.transform.GetChild(3).gameObject.SetActive(true);
            ArrayWeapons[1].gameObject.SetActive(true);
            ArrayWeapons[2].gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isMelee = false;
            Debug.Log($"Melee: {isMelee}");
            this.transform.GetChild(3).gameObject.SetActive(false);
            ArrayWeapons[1].gameObject.SetActive(false);
            ArrayWeapons[2].gameObject.SetActive(true);
        }
    }

    private void UpdateHealtBar()
    {
        healtBar.SetHealth(this.Health);
    }

    private void resetEstance()
    {
        if (this.AtkInCooldown)
        {
            AnimationController.SetBool("isAttacking", false);
            if (isMelee)
            {
                this.hiddenWeapon.gameObject.GetComponent<Collider>().isTrigger = false;
            }
        }
        
    }
    #endregion

    protected override void GetDamaged(EventParam eventParam)
    {
        base.GetDamaged(eventParam);
        if (GameObject.ReferenceEquals(eventParam.gameObjParam, this.gameObject))
        {
            EventManager.RaiseEvent("onHit");
        }
    }
}
