using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour, IDamageable
{
    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;
    [SerializeField] Camera mainCam;

    [Header("----- Player Attributes -----")]
    [SerializeField] int playerHealth;
    [SerializeField] float playerSpeed;
    [SerializeField] float sprintMulti;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;
    [SerializeField] int jumpsMax;

    [Header("----- Gun Stats -----")]
    [SerializeField] float fireRate;
    [SerializeField] int shootDistance;
    [SerializeField] int shootDamage;

    [SerializeField] List<GunStats> gunStat = new List<GunStats>();
    [SerializeField] GameObject gunModel;

    [Header("----- Audio -----")]
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] playerDamage;
    [Range(0, 1)] [SerializeField] float playerDamageVol;

    [SerializeField] AudioClip[] playerJumpSound;
    [Range(0, 1)] [SerializeField] float playerJumpSoundVol;

    [SerializeField] AudioClip[] playerFootStepSound;
    [Range(0, 1)] [SerializeField] float playerFootSoundVol;

    [Range(0, 1)] [SerializeField] float gunShootSoundVol;

    int selectedGun;
    public CharacterController controller2;

    int playerHealthOG;
    int jumpCounter;
    private Vector3 playerVelocity;
    Vector3 move;
    bool isShooting;
    float playerSpeedOG;
    bool isSprinting;
    bool playingFootSteps;

    private void Start()
    {
        controller2 = controller;
        playerHealthOG = playerHealth;
        playerSpeedOG = playerSpeed;
        Respawn();
    }

    void Update()
    {
        if (!gameManager.instance.isPaused)
        {
            Movement();
            gunSelect();
            StartCoroutine(Shoot());
            sprint();
            StartCoroutine(footSteps());
        }
    }

    void Movement()
    {

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpCounter = 0;
        }

        move = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && jumpCounter < jumpsMax)
        {
            playerVelocity.y = jumpHeight;
            jumpCounter++;
            aud.PlayOneShot(playerJumpSound[Random.Range(0, playerJumpSound.Length)], playerJumpSoundVol);
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
            playerSpeed = playerSpeed * sprintMulti;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
            playerSpeed = playerSpeedOG;
        }
    }

    IEnumerator footSteps()
    {
        if (!playingFootSteps && controller.isGrounded && move.normalized.magnitude > 0.3f)
        {
            playingFootSteps = true;
            aud.PlayOneShot(playerFootStepSound[Random.Range(0, playerFootStepSound.Length)], playerFootSoundVol);

            if (isSprinting)
                yield return new WaitForSeconds(0.3f);
            else
                yield return new WaitForSeconds(0.4f);

            playingFootSteps = false;
        }
    }

    void gunSelect()
    {
        if (gunStat.Count > 1)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedGun < gunStat.Count - 1)
            {
                selectedGun++;
                fireRate = gunStat[selectedGun].shootRate;
                shootDistance = gunStat[selectedGun].shootDist;
                shootDamage = gunStat[selectedGun].shootDamage;


                gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat[selectedGun].model.GetComponent<MeshFilter>().sharedMesh;
                gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat[selectedGun].model.GetComponent<MeshRenderer>().sharedMaterial;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedGun > 0)
            {
                selectedGun--;
                fireRate = gunStat[selectedGun].shootRate;
                shootDistance = gunStat[selectedGun].shootDist;
                shootDamage = gunStat[selectedGun].shootDamage;


                gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat[selectedGun].model.GetComponent<MeshFilter>().sharedMesh;
                gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat[selectedGun].model.GetComponent<MeshRenderer>().sharedMaterial;
            }
        }
    }

    public void gunPickup(GunStats stats)
    {
        //when picking up add the stat to the gun.
        fireRate = stats.shootRate;
        shootDamage = stats.shootDamage;
        shootDistance = stats.shootDist;

        gunModel.GetComponent<MeshFilter>().sharedMesh = stats.model.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = stats.model.GetComponent<MeshRenderer>().sharedMaterial;

        //Added the gun to the list.
        gunStat.Add(stats);
    }

    IEnumerator Shoot()
    {
        if (gunStat.Count > 0 && !isShooting && Input.GetButton("Shoot"))
        {
            isShooting = true;
            aud.PlayOneShot(gunStat[selectedGun].sound, gunShootSoundVol);
            Debug.Log("Log Shot");

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDistance))
            {
                if (hit.collider.GetComponent<IDamageable>() != null) 
                    hit.collider.GetComponent<IDamageable>().TakeDamage(shootDamage);

                Instantiate(gunStat[selectedGun].hitEffect, hit.point, transform.rotation);
                Instantiate(gunStat[selectedGun].hitEffect, gunModel.transform.position, transform.rotation);
            }

            yield return new WaitForSeconds(fireRate);
            isShooting = false;
        }
    }

    IEnumerator DamageFlash()
    {
        gameManager.instance.playerDamage.SetActive(true);
        yield return new WaitForSeconds(.1f);
        gameManager.instance.playerDamage.SetActive(false);
    }

    public void Respawn()
    {
        controller.enabled = false;
        playerHealth = playerHealthOG;
        UpdatePlayerHP();
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        gameManager.instance.CursorUnlockUnpause();
        gameManager.instance.isPaused = false;
        controller.enabled = true;
    }

    public void UpdatePlayerHP()
    {
        gameManager.instance.HPBar.fillAmount = (float)playerHealth / (float)playerHealthOG;
    }

    public void TakeDamage(int dmg)
    {
        playerHealth -= dmg;
        UpdatePlayerHP();
        aud.PlayOneShot(playerDamage[Random.Range(0, playerDamage.Length)], playerDamageVol);

        StartCoroutine(DamageFlash());

        if (playerHealth <= 0) gameManager.instance.PlayerIsDead();
    }

    public void GivePlayerHP(int healthToAdd)
    {
        if (playerHealth + healthToAdd > playerHealthOG)
        {
            playerHealth = playerHealthOG;
        }
        else
        {
            playerHealth += healthToAdd;
        }
        UpdatePlayerHP();
    }

    public void GiveJump(int jumpsToAdd)
    {
        jumpsMax += jumpsToAdd;
    }

    public void GiveSpeed(int speedToAdd)
    {
        playerSpeed += speedToAdd;
    }

}
