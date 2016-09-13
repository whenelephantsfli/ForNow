using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {

public int RotateSpeed;

	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(Vector3.up * RotateSpeed * Time.fixedDeltaTime);
	
	}
}
