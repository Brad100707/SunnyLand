using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{

    // Update is called once per frame
    void CherryGet()
    {
        FindObjectOfType<PlayerController>().CherryGotto();
        Destroy(gameObject);
    }
}
