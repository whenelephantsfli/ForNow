using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

	public bool opened = false; 

	AudioSource audio;

	//AudioSource audio; 
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		 
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Open(){
		print (" Door: Opened"); 
		transform.Rotate(0,-107,0);
		opened = true;


	}

	public void Close(){

		print (" Door: Closed"); 

		transform.Rotate(0,107,0); 
		opened = false;

		audio.Play();
	}


}
