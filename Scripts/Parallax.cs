using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform CamPosition;
    public float MoveRate;
    private float StartPointX, StartPointY;
    public bool LockY;
    void Start()
    {
        StartPointX = transform.position.x;
        StartPointY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (LockY)
        {
            transform.position = new Vector2(StartPointX + CamPosition.position.x * MoveRate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(StartPointX + CamPosition.position.x * MoveRate, StartPointY + CamPosition.position.y*MoveRate);
        }
    }
}
