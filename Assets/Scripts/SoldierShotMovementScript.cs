using UnityEngine;
using System.Collections;

public class SoldierShotMovementScript : MonoBehaviour {
    public GameObject Komando;
    public float speed = 25;
    public Vector2 direction = new Vector2(-1, 0); // Vektörün yönü, Ana karakter ve onun mermileri için 1; düşmanlar ve düşmanların mermileri için -1 olmalıdır.
    public Vector2 movement; // Yönü ve hızı belli olan vektörü harekete geçirecek değişken.

    void Start()
    {
        Komando = GameObject.Find("Komando");
        Vector3 KomandoPosition = Komando.transform.position;

        KomandoPosition.z = 0;
        KomandoPosition.x = KomandoPosition.x - transform.position.x;
        KomandoPosition.y = KomandoPosition.y - transform.position.y;

        float m = KomandoPosition.y / KomandoPosition.x;
        float angle = Mathf.Atan(m) * Mathf.Rad2Deg;

        if (angle >= -90 || angle <= 90)
            transform.rotation = Quaternion.Euler(0, 0, angle);
        if (KomandoPosition.x < 0)
            transform.rotation = Quaternion.Euler(0, -180, -(angle));


        movement = new Vector2(Komando.transform.position.x-transform.position.x, Komando.transform.position.y-transform.position.y).normalized;
        //Debug.Log(movement.ToString());
    }

    void FixedUpdate()
    {
        gameObject.rigidbody2D.velocity = movement * speed;
    }
}
