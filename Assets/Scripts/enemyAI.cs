using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamageable
{
    [Header("----- Components -----")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer rend;
    [SerializeField] Animator anim;
    [SerializeField] GameObject HeadPosition;
    [SerializeField] GameObject medKitToDrop;

    [Header("----- Enemy Stats -----")]
    [Range(1, 10)][SerializeField] int hP;
    [Range(1, 10)][SerializeField] float speedChase;
    [Range(1, 10)][SerializeField] int playerFaceSpeed;
    [Range(1, 50)][SerializeField] int roamRadius;
    [Range(1, 180)][SerializeField] int viewAngle;
    public bool roamingEnemy;

    [Header("----- Weapon Stats -----")]
    [SerializeField] float fireRate;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos;

    public GameObject enemyPrefab;
    Vector3 playerDir;
    bool isShooting;
    bool takingDmg;
    public bool playerInRange;
    Vector3 lastPlayerPos;
    float stoppingDistanceOrig;
    float speedOrig;
    Vector3 startingPos;
    bool roamPathValid;
    float angle;
    System.Random rand = new System.Random();
    int randy;
    int hpOG;
    bool respawnEnemy;

    // Start is called before the first frame update
    void Start()
    {
        hpOG = hP;
        lastPlayerPos = transform.position;
        stoppingDistanceOrig = agent.stoppingDistance;
        speedOrig = agent.speed;
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.instance.isFiringRange && agent.enabled)
        {
            angle = Vector3.Angle(playerDir, transform.forward);
            playerDir = gameManager.instance.player.transform.position - HeadPosition.transform.position; playerDir.y += 1;

            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agent.velocity.normalized.magnitude, Time.deltaTime * 4));
            if (!takingDmg)
            {
                if (!roamingEnemy)
                {
                    agent.stoppingDistance = stoppingDistanceOrig;
                    agent.SetDestination(gameManager.instance.player.transform.position);
                }
                if (playerInRange)
                {
                    canSeePlayer();
                }

                if (agent.remainingDistance < 0.1f && agent.destination != gameManager.instance.player.transform.position && roamingEnemy)
                {
                    roam();
                }
            }
        }
    }
    void roam()
    {
        agent.stoppingDistance = 0;
        agent.speed = speedOrig;

        Vector3 randomDir = Random.insideUnitSphere * roamRadius;
        randomDir += startingPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDir, out hit, 1, 1);
        NavMeshPath path = new NavMeshPath();

        agent.CalculatePath(hit.position, path);
        agent.SetPath(path);

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
        if (roamingEnemy && other.CompareTag("Player"))
        {
            lastPlayerPos = gameManager.instance.player.transform.position;
            agent.stoppingDistance = 0;
        }
        playerInRange = false;
    }

    void FacePlayer()
    {
        //playerDir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * playerFaceSpeed);
    }

    void canSeePlayer()
    {
        float angle = Vector3.Angle(playerDir, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(HeadPosition.transform.position, playerDir, out hit))
        {
            Debug.DrawRay(HeadPosition.transform.position, playerDir);
            if (hit.collider.CompareTag("Player") && angle <= viewAngle)
            {
                agent.SetDestination(gameManager.instance.player.transform.position);
                agent.stoppingDistance = stoppingDistanceOrig;

                FacePlayer();

                if (!isShooting)
                {
                    StartCoroutine(Shoot());
                }
            }
            else
            {
                agent.stoppingDistance = 0;
            }
            if (gameManager.instance.playerDeadMenu.activeSelf)
            {
                playerInRange = false;
                agent.stoppingDistance = 0;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        hP -= dmg;
        anim.SetTrigger("Damage");

        lastPlayerPos = gameManager.instance.player.transform.position;
        agent.stoppingDistance = 0;

        StartCoroutine(FlashColor());

        if (hP <= 0 && agent.enabled)
        {
            enemyDead();
        }
    }

    IEnumerator FlashColor()
    {
        takingDmg = true;
        agent.speed = 0;
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.50f);
        rend.material.color = Color.white;
        agent.speed = speedOrig;
        takingDmg = false;
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        Instantiate(bullet, bulletPos.position, transform.rotation);

        yield return new WaitForSeconds(fireRate);
        isShooting = false;
    }

    void enemyDead()
    {
        randy = rand.Next(10000);
        if (!gameManager.instance.isFiringRange && randy > 7000) Instantiate(medKitToDrop, transform.position, transform.rotation);


        anim.SetBool("Dead", true);
        agent.enabled = false;
        //if (gameManager.instance.isFiringRange && !respawnEnemy)
        //{
        //    StartCoroutine(EnemyRespawn());
        //    respawnEnemy = true;
        //}
        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }

    }
    IEnumerator EnemyRespawn()
    {
        yield return new WaitForSeconds(2f);
        // respawn enemy in the same position with all necessary statistics (health)
    }
}
