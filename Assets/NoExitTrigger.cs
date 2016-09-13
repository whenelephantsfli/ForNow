using UnityEngine;
using System.Collections;

public class NoExitTrigger : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider other){

		if (other.CompareTag("Player")){

			//other.isTrigger = false; 

			gameObject.GetComponent<BoxCollider>().isTrigger = false;



		}
	}
}
