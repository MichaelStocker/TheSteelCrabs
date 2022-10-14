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
        //Exits paused state and restarts scene
        gameManager.instance.CursorUnlockUnpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerRespawn()
    {
        //Respawns player
        gameManager.instance.playerScript.Respawn();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        //Loads in main level
        SceneManager.LoadScene("SpaceScene");
    }
    public void ReturnToMainMenu()
    {
        //Loads scene that has the main menu
        SceneManager.LoadScene("Main Menu");
    }
    public void FiringRange()
    {
        SceneManager.LoadScene("FiringRangeScene");
    }

    public void BackMainMenu()
    {
        //Checks for which menu is open
        if (gameManager.instance.menuCurrentlyOpen == gameManager.instance.settingsMenu)
        {
            //Closes settings menu, makes sure active menu is set to nothing, and saves any settings changes
            gameManager.instance.menuCurrentlyOpen.SetActive(false);
            gameManager.instance.menuCurrentlyOpen = null;
            gameManager.instance.SaveSoundSettings();
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
        //Closes the settings menu
        gameManager.instance.menuCurrentlyOpen.SetActive(false);

        //Sets the pause menu as active menu and opens it then saves the settings
        gameManager.instance.menuCurrentlyOpen = gameManager.instance.pauseMenu;
        gameManager.instance.menuCurrentlyOpen.SetActive(true);
        gameManager.instance.SaveSoundSettings();

        //Sets the highlighted button to the needed button
        gameManager.instance.buttonToSelect = gameManager.instance.resumeButton;
        gameManager.instance.eventSystem.SetSelectedGameObject(gameManager.instance.buttonToSelect);
    }

    public void Settings()
    {
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
