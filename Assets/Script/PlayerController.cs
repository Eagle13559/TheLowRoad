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
    public AudioSource center;
    public AudioClip monsterGrowlFar;
    public AudioClip monsterGrowlClose;
    public AudioClip monsterRoar;
    public AudioClip death;

    public GameObject gameOverPanel;
    public GameObject levelWinPanel;

    public bool spawnMonster = true;
    public Component label;
    private string Text;

    private CharacterController2D _controller;
    private AnimationController2D _animator;

    private bool playerControl = true;
    private float timer = 0;
    private bool isFacingRight = true;
    private bool isAlive = true;
    private bool playerAtCheckpoint = false;

    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController2D>();
        _animator = gameObject.GetComponent<AnimationController2D>();

        gameCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);
        //UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        if (!playerControl) return;
        _controller.move(PlayerInputFunction() * Time.deltaTime);

        if (!spawnMonster) return;

        if (timer > 20)
        {
            //GameObject monster = Instantiate(stalkingMonster) as GameObject;
            //if (isFacingRight) monster.transform.position = (new Vector3(this.transform.position.x - 5, this.transform.position.y + 10, this.transform.position.z));
            //else monster.transform.position = (new Vector3(this.transform.position.x + 5, this.transform.position.y + 10, this.transform.position.z));
            center.PlayOneShot(monsterRoar, 1f);
            //monsterAudioRight.PlayOneShot(monsterRoar, 1f);
            Text = "'He' has claimed you. Don't stand still for too long.\nRemember you are safe in light.";
            PlayerDeath();
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
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "KillZ")
        {
            Text = "You fell.\nRemember to use your light to check for pitfalls.";
            gameCamera.GetComponent<CameraFollow2D>().stopCameraFollow();
            PlayerDeath();
        }
        else if (col.tag == "EvilDoer")
        {
            Text = "You ran into an enemy.\nUse your light to check what's in front of you.";
            PlayerDeath();
        }
        else if (col.tag == "Checkpoint")
        {
            playerRespawnCoordinate = this.transform.position;
            playerRespawnCoordinate.x += 0.4f;
            playerAtCheckpoint = true;
        }
        else if (col.tag == "LevelWin")
        {
            levelWinPanel.SetActive(true);
            UnityEngine.Cursor.visible = true;
            playerControl = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Checkpoint")
        {
            playerAtCheckpoint = false;
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
            if ((playerControl) && (!playerAtCheckpoint)) timer += Time.deltaTime;
            _animator.setAnimation("Player_Idle");
        }

        if (Input.GetAxis("Jump") > 0 && _controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            _animator.setAnimation("Kid_Jump_p");
        }
        
        velocity.y += gravity * Time.deltaTime;

        return velocity;
    }

    private void PlayerDeath()
    {
        center.PlayOneShot(death, 1.0f);
        label.GetComponent<Text>().text = Text;
        playerControl = false;
        //_animator.setAnimation("DeathAnimation");
        gameOverPanel.SetActive(true);
        UnityEngine.Cursor.visible = true;
        isAlive = false;
    }

    public void PlayerRespawn()
    {
        playerControl = true;
        isAlive = true;
        gameOverPanel.SetActive(false);
        UnityEngine.Cursor.visible = false;
        gameCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);
        _controller.transform.position = playerRespawnCoordinate;
    }

    public bool getIsAlive()
    {
        return isAlive;
    }
}