using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kingslayer : Creature
{
    #region Fields

    [SerializeField] private int mp;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Weapon hiddenWeapon;
    [SerializeField] private ItemController item;
    [SerializeField] private Weapon[] ArrayWeapons;
    [SerializeField] private HealthBar healtBar;
    [SerializeField] private int playerOrb = 0;
    private float maxHealt;
    private int maxMP;
    //[SerializeField] private List <GameObject> listOfWeapons = new List<GameObject>();

    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        maxHealt = hp;
        maxMP = mp;
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

    /*private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && item.ItemIsGrounded())
        {
            this.transform.GetChild(3).gameObject.SetActive(true);
            hiddenWeapon.gameObject.SetActive(true);
            item.DestroyItem();
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Orb"))
        {
            GameObject orbPrefab = other.gameObject;
            Orb orb = orbPrefab.GetComponent<Orb>();

            switch (orb.GetTypesOrbs)
            {
                case Orb.typesOrbs.HP:
                    if (hp < maxHealt)
                        hp += orb.OrbValue;
                    else
                        Debug.Log("HP: " + hp);
                    break;
                case Orb.typesOrbs.MP:
                    if (mp < maxMP)
                        mp += orb.OrbValue;
                    else
                        Debug.Log("MP: " + mp);
                    break;
                case Orb.typesOrbs.EnemyOrbs:
                    playerOrb += orb.OrbValue;
                    //Debug.Log(orb.OrbValue);
                    Debug.Log("Orbs: " + playerOrb);
                    break;
                case Orb.typesOrbs.SpPoints:
                    break;
                default:
                    break;
            }
        }
    }

    #endregion

    #region ProtectedMethods

    protected override void Atack()
    {
        if (Input.GetMouseButtonDown(0) && !this.attackCDTimer.Running)
        {
            base.Atack();
            this.attackCDTimer.Run();
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

    protected override void Die()
    {
        base.Die();
        AudioManager.Stop();
        AudioManager.Play(AudioClipName.GameLost);
    }

    private void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MeleeMode();
        }
        /*if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.transform.GetChild(3).gameObject.SetActive(false);
            ArrayWeapons[1].gameObject.SetActive(false);
            ArrayWeapons[2].gameObject.SetActive(true);
            weapon = ArrayWeapons[2];
        }*/
    }

    private void MeleeMode()
    {
        this.transform.GetChild(3).gameObject.SetActive(true);
        ArrayWeapons[1].gameObject.SetActive(true);
        ArrayWeapons[2].gameObject.SetActive(false);
        AudioManager.Play(AudioClipName.MeleeWeaponDraw);
        weapon = ArrayWeapons[1];
    }

    private void OnEnable()
    {
        MeleeMode();
    }

    private void UpdateHealtBar()
    {
        healtBar.SetHealth(hp);
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
