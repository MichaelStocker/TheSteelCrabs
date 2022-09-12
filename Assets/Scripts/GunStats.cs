using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] 


public class GunStats : ScriptableObject
{
    public float shootRate;
    public int shootDist;
    public int shootDamage;
    public int ammoCount;
    public GameObject model;

}
