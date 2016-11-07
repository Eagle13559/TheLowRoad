using UnityEngine;
using System.Collections;

public class Swapper : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update () {
        Vector3 temp = Input.mousePosition;
		temp.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.
		this.transform.position = Camera.main.ScreenToWorldPoint(temp);
	}
}
