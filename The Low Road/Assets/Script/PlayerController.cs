using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerController : MonoBehaviour {

    public GameObject gameCamera;

    public float walkSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -35f;

    private CharacterController2D _controller;
    //private AnimationController2D _animator;


    // Use this for initialization
    void Start () {
        _controller = gameObject.GetComponent<CharacterController2D>();
        //_animator = gameObject.GetComponent<AnimationController2D>();
        gameCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);

    }

    // Update is called once per frame
    void Update () {
        _controller.move(PlayerInputFunction() * Time.deltaTime);

    }

    private Vector3 PlayerInputFunction ()
    { 
        Vector3 velocity = _controller.velocity;

        if ((_controller.isGrounded) && (_controller.ground != null) && (_controller.ground.tag == "MovingPlatform")) this.transform.parent = _controller.ground.transform;
        else if (this.transform.parent != null) this.transform.parent = null;

        velocity.x = 0;

        if (Input.GetAxis("Horizontal") < 0)
        {
            velocity.x = walkSpeed* -1;
        }

        else if (Input.GetAxis("Horizontal") > 0)
        {
            velocity.x = walkSpeed;
        }

        if (Input.GetAxis("Jump") > 0 && _controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
        }

        velocity.y += gravity* Time.deltaTime;

        return velocity;
    }

}
