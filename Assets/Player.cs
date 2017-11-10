using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Tooltip("in meters per second (ms^-1)")][SerializeField] float xSpeed = 4f;
    [SerializeField] float xRange = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float xPosRaw = transform.localPosition.x + xOffset;
        float xPosClamp = Mathf.Clamp(xPosRaw, -xRange, xRange);
        transform.localPosition = new Vector3(xPosClamp, transform.localPosition.y, transform.localPosition.z);
	}
}
