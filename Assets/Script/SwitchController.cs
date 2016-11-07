using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {

    public Light light;
    public GameObject objectToDissolveOnTrigger;

    private bool isTriggered = false;
    private bool isAlive = true;
    private SpriteRenderer objectToDissolveOnTriggerSprite;

	// Use this for initialization
	void Start () {
        objectToDissolveOnTriggerSprite = objectToDissolveOnTrigger.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isAlive && isTriggered)
        {
            Color spriteColor = objectToDissolveOnTriggerSprite.color;
            spriteColor.a -= 1.0f * Time.deltaTime;
            objectToDissolveOnTriggerSprite.color = spriteColor;
            if (spriteColor.a <= 0)
            {
                Destroy(objectToDissolveOnTrigger);
                isAlive = false;
            }
        }
	    
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Firefly")
        {
            light.color = Color.green;
            isTriggered = true;
        }
    }
}
