using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Tooltip("in meters per second (ms^-1)")][SerializeField] float speed = 20f;
    [SerializeField] float xRange = 5.5f;
    [SerializeField] float yRange = 3f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 3f;
    [SerializeField] float controlPitchFaction = -20f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow;
    float yThrow;

    string eric = "Eric";
    string fatima = "Fatima";

    // Use this for initialization
    void Start () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		print("TRIGGER");
	}

	// Update is called once per frame
	void Update()
    {
        ProcessTranslation();
        ProcessRotation();
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

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float xPosRaw = transform.localPosition.x + xOffset;
        float xPosClamp = Mathf.Clamp(xPosRaw, -xRange, xRange);

        float yPosRaw = transform.localPosition.y + yOffset;
        float yPosClamp = Mathf.Clamp(yPosRaw, -yRange, yRange);

        transform.localPosition = new Vector3(xPosClamp, yPosClamp, transform.localPosition.z);
    }
}
