using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance => m_Instance;
    private static AudioManager m_Instance;

    public AudioReferences references;
    // Start is called before the first frame update
    void Start()
    {
        m_Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
