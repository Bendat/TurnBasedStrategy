using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {

    TurnManagerUI _tmUI;

    /// <summary>
    /// The Current Turn Number
    /// </summary>
    public int TurnNumber = 0;

    /// <summary>
    /// A list of all players in the game
    /// </summary>
    public List<Player> Players;

    /// <summary>
    /// The list index of the current player
    /// </summary>
    public int CurrentPlayer = 0;

    void Start()
    {
        SetUpPlayers();

        _tmUI = GetComponent<TurnManagerUI>();

        TurnStart();
    }

    /// <summary>
    /// Called on the start of a players turn
    /// </summary>
    public void TurnStart()
    {
        // Iterate the turn if it's back to the first player
        if(CurrentPlayer == 0)
        {
            TurnNumber ++;
        }

        Players[CurrentPlayer].SetPlayersTurn(true);

        Players[CurrentPlayer].Refresh();

        //Update GUI
        _tmUI.UpdateTurnNumber(TurnNumber);
        _tmUI.UpdateCurrentPlayerText(Players[CurrentPlayer].PlayerName);
    }

    /// <summary>
    /// Ends the current players turn
    /// </summary>
    public void EndTurn()
    {
        Players[CurrentPlayer].SetPlayersTurn(false);

        CurrentPlayer++;
        if (CurrentPlayer >= Players.Count) CurrentPlayer = 0;
        TurnStart();
    }

    void SetUpPlayers()
    {
        foreach(Player p in Players)
        {
            p.SetPlayersTurn(false);
        }
    }    
}
