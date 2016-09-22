using UnityEngine;
using System.Collections;

public class CutOff : MonoBehaviour {
	bool disable = false; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider other){
	

		if (other.CompareTag("Player") && GetComponentInChildren<DoorOpen>().opened == true){

			print ("Door: closeTriggered");
			GetComponentInChildren<DoorOpen>().Close(); 
			disable = true;
		
	
		}


		if (other.CompareTag("Player")){
		gameObject.GetComponent<BoxCollider>().isTrigger = false;


		}
	}

}
