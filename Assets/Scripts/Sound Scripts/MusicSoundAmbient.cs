using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSoundAmbient : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Event;
    void Start()
    {
       FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
