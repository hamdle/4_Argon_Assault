using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Tooltip("in meters per second (ms^-1)")][SerializeField] float speed = 20f;
    [SerializeField] float xRange = 5.5f;
    [SerializeField] float yRange = 3f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        transform.localRotation = Quaternion.Euler(-30f, 30f, 0f);
    }

    private void ProcessTranslation()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float xPosRaw = transform.localPosition.x + xOffset;
        float xPosClamp = Mathf.Clamp(xPosRaw, -xRange, xRange);

        float yPosRaw = transform.localPosition.y + yOffset;
        float yPosClamp = Mathf.Clamp(yPosRaw, -yRange, yRange);

        transform.localPosition = new Vector3(xPosClamp, yPosClamp, transform.localPosition.z);
    }
}
