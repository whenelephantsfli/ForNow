using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerEnter(Collider other){

		if (other.CompareTag("Player")){
			print ("Door: openTriggered");
		GetComponentInChildren<DoorOpen>().Open(); 

		}
	} 

	void OnTriggerExit(Collider other){

		if (other.CompareTag("Player") && GetComponentInChildren<DoorOpen>().opened == true){

			print ("Door: closeTriggered");
			GetComponentInChildren<DoorOpen>().Close(); 

		}

		}

}
