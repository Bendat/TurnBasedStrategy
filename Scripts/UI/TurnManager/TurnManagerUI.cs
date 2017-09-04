using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManagerUI : MonoBehaviour {

    public Text TurnNumberText;
    public Text CurrentPlayerText;

    public void UpdateTurnNumber(int turn)
    {
        TurnNumberText.text = "Turn: " + turn;
    }

    public void UpdateCurrentPlayerText(string PlayerName)
    {
        CurrentPlayerText.text = PlayerName;
    }

}
