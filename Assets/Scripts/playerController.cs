using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour, IDamageable
{
    [SerializeField] CharacterController controller;
    [SerializeField] Camera mainCam;

    [SerializeField] int playerHealth;
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravityValue;
    [SerializeField] int jumpsMax;

    [SerializeField] float fireRate;
    [SerializeField] int shootDistance;
    [SerializeField] int shootDamage;

    

    int jumpCounter;
    private Vector3 playerVelocity;
    Vector3 move;
    bool isShooting;

    private void Start()
    {
        
    }

    void Update()
    {
        Movement();
        StartCoroutine(Shoot());
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
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    IEnumerator Shoot()
    {
        if (!isShooting && Input.GetButton("Shoot"))
        {
            isShooting = true;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDistance))
            {
                if (hit.collider.GetComponent<IDamageable>() != null) 
                    hit.collider.GetComponent<IDamageable>().TakeDamage(shootDamage);
            }

            yield return new WaitForSeconds(fireRate);
            isShooting = false;
        }
    }
    public void GivePlayerHP(int healthToAdd)
    {
        playerHealth += healthToAdd;
    }
    public void GiveJump(int jumpsToAdd)
    {
        jumpsMax += jumpsToAdd;
    }
    public void GiveSpeed(int speedToAdd)
    {
        playerSpeed += speedToAdd;
    }
    public void TakeDamage(int dmg)
    {
        playerHealth -= dmg;
        StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        gameManager.instance.playerDamage.SetActive(true);
        yield return new WaitForSeconds(.1f);
        gameManager.instance.playerDamage.SetActive(false);
    }
}
