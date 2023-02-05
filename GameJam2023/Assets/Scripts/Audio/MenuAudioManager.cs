using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MenuAudioManager : MonoBehaviour
{
    public static MenuAudioManager Instance => m_instance;
    private static MenuAudioManager m_instance;
    

    EventInstance musicInstance;

    public AudioReferences references;


    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;
        StartMenuMusic();
    }

    public void StartMenuMusic()
    {
        musicInstance = RuntimeManager.CreateInstance(references.menuMusicEvent);
        musicInstance.start();
    }

    public void StopMenuMusic()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }

    public void OnDestroy()
    {
        StartMenuMusic();
        
    }
}
