using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    #region DEFINE
    private Rigidbody playerRb;
    private float horizontalInput;
    private float verticalInput;
    private float horizontalSpeed = 300;
    private float verticalSpeed = 270;
    private float maxVelocity = 70;
    private float xRange = 96;
    private float yRange = 48;
    public bool hasPowerup = false;
    public bool hasPowerupTwo = false;
    public GameObject projectilePrefabs;
    private GameManager gameManager;
    public TextMeshProUGUI powerupText;
    private float fireRate = 0.25f;
    private float nextFire = 0.0f;
    #endregion

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        #region Define etc.
        if (playerRb.velocity.sqrMagnitude > maxVelocity)
        {
            playerRb.velocity *= 0.99f;
        }
        Vector3 rightProjectile = new Vector3((transform.position.x + 2.25f), (transform.position.y - 0.15f), (transform.position.z + 2.145f));
        Vector3 leftProjectile = new Vector3((transform.position.x -2.25f), (transform.position.y - 0.15f), (transform.position.z + 2.145f));
        #endregion
        if (gameManager.isGameActive == true)
        {
            PlayerControlling();
            PlayerBoundaries();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire && hasPowerupTwo == false)
            {
                Shooting(leftProjectile, rightProjectile);
            }
            if(Time.time > nextFire && hasPowerupTwo == true)
            {
                PowerupTwo(leftProjectile, rightProjectile);
            }
        }
    }

    void PlayerControlling()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector3.right * Time.deltaTime * horizontalInput * horizontalSpeed, ForceMode.Impulse);
        playerRb.AddForce(Vector3.up * Time.deltaTime * verticalInput * verticalSpeed, ForceMode.Impulse);
    }

    void PlayerBoundaries()
    {
        float xMovementClamp = Mathf.Clamp(transform.position.x, -xRange, xRange);
        float yMovementClamp = Mathf.Clamp(transform.position.y, -yRange, yRange);
        Vector3 limitPlayerMovement = new Vector3(xMovementClamp, yMovementClamp, 0);
        transform.position = limitPlayerMovement;
    }

    void Shooting(Vector3 leftProjectile,Vector3 rightProjectile)
    {
        nextFire = Time.time + fireRate;
        Instantiate(projectilePrefabs, leftProjectile, projectilePrefabs.transform.rotation);
        Instantiate(projectilePrefabs, rightProjectile, projectilePrefabs.transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupText.text = "Frozen Enemies!!";
            powerupText.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
        if (other.CompareTag("PowerupTwo"))
        {
            hasPowerupTwo = true;
            Destroy(other.gameObject);
            powerupText.text = "Auto-ATTACK!!";
            powerupText.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
        else if (other.CompareTag("BadProjectile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            gameManager.GameOver();
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        hasPowerupTwo = false;
        powerupText.gameObject.SetActive(false);
    }

    void PowerupTwo(Vector3 leftProjectile, Vector3 rightProjectile)
    {
        nextFire = Time.time + (fireRate - 0.15f);
        Instantiate(projectilePrefabs, leftProjectile, projectilePrefabs.transform.rotation);
        Instantiate(projectilePrefabs, rightProjectile, projectilePrefabs.transform.rotation);
    }
}
