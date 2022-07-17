using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour
{

    public float speed = 25; 
    public Vector2 direction = new Vector2(-1, 0); 
    public Vector2 movement;


    //public bool move;
    void Start()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPosition.z = 0;
        mouseWorldPosition.x = mouseWorldPosition.x - transform.position.x;
        mouseWorldPosition.y = mouseWorldPosition.y - transform.position.y;

        float m = mouseWorldPosition.y / mouseWorldPosition.x;
        float angle = Mathf.Atan(m) * Mathf.Rad2Deg;

        if (angle >= -90 || angle <= 90)
            transform.rotation = Quaternion.Euler(0, 0, angle);
        if (mouseWorldPosition.x < 0)
            transform.rotation = Quaternion.Euler(0, -180, -(angle));


        //move = Input.GetButtonDown("Horizontal");
        //move |= Input.GetButtonDown("Horizontal");

        //if (move)
        //{
        movement = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y).normalized;
        //}
    }

    void FixedUpdate()
    {
        //if (Input.GetButtonDown("Fire1"))
            //rigidbody2D.velocity = movement;
        //rigidbody2D.AddForce(transform.forward * 10);
        gameObject.rigidbody2D.velocity = movement * speed;
    }
}
