using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu] 


public class GunStats : ScriptableObject
{
    public float shootRate;
    public int shootDist;
    public int shootDamage;
    public int maximAmmo;
    public int currAmmo;
    public float reloTime;

    [SerializeField] public Animator gunAnim;

    public GameObject model;
    public GameObject sightModel;
    public GameObject silModel;

    public AudioClip sound;
    public GameObject hitEffect;
}
