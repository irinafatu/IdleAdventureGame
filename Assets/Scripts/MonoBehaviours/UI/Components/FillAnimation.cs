using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAnimation : MonoBehaviour
{
    Image timerBar;
    double maxTime;
    double currentTime = -1;
    bool isAuto = false;
    Action onAnimComplete = null;
    // Start is called before the first frame update
  
    public void Init(Image timerImage, double pMaxTime, Action pOnAnimComplete)
    {
        timerBar = timerImage;
        maxTime = pMaxTime;
        currentTime = -1;
        onAnimComplete = pOnAnimComplete;
     
    }
    public void SetAutoOn()
    {
        isAuto = true;
    }

    public void StartAnim()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= 0)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= maxTime)
            {
                currentTime = isAuto ? 0 :- 1;
              //  if (isAuto == false)
                //    timerBar.fillAmount = 0;
                if (onAnimComplete != null)
                    onAnimComplete();
            }
            timerBar.fillAmount = (float)(currentTime / maxTime) < 0 ? 0 : (float)(currentTime / maxTime);
        } 
       
    }
}
