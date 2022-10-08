using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
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

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackMainMenu()
    {
        gameManager.instance.settingsMenu.SetActive(false);
        gameManager.instance.menuCurrentlyOpen = null;
        gameManager.instance.startMenu.SetActive(true);
        gameManager.instance.SaveSoundSettings();
    }

    public void BackInGame()
    {
        gameManager.instance.menuCurrentlyOpen.SetActive(false);
        gameManager.instance.pauseMenu.SetActive(true);
        gameManager.instance.SaveSoundSettings();
    }

    public void Settings()
    {
        if (gameManager.instance.menuCurrentlyOpen == null) gameManager.instance.startMenu.SetActive(false);
        else gameManager.instance.menuCurrentlyOpen.SetActive(false);

        gameManager.instance.settingsMenu.SetActive(true);
        gameManager.instance.menuCurrentlyOpen = gameManager.instance.settingsMenu;
    }

    public void SetDisplayMode(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetSensitivityHori(float sensitivity)
    {
        gameManager.instance.sensHor = (int)sensitivity;
    }

    public void SetSensitivityVert(float sensitivity)
    {
        gameManager.instance.sensVert = (int)sensitivity;
    }

    public void SetBrightness()
    {

    }

    public void SetMainVolume()
    {
        //audioMixer.SetFloat("Main Volume", volume);
    }

    public void UpdateSound()
    {
        gameManager.instance.MainVolume.volume = gameManager.instance.mainVolumeSlider.value;
        gameManager.instance.SFXVolume.volume = gameManager.instance.SFXSlider.value;
        gameManager.instance.MusicVolume.volume = gameManager.instance.musicVolumeSlider.value;
    }



}
