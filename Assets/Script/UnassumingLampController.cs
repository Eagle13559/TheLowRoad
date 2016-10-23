﻿using UnityEngine;
using System.Collections;
using Prime31;

public class UnassumingLampController : MonoBehaviour
{
    public GameObject player;
    private AnimationController2D _animator;
    private CharacterController2D _controller;

    public Vector3 endPosition = Vector3.zero;
    public float walkSpeed;
    public float runSpeed;
    public float gravity;
    public float rovingPauseTime;
    public float playerDetectionDistance;

    private float timer = 0;
    private Vector3 startPostion = Vector3.zero;
    private bool going = true;
    private bool seesPlayer = false;
    private bool isFacingRight = true;

    // Use this for initialization
    void Start()
    {
        _animator = gameObject.GetComponent<AnimationController2D>();
        _controller = gameObject.GetComponent<CharacterController2D>();

        startPostion = this.gameObject.transform.position;
        endPosition = endPosition + startPostion;

        //float distance = Vector3.Distance(startPostion, endPosition);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = _controller.velocity;
        Vector2 playerPosition = player.GetComponent<Transform>().position;

        velocity.x = 0;

        // Checks the surrounding area for a nearby player, and ensures that the lamp is facing the right 
        //  way to detect them
        if ((((playerPosition.x < this.transform.position.x + playerDetectionDistance) && (playerPosition.x > this.transform.position.x)) && isFacingRight) ||
            (((playerPosition.x > this.transform.position.x - playerDetectionDistance) && (playerPosition.x < this.transform.position.x)) && !isFacingRight))
        {
            seesPlayer = true;
        }
        else seesPlayer = false;

        if (!seesPlayer)
        {
            // If the lamp has reached it's end position, pause before continuing
            if ((((this.transform.position.x > endPosition.x) && isFacingRight) || ((this.transform.position.x < startPostion.x) && !isFacingRight)) && going)
            {
                going = false;                
            }

            // Wait for a brief moment before continuing
            if (!going)
            {
               // _animator.setAnimation("EnemyIdle");
                timer += Time.deltaTime * walkSpeed;
                if (timer > rovingPauseTime)
                {
                    timer = 0;
                    going = true;
                    isFacingRight = !isFacingRight;
                }
            }

            // If the lamp is moving, move in the direction it's facing
            else if (isFacingRight)
            {
                _animator.setFacing("Right");
                //_animator.setAnimation("EnemyWalk");
                velocity.x = walkSpeed;
            }

            else
            {
                _animator.setFacing("Left");
                //_animator.setAnimation("EnemyWalk");
                velocity.x = walkSpeed * -1;
            }
        }

        // If the lamp has detected a player
        else
        {
            // Determine which direction to run toward the player
			if (playerPosition.x < this.transform.position.x) {
				velocity.x = runSpeed * -1;
				_animator.setFacing ("Left");
			} else {
				velocity.x = runSpeed;
				_animator.setFacing ("Right");
			}
        }

        // Always apply gravity
		if(!_controller.isGrounded)velocity.y += gravity * Time.deltaTime;

        // Execute the move
        _controller.move(velocity * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, endPosition + this.transform.position);
        Gizmos.color = Color.blue;
        Vector3 startViewDistance = this.transform.position;
        startViewDistance.y += 0.2f;
        Vector3 endViewDistance = startViewDistance;
        endViewDistance.x += playerDetectionDistance;
        Gizmos.DrawLine(startViewDistance, endViewDistance);
    }
}
