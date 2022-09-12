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
    public bool playerInRange;
    Vector3 lastPlayerPos;
    float stoppingDistanceOrig;

    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.EnemyIncrement();
        lastPlayerPos = transform.position;
        stoppingDistanceOrig = agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = gameManager.instance.player.transform.position - transform.position;
        //if player is in range, enemy will move towards him. Else stand still.
        if (playerInRange)
        {
            canSeePlayer();
        }
        else
        {
            agent.SetDestination(lastPlayerPos);
            agent.stoppingDistance = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            lastPlayerPos = gameManager.instance.player.transform.position;
        }
    }

    void FacePlayer()
    {
        playerDir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation,Time.deltaTime * playerFaceSpeed);
    }

    void canSeePlayer()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, playerDir, out hit))
        {
            Debug.DrawRay(transform.position, playerDir);
            if (hit.collider.CompareTag("Player"))
            {
                agent.SetDestination(gameManager.instance.player.transform.position);
                agent.stoppingDistance = stoppingDistanceOrig;
                //if the distance is < or = the agent then face the player.
                if (agent.stoppingDistance <= agent.remainingDistance)
                {
                    FacePlayer();
                }
                if (!isShooting)
                {
                    StartCoroutine(Shoot());
                }
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        hP -= dmg;

        lastPlayerPos = gameManager.instance.player.transform.position;

        StartCoroutine(FlashColor());

        if (hP <= 0)
        {
            gameManager.instance.EnemyDecrement();
            Destroy(gameObject);
        }
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
