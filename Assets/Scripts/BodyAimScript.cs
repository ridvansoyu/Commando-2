using UnityEngine;
using System.Collections;

public class BodyAimScript : MonoBehaviour
{
    private Transform body;
    private Transform head;

    private Ray ray2;

    // Use this for initialization
    void Start()
    {
        body = transform.Find("Body");
        head = transform.Find("Head");

    }

    // Update is called once per frame
    void Update()
    {
        ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        mouseWorldPosition.x = mouseWorldPosition.x - transform.position.x;
        mouseWorldPosition.y = mouseWorldPosition.y - transform.position.y;
        Debug.DrawRay(gameObject.transform.position, mouseWorldPosition, Color.red);

        float m = mouseWorldPosition.y / mouseWorldPosition.x;
        float angle = Mathf.Atan(m) * Mathf.Rad2Deg;

        if (angle >= -90 || angle <= 90)
        {
            transform.rotation = Quaternion.Euler(0, 0, (angle));
            head.transform.rotation = Quaternion.Euler(0, -180, -angle);
            body.transform.rotation = Quaternion.Euler(0, -180, -angle / 5);
            body.transform.position = new Vector3(transform.position.x + 0.4f/* + 0.31f*/, transform.position.y - 0.29f,0);
            head.transform.position = new Vector3(head.transform.position.x, head.transform.position.y, head.transform.position.z);

        }
        if (mouseWorldPosition.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, -(angle));
            head.transform.rotation = Quaternion.Euler(0, 0, angle);
            body.transform.rotation = Quaternion.Euler(0, 0, angle / 5);
            body.transform.position = new Vector3(transform.position.x - 0.575f/* - 0.65f*/, (transform.position.y - 0.29f), 0);
            head.transform.position = new Vector3(head.transform.position.x, head.transform.position.y, head.transform.position.z);

        }

        //transform.position = new Vector3(transform.position.x - (Input.GetAxis("Mouse Y") / 100), transform.position.y, transform.position.z);

        //Debug.Log(m.ToString());
        //Debug.Log(mouseWorldPosition.ToString());
        //Debug.Log(angle.ToString());
    }

}