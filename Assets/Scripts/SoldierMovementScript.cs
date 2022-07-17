using UnityEngine;
using System.Collections;

public class SoldierMovementScript : MonoBehaviour
{

    public Vector2 direction = new Vector2(-1, 0);
    public Vector2 speed = new Vector2(10, 10);
    private Vector2 movement;

    void Update()
    {
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = movement;
    }

}
