using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GameObject player;
    public playerController playerScript;

    public Camera cam;
    public GameObject scopeMask;
    public GameObject basicReticle;
    public GameObject pauseMenu;
    public GameObject playerDamage;
    public float zoomMult;
    public float defaultFOV;

    public bool isPaused;
    float timeScaleOrig;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript= player.GetComponent<playerController>();
        timeScaleOrig = Time.timeScale;
        defaultFOV = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {

            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused); 
            if (isPaused) CursorLockPause();
            else CursorUnlockUnpause();
        }
        if (!isPaused)
        {
            if (Input.GetMouseButton(1))
            {
                ZoomCamera(defaultFOV / zoomMult);
                scopeMask.SetActive(true);
                basicReticle.SetActive(false);
            }
            else if (cam.fieldOfView != defaultFOV)
            {
                ZoomCamera(defaultFOV);
                scopeMask.SetActive(false);
                basicReticle.SetActive(true);
            }
        }
    }
    public void CursorLockPause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }
    public void CursorUnlockUnpause()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = timeScaleOrig;
        pauseMenu.SetActive(false);
    }
    void ZoomCamera(float target)
    {
        float angle = Mathf.Abs((defaultFOV/zoomMult)-defaultFOV);
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, target, angle * Time.deltaTime);
    }
}
