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

    }
    void AssembleShip()
    {
        if (objectPickup.name == "Ship Wings Pickup")
        {
            shipWings.SetActive(true);
            gameManager.instance.wingsCollected = true;
            gameManager.instance.AdjustPartsList(gameManager.instance.wingsOnList);
        }
        else if (objectPickup.name == "Ship Hull Pickup")
        {
            shipHull.SetActive(true);
            gameManager.instance.hullCollected = true;
            gameManager.instance.AdjustPartsList(gameManager.instance.hullOnList);
        }
        else if (objectPickup.name == "Ship Engines Pickup")
        {
            shipEngine.SetActive(true);
            gameManager.instance.engineCollected = true;
            gameManager.instance.AdjustPartsList(gameManager.instance.enginesOnList);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AssembleShip();
            Destroy(objectPickup, 0.2f);
        }
    }
}
