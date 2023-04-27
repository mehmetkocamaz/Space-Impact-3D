using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private float speed = 20;
    private float zMinRange = -15;
    private GameManager gameManager;
    public int healthPoint = 3;
    public GameObject projectile;
    public GameObject horizontalShield;
    public GameObject verticalShield;
    private PlayerController playerController;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        horizontalShield = GameObject.Find("Horizontal Guard");
        verticalShield = GameObject.Find("Vertical Guard");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {   if(playerController.hasPowerup == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
            Vector3 projectilePosition = new Vector3((transform.position.x), (transform.position.y), (transform.position.z));
            Shooting(projectilePosition);
        }

        DieHard();
        if (transform.position.z < zMinRange)
        {
            Destroy(gameObject);
            gameManager.GameOver();
        }
    }

    void DieHard()
    {
        switch (healthPoint)
        {
            case 3:
                verticalShield.gameObject.SetActive(true);
                horizontalShield.gameObject.SetActive(true);
                break;
            case 2:
                verticalShield.gameObject.SetActive(false);
                horizontalShield.gameObject.SetActive(true);
                break;
            case 1:
                verticalShield.gameObject.SetActive(false);
                horizontalShield.gameObject.SetActive(false);
                break;
            case 0:
                Destroy(gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile")) 
        {
            --healthPoint;
            Destroy(other.gameObject);
        }
    }

    void Shooting(Vector3 projectilePosition)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, projectilePosition, projectile.transform.rotation);
        }
    }
}
