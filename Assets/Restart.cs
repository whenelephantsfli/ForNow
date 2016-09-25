 	using UnityEngine;
using System.Collections;
//using UnityEditor.SceneManagement;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit (Collider other) {

		if (other.CompareTag("Player")){
			print ("RESTART");
			Application.LoadLevel(1); } 
		//	Application.LoadLevel(Application.loadedLevel);

		}

}



