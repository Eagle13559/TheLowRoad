using UnityEngine;
using System.Collections;

public class SpooderController : MonoBehaviour {

    private AnimationController2D _animator;

    public Vector3 endPosition = Vector3.zero;
    public float speed = 1;
    public float rovingPauseTime;

    private float timer = 0;
    private Vector3 startPostion = Vector3.zero;
    private bool outgoing = true;

    // Use this for initialization
    void Start () {
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
	void Update () {

        timer += Time.deltaTime * speed;

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, endPosition + this.transform.position);
    }
}
