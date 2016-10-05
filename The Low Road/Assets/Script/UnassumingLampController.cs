using UnityEngine;
using System.Collections;

public class UnassumingLampController : MonoBehaviour
{
    public GameObject player;
    private AnimationController2D _animator;

    public Vector3 endPosition = Vector3.zero;
    public float speed = 1;
    public float rovingPauseTime;
    public float playerDetectionDistance;

    private float timer = 0;
    private Vector3 startPostion = Vector3.zero;
    private bool outgoing = true;
    private bool seesPlayer = false;
    private bool isFacingRight = true;

    // Use this for initialization
    void Start()
    {
        _animator = gameObject.GetComponent<AnimationController2D>();

        startPostion = this.gameObject.transform.position;
        endPosition = endPosition + startPostion;

        float distance = Vector3.Distance(startPostion, endPosition);
        if (distance != 0)
        {
            speed = speed / distance;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (((player.GetComponent<Transform>().position.x < this.transform.position.x + playerDetectionDistance) && isFacingRight) ||
            ((player.GetComponent<Transform>().position.x > this.transform.position.x + playerDetectionDistance) && !isFacingRight))
        {
            seesPlayer = true;
        }
        else seesPlayer = false;

        timer += Time.deltaTime * speed;

        if (!seesPlayer)
        {
            if (timer > rovingPauseTime + 1)
            {
                outgoing = !outgoing;
                timer = 0;
            }

            else if (timer < 1)
            {
                if (outgoing)
                {
                    //_animator.setFacing("Right");
                    //_animator.setAnimation("EnemyWalk");
                    this.transform.position = Vector3.Lerp(startPostion, endPosition, timer);
                }
                else
                {
                    //_animator.setFacing("Left");
                    //_animator.setAnimation("EnemyWalk");
                    this.transform.position = Vector3.Lerp(endPosition, startPostion, timer);
                }
            }

            //else _animator.setAnimation("EnemyIdle");
        }

        else
        {
            Vector3 playerPos = player.GetComponent<Transform>().position;
            // We don't want the sprite to move vertically, just horizontally.
            playerPos.y = this.transform.position.y;
            this.transform.position = Vector3.Lerp(this.transform.position, playerPos, timer);
        }

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
