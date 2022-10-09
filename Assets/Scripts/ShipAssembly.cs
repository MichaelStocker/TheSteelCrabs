using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAssembly : MonoBehaviour
{
    [Header("----- Ship Parts -----")]
    [SerializeField] GameObject shipHull;
    [SerializeField] GameObject shipEngine;
    [SerializeField] GameObject shipWings;

    [SerializeField] GameObject objectPickup;

    [Header("----- Parts Collected -----")]
    public bool hullCollected;
    public bool engineCollected;
    public bool wingsCollected;
    public bool shipAssemblyCoomplete;

    // Start is called before the first frame update
    void Start()
    {
        shipEngine.SetActive(false);
        shipHull.SetActive(false);
        shipWings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (wingsCollected && hullCollected && engineCollected) shipAssemblyCoomplete = true;
        if (shipAssemblyCoomplete) Debug.Log("Ship Complete! You Win!");

    }
    void AssembleShip()
    {
        if (objectPickup.name == "Ship Wings Pickup")
        {
            shipWings.SetActive(true);
            wingsCollected = true;
        }
        else if (objectPickup.name == "Ship Hull Pickup")
        {
            shipHull.SetActive(true);
            hullCollected = true;
        }
        else if (objectPickup.name == "Ship Engines Pickup")
        {
            shipEngine.SetActive(true);
            engineCollected = true;
        }

        if (wingsCollected && hullCollected && engineCollected) shipAssemblyCoomplete = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AssembleShip();
            Destroy(objectPickup, 0.2f);
            //DestroyImmediate(healthKitModel, true);
        }
    }
}
