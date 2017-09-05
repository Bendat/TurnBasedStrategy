using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main class for each player
/// </summary>
public class Player : MonoBehaviour {

    public string PlayerName;

    /// <summary>
    /// If true it is this players turn
    /// </summary>
    bool _isTurn;

    /// <summary>
    /// If true this player is an AI player
    /// </summary>
    bool _isAI;

    public List<Building> Buildings;
    public List<Unit> Units;

    public int WorkPerTurn;

    /// <summary>
    /// Used to activate or deactivate this player
    /// </summary>
    /// <param name="isTurn"></param>
    public void SetPlayersTurn(bool isTurn)
    {
        _isTurn = isTurn;
    }

    /// <summary>
    /// calls the refresh abstract method of all buildings and units
    /// </summary>
    public void Refresh()
    {
        foreach(Unit unit in Units)
        {
            unit.Refresh();
        }
        foreach(Building building in Buildings)
        {
            building.Refresh();
        }
    }

}
