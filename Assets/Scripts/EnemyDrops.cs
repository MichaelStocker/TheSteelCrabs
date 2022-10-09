using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [SerializeField] GameObject healthKitModel;
    [SerializeField] int healthPerDrop;
    public bool isDropping;

    // Update is called once per frame
    void Update()
    {



    }
    public void HealPlayer()
    {
        gameManager.instance.playerScript.GivePlayerHP(healthPerDrop);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealPlayer();
            Destroy(healthKitModel,0.2f);
        }
    }
}
