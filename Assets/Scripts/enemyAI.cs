using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamageable
{
    [Header("----- Components -----")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer rend;

    [Header("----- Enemy Stats -----")]
    [Range(1, 10)] [SerializeField] int hP;
    [Range(1, 10)] [SerializeField] int playerFaceSpeed;

    [Header("----- Weapon Stats -----")]
    [SerializeField] float fireRate;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos;
    
    Vector3 playerDir;
    bool isShooting;
    
    // Start is called before the first frame update
    void Start()
    {
        //agent.SetDestination(gameManager.instance.player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = gameManager.instance.player.transform.position - transform.position;

        agent.SetDestination(gameManager.instance.player.transform.position);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!isShooting)
                StartCoroutine(Shoot());
            
            FacePlayer();
            
        }
    }

    void FacePlayer()
    {
        playerDir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation,Time.deltaTime * playerFaceSpeed);
    }
    public void TakeDamage(int dmg)
    {
        hP -= dmg;
        StartCoroutine(FlashColor());
        if (hP <= 0) Destroy(gameObject);
    }

    IEnumerator FlashColor()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rend.material.color = Color.white;
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        Instantiate(bullet, bulletPos.position, transform.rotation);
        
        yield return new WaitForSeconds(fireRate);
        isShooting = false;
    }
}
