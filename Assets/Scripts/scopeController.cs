using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scopeController : MonoBehaviour
{
    [SerializeField] GameObject scopeMask;
    bool isAiming;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        ScopeOverlayToggle(isAiming);
    }
    void ScopeOverlayToggle(bool temp)
    {
        if (!isAiming && Input.GetButton("Aim"))
        {
            isAiming = true;
            scopeMask.SetActive(isAiming);
        }
        isAiming = false;
        scopeMask.SetActive(isAiming);
    }
}
