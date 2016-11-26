using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public Vector3 endPosition = Vector3.zero;
    public float speed = 1;

    private float timer = 0;
    private Vector3 startPostion = Vector3.zero;
    private bool outgoing = true;
    public bool moving = true;
    public bool continuous = true;

	// Use this for initialization
	void Start () {
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
        if (!moving) return;
        timer += Time.deltaTime * speed;
        if (outgoing) this.transform.position = Vector3.Lerp(startPostion, endPosition, timer);            
        else this.transform.position = Vector3.Lerp(endPosition, startPostion, timer);

        if (timer > 1)
        {
            if (!continuous) moving = false;
            outgoing = !outgoing;
            timer = 0;
        }

    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, endPosition + this.transform.position);
    }
}
