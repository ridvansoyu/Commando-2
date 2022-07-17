using UnityEngine;
using System.Collections;

public class AimScript : MonoBehaviour
{
    private Ray ray;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        mouseWorldPosition.x = mouseWorldPosition.x - transform.position.x;
        mouseWorldPosition.y = mouseWorldPosition.y - transform.position.y;
        Debug.DrawRay(gameObject.transform.position, mouseWorldPosition, Color.red);

        float m = mouseWorldPosition.y / mouseWorldPosition.x;
        float angle = Mathf.Atan(m) * Mathf.Rad2Deg;

        if (angle >= -90 || angle <= 90)
            transform.rotation = Quaternion.Euler(0, 0, angle);
        if(mouseWorldPosition.x < 0)
            transform.rotation = Quaternion.Euler(0, -180, -(angle));


        //transform.position = new Vector3(transform.position.x - (Input.GetAxis("Mouse Y") / 100), transform.position.y, transform.position.z);

        //Debug.Log(m.ToString());
        //Debug.Log(mouseWorldPosition.ToString());
        //Debug.Log(angle.ToString());
    }

}