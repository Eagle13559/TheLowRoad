using UnityEngine;
using System.Collections;
using Prime31;

public class LightController : MonoBehaviour {

    private CharacterController2D _controller;
    private AnimationController2D _animator;

    // Use this for initialization
    void Start () {
        _controller = gameObject.GetComponent<CharacterController2D>();
        _animator = gameObject.GetComponent<AnimationController2D>();
        _animator.setAnimation("Butthole");
    }

    // Update is called once per frame
    void Update() {
        //Vector3 temp = Input.mousePosition;
        //temp.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.
        //this.transform.position = Camera.main.ScreenToWorldPoint(temp);
        Vector3 velocity = _controller.velocity;
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        direction.z = 0;
        // The max move is 1 voxel per frame
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }
        //if (velocity.magnitude > 1)
        //{
            //velocity = velocity.normalized;
        //}
        //velocity.x *= velocity.x;
        //velocity.y *= velocity.y;        
        //if (velocity.x > 2) velocity.x = 2;
        //else if (velocity.x < -2) velocity.x = -2;
        //if (velocity.y > 2) velocity.y = 2;
        //direction.x += velocity.x;
        _controller.move(direction/* * Time.deltaTime*/);
    }
}
