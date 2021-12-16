using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeScript : MonoBehaviour
{
    float Volume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //I obtained how to do the below method from https://forum.unity.com/threads/how-to-change-a-audiolistener-volume-with-slider-ui.457530/
    public void ChangeVol(float newVolume)
    {
        Volume = AudioListener.volume;
        Volume = newVolume;
        AudioListener.volume = Volume;
    }
}
