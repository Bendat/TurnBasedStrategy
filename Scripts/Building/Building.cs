using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour {

    public string BuildingName;

    public abstract void Refresh();
}
