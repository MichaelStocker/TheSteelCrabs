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
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        //If the game is paused
        if (gameManager.instance.isPaused)
        {
            //The game is unpaused
            gameManager.instance.isPaused = !gameManager.instance.isPaused;
            gameManager.instance.CursorUnlockUnpause();
        }
    }

    public void Restart()
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        //Exits paused state and restarts scene
        gameManager.instance.CursorUnlockUnpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerRespawn()
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        //Respawns player
        gameManager.instance.playerScript.Respawn();
    }

    public void Quit()
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        Application.Quit();
    }

    public void StartGame()
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        //Loads in main level
        SceneManager.LoadScene("SpaceScene");
    }
    public void ReturnToMainMenu()
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        //Loads scene that has the main menu
        SceneManager.LoadScene("Main Menu");
    }
    public void FiringRange()
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        SceneManager.LoadScene("FiringRangeScene");
    }

    public void BackMainMenu()
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        //Checks for which menu is open
        if (gameManager.instance.menuCurrentlyOpen == gameManager.instance.settingsMenu)
        {
            //Closes settings menu, makes sure active menu is set to nothing, and saves any settings changes
            gameManager.instance.menuCurrentlyOpen.SetActive(false);
            gameManager.instance.menuCurrentlyOpen = null;
            gameManager.instance.SaveSettings();
        }
        else if(gameManager.instance.menuCurrentlyOpen == gameManager.instance.creditsMenu)
        {
            //Closes credits and makes sure active menu is set to nothing
            gameManager.instance.menuCurrentlyOpen.SetActive(false);
            gameManager.instance.menuCurrentlyOpen = null;
        }

        //Opens start menu
        gameManager.instance.startMenu.SetActive(true);

        //Sets the highlighted button to the needed button
        gameManager.instance.buttonToSelect = gameManager.instance.startButton;
        gameManager.instance.eventSystem.SetSelectedGameObject(gameManager.instance.buttonToSelect);
    }

    public void BackInGame()
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        //Closes the settings menu
        gameManager.instance.menuCurrentlyOpen.SetActive(false);

        //Sets the pause menu as active menu and opens it then saves the settings
        gameManager.instance.menuCurrentlyOpen = gameManager.instance.pauseMenu;
        gameManager.instance.menuCurrentlyOpen.SetActive(true);
        gameManager.instance.SaveSettings();

        //Sets the highlighted button to the needed button
        gameManager.instance.buttonToSelect = gameManager.instance.resumeButton;
        gameManager.instance.eventSystem.SetSelectedGameObject(gameManager.instance.buttonToSelect);
    }

    public void Settings()
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        //Checks if the main menu is open or if the pause menu is open and closes it
        if (gameManager.instance.menuCurrentlyOpen == null) gameManager.instance.startMenu.SetActive(false);
        else gameManager.instance.menuCurrentlyOpen.SetActive(false);

        //Sets settings menu as active menu and opens it
        gameManager.instance.menuCurrentlyOpen = gameManager.instance.settingsMenu;
        gameManager.instance.menuCurrentlyOpen.SetActive(true);

        //Sets the highlighted button to the needed button
        gameManager.instance.buttonToSelect = gameManager.instance.backButtonSettings;
        gameManager.instance.eventSystem.SetSelectedGameObject(gameManager.instance.buttonToSelect);
    }

    public void Credits()
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        //Closes start menu
        gameManager.instance.startMenu.SetActive(false);

        //Sets credits as active menu and opens it
        gameManager.instance.menuCurrentlyOpen = gameManager.instance.creditsMenu;
        gameManager.instance.menuCurrentlyOpen.SetActive(true);

        //Sets the highlighted button to the needed button
        gameManager.instance.buttonToSelect = gameManager.instance.backButtonCredits;
        new WaitForSeconds(5);
        gameManager.instance.buttonToSelect.SetActive(true);
        gameManager.instance.eventSystem.SetSelectedGameObject(gameManager.instance.buttonToSelect);
    }

    public void SetDisplayMode(bool isFullscreen)
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        Screen.fullScreen = isFullscreen;
    }

    public void SetSensitivityHori(float sensitivity)
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        gameManager.instance.sensHor = (int)sensitivity;
    }

    public void SetSensitivityVert(float sensitivity)
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        gameManager.instance.sensVert = (int)sensitivity;
    }

    public void SetBrightness(float value)
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        if (value != 0) gameManager.instance.exposure.keyValue.value = value;
        else gameManager.instance.exposure.keyValue.value = .05f;
    }

    public void SetMainVolume()
    {
        //audioMixer.SetFloat("Main Volume", volume);
    }

    public void UpdateSound()
    {
        //Play click sound
        gameManager.instance.SFXVolume.PlayOneShot(gameManager.instance.buttonClicked);

        gameManager.instance.MainVolume.volume = gameManager.instance.mainVolumeSlider.value;
        gameManager.instance.SFXVolume.volume = gameManager.instance.SFXSlider.value;
        gameManager.instance.MusicVolume.volume = gameManager.instance.musicVolumeSlider.value;
    }



}
