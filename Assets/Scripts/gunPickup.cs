using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickup : MonoBehaviour
{
    [SerializeField] GunStats gunStat;

    //Gun Rotation
    float x;

    private void Update()
    {
        x += Time.deltaTime * 10;
        transform.rotation = Quaternion.Euler(0, x, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.playerScript != null)
        {
            gameManager.instance.playerScript.gunPickup(gunStat);
            Destroy(gameObject);
        }
    }

}
