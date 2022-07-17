using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{
    public Animator destroyAnimator;
    public float destroyTime = 2;
    public int hp = 1;

    //public AudioClip deathSound;

    // Enemy or player?
    public bool isEnemy = true;

    void Start()
    {

        destroyAnimator = GetComponent<Animator>();
    }

    public void Damage(int damageCount)
    {
        // Her hasarda hp yi o kadar azalt.
        hp -= damageCount;

        // Hp 0 veya altına inince player ya da enemy ölür
        if (hp <= 0)
        {
            destroyAnimator.SetBool("Destroyed", true);
            
            //AudioSource.PlayClipAtPoint(deathSound, transform.position); 
            Destroy(gameObject, destroyTime);
            //Instantiate(PointObjects); 
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();

        if (shot != null)
        {
            // Avoid friendly fire: Eğer bu şut bir enemy den geldiyse ve şuta maruz kalan da enemy ise if in içine girmez. Değilse player düşmana atak yapmış demektir. if içine girer.Damage verir, sonra obje silinir.
            if (shot.isEnemyShot != isEnemy)
            {

                Damage(shot.damage);
                Destroy(shot.gameObject);
            }
        }
    }
}
