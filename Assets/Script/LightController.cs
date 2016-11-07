using UnityEngine;
using System.Collections;
using Prime31;

public class LightController : MonoBehaviour {

    private CharacterController2D _controller;

    // Use this for initialization
    void Start () {
        _controller = gameObject.GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update() {
        //Vector3 temp = Input.mousePosition;
        //temp.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.
        //this.transform.position = Camera.main.ScreenToWorldPoint(temp);

        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        direction.z = 0;
        // The max move is 1 voxel per frame
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }
        _controller.move(direction);
    }
}
