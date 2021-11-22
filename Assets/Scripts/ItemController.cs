using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private TextMesh pickText;
    private bool IsGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsGrounded && pickText != null)
        {
            pickText.gameObject.SetActive(true);
            pickText.text = $"Press E to pick {item.name}";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsGrounded && pickText != null)
        {
            pickText.gameObject.SetActive(false);
        }
    }
    public bool ItemIsGrounded()
    {
        return IsGrounded;
    }
    public GameObject ItemInGround()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(item);
    }
}
