using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float scale = 4f;


	void Update(){
		var cam = GetComponent<Camera> ();
		cam.orthographicSize = scale;
	}

	// Use this for initialization
	
	// Update is called once per frame

}
