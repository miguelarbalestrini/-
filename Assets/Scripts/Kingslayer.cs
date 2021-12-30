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
    protected override void Start()
    {
        base.Start();
        maxHealt = this.Health;
        healtBar.SetMaxHealth(maxHealt);
        //EventManager.StartListening("onLeftClick", this.Atack);
    }

    // Update is called once per frame
    void Update()
    {
        this.Atack();
        //this.resetEstance();
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
        if (Input.GetMouseButtonDown(0) && !this.attackCDTimer.Running)
        {
            Debug.Log("No Tengo cooldown");
            base.Atack();
            this.attackCDTimer.Run();
            //Debug.Log($"atack");
            if (this.weapon.IsMelee)
            {
                this.hiddenWeapon.MakeMeleeDamage();
                AnimationController.SetBool("isAttacking", true);
            }
            else
            {
                this.weapon.MakeLongDamage(6f);
            }
        } 
        else if (this.attackCDTimer.SecondsLeft < 1f)
        {
            this.attackCDTimer.Duration = this.atkCooldown;
            AnimationController.SetBool("isAttacking", false);
        }
    }

    private void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.transform.GetChild(3).gameObject.SetActive(true);
            ArrayWeapons[1].gameObject.SetActive(true);
            ArrayWeapons[2].gameObject.SetActive(false);
            weapon = ArrayWeapons[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.transform.GetChild(3).gameObject.SetActive(false);
            ArrayWeapons[1].gameObject.SetActive(false);
            ArrayWeapons[2].gameObject.SetActive(true);
            weapon = ArrayWeapons[2];
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
            EventManager.RaiseEvent("onHit");
        }
    }
}
