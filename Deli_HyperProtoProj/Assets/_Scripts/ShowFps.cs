using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowFps : MonoBehaviour
{

    public float timer, refresh, avgFramerate;
    public string display = "{0} FPS";

    public TextMeshProUGUI text;
    void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if(timer<=0)
            avgFramerate=(int) (1f/timelapse);

        text.text=string.Format(display,avgFramerate.ToString());

        
    }
}
