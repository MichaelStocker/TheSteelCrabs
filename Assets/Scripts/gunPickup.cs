using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickup : MonoBehaviour
{
    [SerializeField] GunStats gunStat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.playerScript != null)
        {
            gameManager.instance.playerScript.gunPickup(gunStat);
            Destroy(gameObject);
        }
    }

}
