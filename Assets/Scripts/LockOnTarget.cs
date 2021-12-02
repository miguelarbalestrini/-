using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private bool isLocked;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _camera.transform.rotation = Quaternion.LookRotation(target.position - _camera.transform.position,Vector3.up);
        }   
    }
}
