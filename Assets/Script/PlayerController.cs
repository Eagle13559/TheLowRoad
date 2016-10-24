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
    private float gravity = -35f;

    public GameObject stalkingMonster;
    public AudioSource monsterAudioRight;
    public AudioSource monsterAudioLeft;
    public AudioClip monsterGrowlFar;
    public AudioClip monsterGrowlClose;

    public GameObject gameOverPanel;
    public GameObject levelWinPanel;

    private CharacterController2D _controller;
    private AnimationController2D _animator;

    private bool playerControl = true;
    private float timer = 0;
    private bool isFacingRight = true;
    private bool isAlive = true;

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
            gameCamera.GetComponent<CameraFollow2D>().stopCameraFollow();
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
        else if (col.tag == "LevelWin")
        {
            levelWinPanel.SetActive(true);
            playerControl = false;
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
            if (_controller.isGrounded) _animator.setAnimation("Player_Walk");
            _animator.setFacing("Left");
            isFacingRight = false;
        }

        else if (Input.GetAxis("Horizontal") > 0)
        {
            velocity.x = walkSpeed;
            if (_controller.isGrounded) _animator.setAnimation("Player_Walk");
            _animator.setFacing("Right");
            isFacingRight = true;
        }

        else
        {
            if (playerControl) timer += Time.deltaTime;
            _animator.setAnimation("Player_Idle");
        }

        if (Input.GetAxis("Jump") > 0 && _controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            //_animator.setAnimation("JumpAnimation");
        }
        if (timer > 20)
        {
            GameObject monster = Instantiate(stalkingMonster) as GameObject;
            if (isFacingRight) monster.transform.position = (new Vector3(this.transform.position.x-5, this.transform.position.y + 10, this.transform.position.z));
            else monster.transform.position = (new Vector3(this.transform.position.x+5, this.transform.position.y + 10, this.transform.position.z));
            timer = 0;
        }

        else if ((timer > 14) && (timer < 15))
        {
            if (isFacingRight) monsterAudioLeft.PlayOneShot(monsterGrowlClose, 0.75f);
            else monsterAudioRight.PlayOneShot(monsterGrowlClose, 0.75f);
            timer += 1;
        }

        else if ((timer > 8) && (timer < 9))
        {
            if (isFacingRight) monsterAudioLeft.PlayOneShot(monsterGrowlFar, 0.5f);
            else monsterAudioRight.PlayOneShot(monsterGrowlFar, 0.5f);
            timer += 1;         
        }
        velocity.y += gravity * Time.deltaTime;

        return velocity;
    }

    private void PlayerDeath()
    {
        playerControl = false;
        //_animator.setAnimation("DeathAnimation");
        gameOverPanel.SetActive(true);
        isAlive = false;
    }

    public void PlayerRespawn()
    {
        playerControl = true;
        isAlive = true;
        gameOverPanel.SetActive(false);
        gameCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);
        _controller.transform.position = playerRespawnCoordinate;
    }

    public bool getIsAlive()
    {
        return isAlive;
    }
}