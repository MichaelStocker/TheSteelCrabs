using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("----- Player -----")]
    public GameObject player;
    public playerController playerScript;
    public GameObject playerSpawnPos;
    public GameObject playerDamage;

    [Header("----- Menus -----")]
    public GameObject menuCurrentlyOpen;
    public GameObject pauseMenu;
    public GameObject playerDeadMenu;
    public GameObject winMenu;

    [Header("----- UI -----")]
    [Range(3, 10)] [SerializeField] int countDownTimer;
    public Image HPBar;
    public Text countDownDisplay;
    public TextMeshProUGUI enemyCounter;

    [Header("----- Scope -----")]
    public GameObject scopeMask;
    public GameObject basicReticle;
    public float zoomMult;
    public float defaultFOV;

    [Header("----- Misc -----")]
    public int enemyCount;
    public int waveCount;
    public bool isCounting;
    public bool isPaused;
    public bool isFiringRange;
    float timeScaleOrig;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript= player.GetComponent<playerController>();
        playerSpawnPos = GameObject.Find("Player Spawn Pos");

        timeScaleOrig = Time.timeScale;
        defaultFOV = Camera.main.fieldOfView;

        isCounting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && menuCurrentlyOpen != playerDeadMenu && menuCurrentlyOpen != winMenu)
        {
            scopeMask.SetActive(false);
            isPaused = !isPaused;
            menuCurrentlyOpen = pauseMenu;
            menuCurrentlyOpen.SetActive(isPaused);
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
            else if (Camera.main.fieldOfView != defaultFOV)
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
        if (menuCurrentlyOpen != null) menuCurrentlyOpen.SetActive(false);
        menuCurrentlyOpen = null;
    }

    public void PlayerIsDead()
    {
        scopeMask.SetActive(false);
        isPaused = true;
        playerDeadMenu.SetActive(true);
        menuCurrentlyOpen = playerDeadMenu;
        CursorLockPause();
    }

    public void EnemyDecrement()
    {
        enemyCount--;
        enemyCounter.text = enemyCount.ToString("F0");
        StartCoroutine(checkEnemyTotal());
    }

    public void EnemyIncrement(int amount)
    {
        enemyCount += amount;
        enemyCounter.text = enemyCount.ToString("F0");
    }

    IEnumerator checkEnemyTotal()
    {
        if (!isFiringRange && enemyCount <= 0)
        {
            yield return new WaitForSeconds(2);
            menuCurrentlyOpen = winMenu;
            menuCurrentlyOpen.SetActive(true);
            CursorLockPause();
        }
    }

    //IEnumerator WinGame()
    //{
    //    yield return new WaitForSeconds(2);
    //    menuCurrentlyOpen = winMenu;
    //    menuCurrentlyOpen.SetActive(true);
    //    CursorLockPause();
    //}

    public IEnumerator CountDownStart()
    {
        //Pauses game & turns on text
        Time.timeScale = 0;
        countDownDisplay.gameObject.SetActive(true);

        while (countDownTimer != 0)
        {
            //Sets text to int's value
            countDownDisplay.text = countDownTimer.ToString();

            //Waits a second
            yield return new WaitForSecondsRealtime(1f);

            //Decrement the int
            countDownTimer--;
        }

        //Resumes game & gives back player functionality
        Time.timeScale = 1;

        //Lets player know they can move now
        countDownDisplay.text = "Go!!!";

        //Disables the text getting start off the screen
        yield return new WaitForSeconds(1f);
        countDownDisplay.gameObject.SetActive(false);
        isCounting = false;
    }

    void ZoomCamera(float target)
    {
        float angle = Mathf.Abs((defaultFOV/zoomMult)-defaultFOV);
        Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, target, angle * Time.deltaTime);
    }

}

