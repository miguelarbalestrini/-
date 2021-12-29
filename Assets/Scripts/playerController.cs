using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region Fields       

    [Header("Player Controller Input")]
    [SerializeField] private string horizontalInput = "Horizontal";
    [SerializeField] private string verticallInput = "Vertical";
    [SerializeField] private KeyCode jumpInput = KeyCode.Space;
    [SerializeField] private KeyCode strafeInput = KeyCode.Tab;
    [SerializeField] private KeyCode sprintInput = KeyCode.LeftShift;
    
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickInput();
    }
    #endregion

    #region Private methods

    private void ClickInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.RaiseEvent("onLeftClick");
        }
    }

    #endregion
}
