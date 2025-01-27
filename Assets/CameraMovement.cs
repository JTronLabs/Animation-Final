﻿using UnityEngine;

//commented out code is working on a solution to the camera moving through walls
//		solution relies on collider on camera

public class CameraMovement : MonoBehaviour {
	float rotateSpeed;
	//public float zoomSpeed;
	//private bool cameraZoomIn, cameraZoomOut;
	//private float originalDistanceFromCharacter;

	void Start(){
		rotateSpeed = 80;
		//zoomSpeed *= 1;

		//originalDistanceFromCharacter = (transform.position - transform.parent.position).magnitude;
	}

	void Update() {	
		HandleCameraLookAround (rotateSpeed * Time.deltaTime);

		/*if (cameraZoomIn) {
			ZoomCameraIn();
		} else if (cameraZoomOut) {
			ZoomCameraOut();
		}*/

	}

	//move the camera around by modifying the rotation/position of its parent, an empty object inside of player character
	void HandleCameraLookAround(float speed){
		float movementX = Input.GetAxis ("Mouse X");
		transform.parent.RotateAround (transform.parent.transform.parent.position, Vector3.down, speed * movementX);
		
		float movementY = Input.GetAxis ("Mouse Y");
		transform.parent.Rotate (Vector3.left, speed * movementY);
	}

	/*void OnCollisionEnter(){
		cameraZoomIn = true;
		cameraZoomOut = false;
	}
	void OnCollisionExit(){
		cameraZoomOut = true;
		cameraZoomIn = false;
	}

	private void ZoomCameraIn(){
		transform.position = Vector3.Lerp (transform.position, transform.parent.position, zoomSpeed);

		//modify player alpha if camera is inside the player
		Color dudeColor = transform.parent.transform.parent.renderer.material.color;
		if (transform.position == transform.parent.position) {
			dudeColor.a = .3f;
		} else {
			dudeColor.a = 1f;
		}
		transform.parent.transform.parent.renderer.material.color = dudeColor;
	}
	private void ZoomCameraOut(){
		transform.position = Vector3.Lerp (transform.parent.position, transform.position * originalDistanceFromCharacter, zoomSpeed);

		if( Mathf.Approximately(Vector3.Distance(transform.parent.position, transform.position),originalDistanceFromCharacter) ){
			cameraZoomOut=false;
			cameraZoomIn=false;
		}
	}


	private void wallCollision(Vector3 fromObject, ref Vector3 toTarget)
	{

		RaycastHit hit;
		if(Physics.Linecast(fromObject,toTarget, out hit))
		   {
			toTarget = new Vector3(hit.point.x, toTarget.y, hit.point.z);
		}
		}*/
}
