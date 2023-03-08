using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetStats : MonoBehaviour
{
    public bool main_menu;
    public TMP_Text turn_num;
    public TMP_Text longest_run;

    // Start is called before the first frame update
    void Start()
    {
        if(main_menu == false)
        {
            turn_num.text = PlayerPrefs.GetInt("TurnNumber").ToString();
        }

        if(PlayerPrefs.HasKey("LongestRun") == false)
        {
            PlayerPrefs.SetInt("LongestRun",1);
        }
        longest_run.text = PlayerPrefs.GetInt("LongestRun").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
