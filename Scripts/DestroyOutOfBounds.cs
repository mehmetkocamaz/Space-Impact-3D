using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float zMaxRange = 500.0f;
    private float zMinRange = -15.0f;

    void Start()
    {

    }

    void Update()
    {
        if (transform.position.z > zMaxRange)
        {
            Destroy(gameObject);
        }
        if(transform.position.z < zMinRange)
        {
            Destroy(gameObject);
        }
    }
}
