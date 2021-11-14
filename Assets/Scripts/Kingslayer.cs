using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kingslayer : Creature
{
    #region Fields

    [SerializeField]
    private Weapon weapon;

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Atack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (weapon.IsGrounded)
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
            this.transform.GetChild(4).gameObject.SetActive(true);
            weapon.DestroyWeapon();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (weapon.IsGrounded)
        {
            weapon.PickText.gameObject.SetActive(false);
        }
    }

    #endregion

    #region ProtectedMethods

    protected override void Atack()
    {
        base.Atack();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"atack");
            AnimationController.SetBool("isAttacking", true);
        } else
        {
            AnimationController.SetBool("isAttacking", false);
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
