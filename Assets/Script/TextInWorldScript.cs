using UnityEngine;
using System.Collections;

public class TextInWorldScript : MonoBehaviour {

    private Vector3 inWorldPosition;

	// Use this for initialization
	void Start () {
        inWorldPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.WorldToViewportPoint(inWorldPosition);
        if ( ((pos.x < 1) && (pos.x > 0)) && ((pos.y < 1) && (pos.y > 0)) )
        {
            this.transform.position = pos;
        }
	}
}
