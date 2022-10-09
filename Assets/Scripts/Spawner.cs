using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int maxEnemies;
    //how quickly they spawn
    [SerializeField] int timer;

    int enemiesSpawned;
    bool isSpawning;
    bool startSpawning;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (startSpawning)
        {
            StartCoroutine(spawn());
        }
    }

    IEnumerator spawn()
    {
        if (!isSpawning && enemiesSpawned < maxEnemies)
        {
            isSpawning = true;
            enemiesSpawned++;

            Instantiate(enemy, transform.position, enemy.transform.rotation);
            yield return new WaitForSeconds(timer);
            isSpawning = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startSpawning = true;
        }
    }
}
