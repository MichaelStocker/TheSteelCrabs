using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class StartGameTimer : MonoBehaviour
{
    //Member Fields
    [Range(3, 10)] [SerializeField] int countDownTimer;
    public Text countDownDisplay;

    void Start()
    {
        StartCoroutine(CountDownStart());   
    }

    IEnumerator CountDownStart()
    {
        //Pauses game
        Time.timeScale = 0;
        
        while(countDownTimer != 0)
        {
            //Sets text to int's value
            countDownDisplay.text = countDownTimer.ToString();

            //Waits a second
            yield return new WaitForSecondsRealtime(1f);

            //Decrement the int
            countDownTimer--;
        }

        //Resumes game
        Time.timeScale = 1;

        //Lets player know they can move now
        countDownDisplay.text = "Eliminate All Enemies";

        //Disables the text getting start off the screen
        yield return new WaitForSeconds(1f);
        countDownDisplay.gameObject.SetActive(false);
    }

}
