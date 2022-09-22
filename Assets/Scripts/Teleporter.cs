using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Teleporter linkedTeleporter;
    [SerializeField] int teleporterCooldown;
    [SerializeField] ParticleSystem party;

    bool canTeleport = true;
    bool isTeleporting;
    
    // Update is called once per frame
    void Update()
    {
        
        
    }
    void PlayerTeleporting()
    {
        isTeleporting = true;
        canTeleport = false;

        gameManager.instance.playerScript.controller2.enabled = false;
        gameManager.instance.player.transform.position = linkedTeleporter.transform.position + new Vector3(1.0f, 1.0f, 1.0f);
        gameManager.instance.playerScript.controller2.enabled = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (linkedTeleporter != null)
            {
                if (canTeleport && !isTeleporting)
                {
                    StartCooldownTimer();
                    PlayerTeleporting();
                    StartCoroutine(gameManager.instance.CountDownStart());
                }
            }
        }
    }
    void StartCooldownTimer()
    {
        StartCoroutine(CooldownCoroutine(teleporterCooldown));
    }
    IEnumerator CooldownCoroutine(float delay)
    {
        canTeleport = false;
        yield return new WaitForSeconds(delay);
        canTeleport = true;
        isTeleporting = false;
    }
}
