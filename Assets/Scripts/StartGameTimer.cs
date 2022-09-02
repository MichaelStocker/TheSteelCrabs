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

        while(countDownTimer != 0)
        {
            //Sets text to int's value
            Thread.Sleep(1000);
            countDownDisplay.text = countDownTimer.ToString();

            //Waits a second
            yield return null;

            //Decrement the int
            countDownTimer--;
        }

        //Lets player know they can move now
        Thread.Sleep(1000);
        countDownDisplay.text = "Start!!!";

        //Disables the text getting start off the screen
        yield return new WaitForSeconds(1f);
        countDownDisplay.gameObject.SetActive(false);
    }

}
