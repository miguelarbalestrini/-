using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeWeapon : Weapon
{
    #region Fields

    [SerializeField] private GameObject atackOrigin;

    #endregion

    #region UnityMethods

    void FixedUpdate()
    {
        makeLongDamage(this.range);  
    }

    #endregion

    #region PrivateMothods

    private void makeLongDamage(float maxRange)
    {
        RaycastHit hit;
        if (Physics.Raycast(atackOrigin.transform.position, atackOrigin.transform.TransformDirection(Vector3.forward), out hit, maxRange))
        {
            if (hit.transform.gameObject.TryGetComponent(out Kingslayer player))
            {
                Debug.DrawRay(atackOrigin.transform.position, atackOrigin.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.DrawRay(atackOrigin.transform.position, atackOrigin.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                this.makeDamage(player);
            }
        }
        else
        {
            Debug.DrawRay(atackOrigin.transform.position, atackOrigin.transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            Debug.Log("Did not Hit");
        }
    }
    #endregion
}
