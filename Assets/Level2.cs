﻿using UnityEngine;
using System.Collections;

public class Level2 : MonoBehaviour {
	
	Animator animateTheDude;
	bool characterRotating;
	float rotationRate = 2f;
	float roofHeight;
	float groundHeight;
	bool currentlyFlipped = false; //based on original oritenation flipped is flipped from beginning
	
	// Use this for initialization
	void Start () {
		animateTheDude = GetComponent<Animator> ();
		characterRotating = false;
		roofHeight = GameObject.Find ("Roof").transform.position.y; //roof height
		groundHeight = 0; 
	}
	
	// Update is called once per frame
	void Update () {
		//Animation controllers
		float vertical = Input.GetAxis ("Vertical");
		float horizontal = Input.GetAxis ("Horizontal");
		animateTheDude.SetFloat("speed", vertical );
		animateTheDude.SetFloat ("Direction", horizontal, 0.25f, Time.deltaTime);
		if( Input.GetKeyDown(KeyCode.Space) ){
			animateTheDude.SetTrigger ("jump");
		}
		if( Input.GetKeyDown(KeyCode.Alpha1) ){
			animateTheDude.SetTrigger ("die");
		}
		if( Input.GetKeyDown(KeyCode.Alpha2) ){
			animateTheDude.SetTrigger ("revive");
		}
		if( Input.GetKeyDown(KeyCode.Alpha3) ){
			animateTheDude.SetTrigger ("wave");
		}
		
		
		//Gravity controller
		if( !characterRotating && Input.GetKeyDown(KeyCode.G) ) //cannot rotate while in air, only near planes
		{
			//set speed of rotation based on how far away wall is (if possible)
			characterRotating = true;
			Physics.gravity *= -1;
			currentlyFlipped = !currentlyFlipped; //flip this variable
		}
		
		rotate ();
		
		if (checkForFallout ())
			respawn ();
		
		//if character is touching ground then isFlipping = false;
	}
	
	//need a function to smoothly rotate character. Should probably change speed of rotation depending on distance from walls.
	//Must ensure that character has flipped by the time he touches next wall
	//this might be useful http://codereview.stackexchange.com/questions/68217/rotating-a-character-upside-down-and-vice-versa
	private void rotateCharacter(){ 
		//var distance = Vector3.Distance(object1.transform.position, object2.transform.position);//calc distance between 2 objects
		Vector3 rot = this.transform.rotation.eulerAngles; 
		rot = new Vector3(rot.x,rot.y,rot.z+180);
		this.transform.rotation = Quaternion.Euler(rot);
	}
	/*
	private void rotate(){
		float totalRotation = 0;
		while (characterRotating) {
			if(totalRotation >= 180){
				characterRotating = false;
			}else{
				Vector3 rot = this.transform.rotation.eulerAngles;
				transform.Rotate(0, 0, rotationRate * Time.deltaTime);
				totalRotation += rotationRate * Time.deltaTime;
			}
		}

	}*/
	
	private void rotate(){
		if (characterRotating) {
			transform.Rotate(0,0,rotationRate);
			
			Vector3 rot = this.transform.rotation.eulerAngles; 
			//check if character is done rotating
			if(Mathf.Abs(rot.z) < Mathf.Abs(rotationRate)){//rightside up
				//smooth rotation
				rot = new Vector3(rot.x,rot.y,0);
				this.transform.rotation = Quaternion.Euler(rot);
				//signal rotation has ended
				characterRotating = false;
			}else if( Mathf.Abs(rot.z-180) < Mathf.Abs(rotationRate) ){//upside down
				//smooth rotation
				rot = new Vector3(rot.x,rot.y,180);
				this.transform.rotation = Quaternion.Euler(rot);
				//signal rotation has ended
				characterRotating = false;
			}
		}
	}
	
	//helper funciton to check and see if player is closeEnough to a plane (roof or floor)
	//in future iterations, this can include moving platforms and other areas that will be acceptable for rotation
	//true if close enough, false o.w
	private bool isOnPlane(){
		float curYpos = this.transform.position.y;
		if (curYpos + 1 >= roofHeight)
			return true;
		if (curYpos - 1 <= groundHeight)
			return true;
		return false; //otherwise
	}
	
	private bool checkForFallout()
	{
		float curYpos = this.transform.position.y;
		if (curYpos > roofHeight + 5)
			return true;
		return false;
		
	}

	private void respawn()
	{
		//reset everything
		if(currentlyFlipped)
			Physics.gravity*=-1; //flip
		currentlyFlipped = !currentlyFlipped; //reset flip bool

		Application.LoadLevel (Application.loadedLevelName); //reload level
	}
	
	
}