using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float speed = 1.0f;
    private Animator animator;

    public Transform groundCheck;
    public bool grounded = false;

    public bool jump = false;
    public float jumpForce = 10.0f;


    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            InvokeRepeating("Shoot", 0.001f, 0.05f); // Mousa basılı tutulduğu sürece shoot fonksiyonu çağrılacak.

        }
        else if (Input.GetButtonUp("Fire1"))
        {
            CancelInvoke("Shoot"); // Moustan el çekilince iptal edilecek.
            animator.SetBool("Attack", false);
        }
    }

    void FixedUpdate()
    {
        animator.SetBool("Walk", false);
        Movement();

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        Jump();

    }

    void Movement()
    {

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            //rigidbody2D.AddRelativeForce(Vector2.right * 10-rigidbody2D.velocity);
            transform.eulerAngles = new Vector2(0, 0);
            if (grounded)
                animator.SetBool("Walk", true);
            transform.Find("Legs").position = new Vector3(transform.Find("UpperBody/Hand").position.x + 0f, transform.Find("Legs").position.y, transform.Find("Legs").position.z);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
            if (grounded)
                animator.SetBool("Walk", true);
            transform.Find("Legs").position = new Vector3(transform.Find("UpperBody/Hand").position.x - 0.2f, transform.Find("Legs").position.y, transform.Find("Legs").position.z);

        }

    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.W) && grounded)
        {
            //animator.SetBool("Walk", false);
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            if (grounded)
                animator.SetTrigger("Jump");

        }
    }

    void Shoot()
    {
        animator.SetBool("Attack", true);
    }
}
