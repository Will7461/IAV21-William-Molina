using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

	void LateUpdate()
    {
		cam = Camera.main.transform;
		if (cam == null) return;
        transform.LookAt(transform.position + cam.forward);
    }
}
