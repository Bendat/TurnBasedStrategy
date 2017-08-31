using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float AccelerationTime = 1f;
    [Range(0f, 1f)]
    public float CurrentSpeedMultiplier = 0f;

    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            transform.Translate(Input.GetAxis("Horizontal"), 0, 0, Space.World);
        }

        if (Input.GetButton("Vertical"))
        {
            transform.Translate(0, 0, Input.GetAxis("Vertical"), Space.World);
        }

    }
}
