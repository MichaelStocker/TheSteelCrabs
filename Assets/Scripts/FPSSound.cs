using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSSound : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public float calmDist;
    public float dangerDist;

    public List<AudioClip> Calm;
    public List<AudioClip> Dramatic;

    private bool inCalmZone = true;
    private bool inDangerZone = false;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlayMusic(gameObject, Calm[0]);
    }

    // Update is called once per frame
    void Update()
    {
        //Calm Zone
        if (Vector3.Distance(enemy.transform.position, player.transform.position) > calmDist && !inCalmZone)
        {
            Debug.Log("CalmZone...");
            inCalmZone = true;
            inDangerZone = false;
            SoundManager.PlayMusic(gameObject, Calm[0]);
        }

        //Danger Zone
        if (Vector3.Distance(enemy.transform.position, player.transform.position) < calmDist && !inDangerZone)
        {
            Debug.Log("DangerZone...");
            inCalmZone = false;
            inDangerZone = true;
            SoundManager.PlayMusic(gameObject, Dramatic[0]);
        }
    }
}
