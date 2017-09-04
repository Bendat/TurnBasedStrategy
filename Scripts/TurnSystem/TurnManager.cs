using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {

    /// <summary>
    /// The Current Turn Number
    /// </summary>
    public int TurnNumber = 1;

    /// <summary>
    /// A list of all players in the game
    /// </summary>
    public List<Player> Players;

    /// <summary>
    /// The player whose turn it is
    /// </summary>
    public Player CurrentPlayer;

    /// <summary>
    /// Called on the start of a players turn
    /// </summary>
    public void TurnStart()
    {

    }

    /// <summary>
    /// Ends the current players turn
    /// </summary>
    public void EndTurn()
    {
        // This should call 
    }

    void Start()
    {
        
    }

    
    
}
