using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    public void Resume()
    {
        if (gameManager.instance.isPaused)
        {
            gameManager.instance.isPaused = !gameManager.instance.isPaused;
            gameManager.instance.CursorUnlockUnpause();
        }
    }

    public void Restart()
    {
        gameManager.instance.CursorUnlockUnpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerRespawn()
    {
        gameManager.instance.playerScript.Respawn();
    }

    public void Quit()
    {
        Application.Quit();
    }



    public void GiveHP(int healthToAdd)
    {
        gameManager.instance.playerScript.GivePlayerHP(healthToAdd);
    }

    public void GiveJump(int jumpsToAdd)
    {
        gameManager.instance.playerScript.GiveJump(jumpsToAdd);
    }

    public void GiveSpeed(int speedToAdd)
    {
        gameManager.instance.playerScript.GiveSpeed(speedToAdd);
    }

}
