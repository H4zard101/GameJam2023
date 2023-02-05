using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance => m_Instance;
    private static AudioManager m_Instance;
    
    public AudioReferences references;

    public EventInstance musicInstance;
    public EventInstance ambienceInstance;
    // Start is called before the first frame update
    void Start()
    {
        m_Instance = this;
        FmodRouting.SetUpBuses();
        StartGameMusic();
        StartAmbience();
        
    }

    public void StartGameMusic()
    {
        musicInstance = RuntimeManager.CreateInstance(references.gameMusicEvent);
        musicInstance.start();
    }

    public void StopGameMusic()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }

    public void StartAmbience()
    {
        ambienceInstance = RuntimeManager.CreateInstance(references.ambienceEvent);
        ambienceInstance.start();
    }

    public void StopAmbience()
    {
       ambienceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
       ambienceInstance.release();
    }

    public void OnDestroy()
    {
        StartGameMusic();
        StopAmbience();
    }
}
