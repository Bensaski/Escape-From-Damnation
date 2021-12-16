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

    public void ChangeVol(float newVolume)
    {
        Volume = AudioListener.volume;
        Volume = newVolume;
        AudioListener.volume = Volume;
    }
}
