using UnityEngine;
using System.Collections;
using Prime31;

public class LightController : MonoBehaviour {

    private CharacterController2D _controller;
    private AnimationController2D _animator;
    private BoxCollider2D _collider;

    // Use this for initialization
    void Start () {
        _controller = gameObject.GetComponent<CharacterController2D>();
        _animator = gameObject.GetComponent<AnimationController2D>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
        _animator.setAnimation("firefly_fly");
    }

    // Update is called once per frame
    void Update() {
        Vector2 location;
        location.x = this.transform.position.x;
        location.y = this.transform.position.y;
        Vector3 velocity = _controller.velocity;
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        direction.z = 0;
        // The max move is 1 voxel per frame
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }
        Vector3 viewPos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (viewPos.z > 0 && viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1)
        {
            _controller.move(direction);
        }
        else
        {
            this.transform.position += direction;
        }
    }
}
