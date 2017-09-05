using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test Class
/// </summary>
public class CubeBuilding : Building {

    void Start()
    {
        if (UnderConstruction)
        {
            UnderConstructionPrefab = Instantiate(UnderConstructionPrefab, transform.position, transform.rotation, transform);
        }
        else
        {
            BuildingPrefab = Instantiate(BuildingPrefab, transform.position, transform.rotation, transform);
        }
    }

    public override void Refresh()
    {
        Debug.Log("Refresh() Called on " + transform.name);

        if (!UnderConstruction)
        {
            
        }
        else
        {
            //Building currently being constructed
            WorkCompleted += Owner.WorkPerTurn;
            if(WorkCompleted >= WorkRequired)
            {
                Destroy(UnderConstructionPrefab);
                Instantiate(BuildingPrefab, transform.position, transform.rotation, transform);
            }

        }
    }


}
