using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    bool readyToFly;
    string textStorage;

    // Start is called before the first frame update
    void Start()
    {
        textStorage = gameManager.instance.triggerAssembly.text;
        gameManager.instance.triggerAssembly.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.instance.canTriggerWin)
        {
            if (readyToFly && Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("Cutscene");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.canTriggerWin)
        {
            gameManager.instance.triggerAssembly.text = textStorage;
            readyToFly = true;
        }
    }
        private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.triggerAssembly.text = "";
            readyToFly = false;
        }
    }
}
