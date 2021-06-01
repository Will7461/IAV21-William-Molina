using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;
    /// <summary>
    /// Movement of an UI element to look at the camera
    /// </summary>
	void LateUpdate()
    {
		cam = Camera.main.transform;
		if (cam == null) return;
        transform.LookAt(transform.position + cam.forward);
    }
}
