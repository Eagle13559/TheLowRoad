using UnityEngine;
using System.Collections;
using Prime31;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public GameObject gameCamera;
    public Vector3 playerRespawnCoordinate;
    //public GameObject healthBar;
    //public GameObject gameOverPanel;
    //public GameObject winPanel;

    public float walkSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -35f;

    private CharacterController2D _controller;
    private AnimationController2D _animator;

    private bool playerControl = true;

    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController2D>();
        _animator = gameObject.GetComponent<AnimationController2D>();

        gameCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);
    }

    void Update()
    {
        if (!playerControl) return;
        _controller.move(PlayerInputFunction() * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "KillZ")
        {
            PlayerDeath();
        }
        else if (col.tag == "EvilDoer")
        {
            PlayerDeath();
        }
        else if (col.tag == "Checkpoint")
        {
            playerRespawnCoordinate = this.transform.position;
        }
    }

    private Vector3 PlayerInputFunction()
    {
        Vector3 velocity = _controller.velocity;

        if ((_controller.isGrounded) && (_controller.ground != null) && (_controller.ground.tag == "MovingPlatform")) this.transform.parent = _controller.ground.transform;
        else if (this.transform.parent != null) this.transform.parent = null;

        velocity.x = 0;

        if (Input.GetAxis("Horizontal") < 0)
        {
            velocity.x = walkSpeed * -1;
            //if (_controller.isGrounded) _animator.setAnimation("RunAnimation");
            //_animator.setFacing("Left");
        }

        else if (Input.GetAxis("Horizontal") > 0)
        {
            velocity.x = walkSpeed;
            //if (_controller.isGrounded) _animator.setAnimation("RunAnimation");
            //_animator.setFacing("Right");
        }

        else
        {
            //_animator.setAnimation("IdleAnimation");
        }

        if (Input.GetAxis("Jump") > 0 && _controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            //_animator.setAnimation("JumpAnimation");
        }

        velocity.y += gravity * Time.deltaTime;

        return velocity;
    }

    private void PlayerDeath()
    {
        // THIS METHOD IS WHERE SOME KIND OF GAME OVER MENU SHOULD BE CALLED AND THE DEATH ANIMATION WILL PLAY
        //playerControl = false;
        //_animator.setAnimation("DeathAnimation");
        //gameOverPanel.SetActive(true);
        PlayerRespawn();
    }

    private void PlayerRespawn()
    {
        _controller.transform.position = playerRespawnCoordinate;
    }
}