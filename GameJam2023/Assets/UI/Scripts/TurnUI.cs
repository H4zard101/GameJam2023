using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnUI : MonoBehaviour
{
    public TMP_Text turnAmount;

    public void UpdateTurn(int turn_num)
    {
        turnAmount.text = "ROUND " + turn_num.ToString();
    }
}
