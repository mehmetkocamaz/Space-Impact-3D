using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 20;
    private float zMinRange = -15;
    private GameManager gameManager;
    private PlayerController playerController;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if(playerController.hasPowerup == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.z < zMinRange)
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }


}
