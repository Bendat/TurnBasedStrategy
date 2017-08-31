using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float AccelerationTime, ScrollSpeed, timer;

    [Range(0f, 1f)]
    public float CurrentSpeedMultiplier;

    public bool HorizontalPressed, VerticalPressed;

    void Start()
    {
        CurrentSpeedMultiplier = 0f;
        HorizontalPressed = false;
        VerticalPressed = false;
        timer = 0f;
    }

    void Update()
    {
        CheckMovementInputs();
    }

    /// <summary>
    /// Deals with horizontal and forward movement of camera
    /// </summary>
    void CheckMovementInputs()
    {
        if (Input.GetButton("Horizontal"))
        {
            HorizontalPressed = true;
            transform.Translate(CurrentSpeedMultiplier * Input.GetAxis("Horizontal") * ScrollSpeed * Time.deltaTime, 0, 0, Space.World);
            if (CurrentSpeedMultiplier == 0f)
            {
                StartCoroutine(ScrollAcceleration());
            }
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            HorizontalPressed = false;
            ResetSpeedMultiplier();
        }

        if (Input.GetButton("Vertical"))
        {
            VerticalPressed = true;
            transform.Translate(0, 0, CurrentSpeedMultiplier * Input.GetAxis("Vertical") * ScrollSpeed * Time.deltaTime, Space.World);
            if (CurrentSpeedMultiplier == 0f)
            {
                StartCoroutine(ScrollAcceleration());
            }
        }

        if (Input.GetButtonUp("Vertical"))
        {
            VerticalPressed = false;
            ResetSpeedMultiplier();
        }
    }

    /// <summary>
    /// If neither scroll button is pressed speed multiplier reset to 0
    /// </summary>
    void ResetSpeedMultiplier()
    {
        if (!VerticalPressed && !HorizontalPressed)
        {
            CurrentSpeedMultiplier = 0f;
        }
    }

    /// <summary>
    /// Accelerates the camera to max speed over AccelerationTime
    /// </summary>
    /// <returns></returns>
    IEnumerator ScrollAcceleration()
    {
        timer = 0f;
        while(timer < AccelerationTime && (HorizontalPressed || VerticalPressed))
        {
            timer += Time.deltaTime;
            CurrentSpeedMultiplier = Mathf.Lerp(0, 1, timer / AccelerationTime);
            yield return null;
        }
    }
}
