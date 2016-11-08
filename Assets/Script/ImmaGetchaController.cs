using UnityEngine;
using System.Collections;
using Prime31;

public class ImmaGetchaController : MonoBehaviour {

    private GameObject _player;
    private AnimationController2D _animator;
    private CharacterController2D _controller;

    public float runSpeed;
    public float despawnTime;
    public AudioSource monsterAudio;
    private float gravity = -35.0f;
    private float timer = 0;

    // Use this for initialization
    void Start () {
        _player = GameObject.FindWithTag("Player");
        _animator = gameObject.GetComponent<AnimationController2D>();
        _controller = gameObject.GetComponent<CharacterController2D>();
        monsterAudio.Play();
    }
	
	// Update is called once per frame
	void Update () {
        
        timer += Time.deltaTime;
        if (timer > despawnTime || (!_player.GetComponent<PlayerController>().getIsAlive()))
        {
            Destroy(this.gameObject);            
        }
        Vector3 velocity = _controller.velocity;
        Vector2 playerPosition = _player.GetComponent<Transform>().position;

        velocity.x = 0;

        if (_controller.isGrounded)
        {
            if (playerPosition.x < this.transform.position.x - 1)
            {
                velocity.x = runSpeed * -1;               
                _animator.setFacing("Right");
                _animator.setAnimation("Spooder_Walk");
            }
            else if (playerPosition.x > this.transform.position.x + 1)
            {
                velocity.x = runSpeed;
                _animator.setFacing("Left");
                _animator.setAnimation("Spooder_Walk");
            }
            else _animator.setAnimation("Spooder_Idle");
        }

        // Apply gravity
        else velocity.y += gravity * Time.deltaTime;

        // Execute the move
        _controller.move(velocity * Time.deltaTime);
    }
}
