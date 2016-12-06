using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {

    //public Light light;
    public Sprite onState;
    public GameObject platformToMoveOnTrigger;
    public GameObject platformToMoveOnTrigger2;

    //private bool isTriggered = false;
    //private bool isAlive = true;
    //private SpriteRenderer objectToDissolveOnTriggerSprite;
    private MovingPlatform platform;
    private MovingPlatform platform2 = null;

    // Use this for initialization
    void Start () {
        //objectToDissolveOnTriggerSprite = objectToDissolveOnTrigger.GetComponent<SpriteRenderer>();
        platform = platformToMoveOnTrigger.GetComponent<MovingPlatform>();
        platform.moving = false;
        platform2 = platformToMoveOnTrigger2.GetComponent<MovingPlatform>();
        platform2.moving = false;
    }
	
	// Update is called once per frame
	void Update () {
        //if (isTriggered)
        //{
            //Color spriteColor = objectToDissolveOnTriggerSprite.color;
            //spriteColor.a -= 1.0f * Time.deltaTime;
            //objectToDissolveOnTriggerSprite.color = spriteColor;
            //if (spriteColor.a <= 0)
            //{
                //Destroy(objectToDissolveOnTrigger);
                //isAlive = false;
            //}
        //}
	    
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Firefly")
        {
            //light.color = Color.green;
            //isTriggered = true;
            GetComponent<SpriteRenderer>().sprite = onState;
            platform.moving = true;
            platform2.moving = true;
        }
    }
}
