using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public GameObject goodProjectile;
    public GameObject badProjectile;

    private float speedGoodProjectile = 200;
    private float speedBadProjectile = 100;
    void Start()
    {
        
    }

    void Update()
    {
        goodProjectile.transform.Translate(Vector3.right * Time.deltaTime * speedGoodProjectile);
        badProjectile.transform.Translate(Vector3.forward * Time.deltaTime * speedBadProjectile);
    }
}
