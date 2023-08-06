using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDialog : MonoBehaviour
{

    public GameObject EnterDialog;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            EnterDialog.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnterDialog.SetActive(false);
        }
    }
}
