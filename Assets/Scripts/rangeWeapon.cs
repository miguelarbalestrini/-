using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeWeapon : Weapon
{
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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxRange))
        {
            if (hit.transform.gameObject.TryGetComponent(out Kingslayer player))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                this.makeDamage(player);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
        }
    }
    #endregion
}
