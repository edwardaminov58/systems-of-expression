using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float time = 120;

    void Start()
    {
        StartCoundownTimer();
    }

    void StartCoundownTimer()
    {
        if (timerText != null)
        {
            time = 120;
            timerText.text = "04:00";
            InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
        }
    }

    void UpdateTimer()
    {
        if (timerText != null)
        {
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            //string fraction = ((time * 100) % 100).ToString("000");
            timerText.text = minutes + ":" + seconds;
        }
        if (timerText.text == "01:60")
            timerText.text = "02:00";
        if (time < 0)
        {
            timerText.text = "00:00";
        }


    }
}
