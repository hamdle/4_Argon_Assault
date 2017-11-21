using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	// todo work-out speed up after reset screen

	[Header("General")]
    [Tooltip("in meters per second (ms^-1)")][SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 3f;

	[Header("Screen-position")]
    [SerializeField] float positionPitchFactor = -5f;
	[SerializeField] float positionYawFactor = 5f;

	[Header("Control-throw")]
	[SerializeField] float controlRollFactor = -20f;
	[SerializeField] float controlPitchFaction = -20f;

	float xThrow;
    float yThrow;
	bool isControlEnabled = true;

	// Update is called once per frame
	void Update()
    {
		if (isControlEnabled)
		{
			ProcessTranslation();
			ProcessRotation();
		}
    }

    private void ProcessRotation()
    {
        // Pitch based on location on the screen + add extra pitch if button is being pressed
        // pitch - coupled with Position on Screen and Control Throw
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFaction;    // x
        // yaw - coupled with Position on Screen
        float yaw = transform.localPosition.x * positionYawFactor;
        // roll - coupled with Control Throw
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float xPosRaw = transform.localPosition.x + xOffset;
        float xPosClamp = Mathf.Clamp(xPosRaw, -xRange, xRange);

        float yPosRaw = transform.localPosition.y + yOffset;
        float yPosClamp = Mathf.Clamp(yPosRaw, -yRange, yRange);

        transform.localPosition = new Vector3(xPosClamp, yPosClamp, transform.localPosition.z);
    }

	void OnPlayerDeath()
	{
		isControlEnabled = false;
	}
}
