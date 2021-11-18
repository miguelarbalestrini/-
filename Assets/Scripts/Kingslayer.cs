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

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        this.RemainingCD = this.atkCooldown;
        hiddenWeapon = this.transform.GetChild(4).gameObject.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        Atack();
        this.AtackCooldown();
        this.RenderHP();
        //Debug.Log(RemainingCD);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (weapon.IsGrounded && weapon.PickText != null)
        {
            weapon.PickText.gameObject.SetActive(true);
            weapon.PickText.text = "Press E to pick weapon";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && weapon.IsGrounded)
        {
            this.transform.GetChild(3).gameObject.SetActive(true);
            hiddenWeapon.gameObject.SetActive(true);
            PickWeapon(hiddenWeapon);
            weapon.DestroyWeapon();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (weapon.IsGrounded && weapon.PickText != null)
        {
            weapon.PickText.gameObject.SetActive(false);
        }
    }

    #endregion

    #region ProtectedMethods

    protected override void Atack()
    {
        base.Atack();
        if (Input.GetMouseButtonDown(0) && !this.AtkInCooldown)
        {
            //Debug.Log($"atack");
            //this.hiddenWeapon.gameObject.GetComponent<Collider>().isTrigger = true;
            this.weapon.MakeLongDamage(6f);
            this.AtkInCooldown = true;
            AnimationController.SetBool("isAttacking", true);
            this.RemainingCD = this.atkCooldown;
        } else if (RemainingCD < 1f){
            this.weapon.MakeLongDamage(6f);
            AnimationController.SetBool("isAttacking", false);
           //this.hiddenWeapon.gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }

    #endregion

    #region PrivateMethods

    private void PickWeapon(Weapon newWeapon)
    {
        this.weapon = newWeapon;
    }

    #endregion
}
