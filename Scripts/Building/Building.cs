using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour {

    public string BuildingName;

    /// <summary>
    /// The Player who owns this building
    /// </summary>
    public Player Owner;

    public GameObject BuildingPrefab;
    public GameObject UnderConstructionPrefab; 

    /// <summary>
    /// True if this building is under construction
    /// </summary>
    public bool UnderConstruction;

    /// <summary>
    /// The amount of work required on this building for it to be constructed
    /// </summary>
    public int WorkRequired;

    /// <summary>
    /// The amount of work already completed on this building
    /// </summary>
    public int WorkCompleted;



    public abstract void Refresh();
}
