using UnityEngine;
using System.Collections;

public class SoldierProjectileScript : MonoBehaviour
{
    public Transform Komando;
    private SoldierMovementScript soldierMoveScript;
    private Animator animatorr;
    void Start()
    {
        soldierMoveScript = GetComponent<SoldierMovementScript>();
        animatorr = GetComponent<Animator>();
        StartCoroutine(AI());

    }

    IEnumerator AI()
    {
        int a = Random.Range(0,2);
        //Debug.Log("a = " + a.ToString());

        // Shooting
        if (a == 0)
        {
            soldierMoveScript.direction = Vector2.zero;

            animatorr.SetBool("SoldierAttack", true);
            Shot();
            yield return StartCoroutine(Wait(0.3f));
            Shot();
            yield return StartCoroutine(Wait(0.3f));

            //Debug.Log("The routine has started");
            yield return StartCoroutine(Wait(1.0f));
            animatorr.SetBool("SoldierAttack", false);
        }

        // Wait for Walk
        soldierMoveScript.direction = Vector3.Normalize(Komando.transform.position);

        if(Komando.transform.position.x-transform.position.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Komando.transform.position.x - transform.position.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }

        yield return StartCoroutine(Wait(1.0f));

        // Bombing
        if (a == 1)
        {
            soldierMoveScript.direction = Vector2.zero;
            animatorr.SetBool("SoldierStop", true);
            //Debug.Log("1 second passed");
            yield return StartCoroutine(Wait(1.5f));
            animatorr.SetBool("SoldierStop", false);
        }
        //animatorr.SetBool("SoldierWalk", true);

        StartCoroutine(AI());

    }
    IEnumerator Wait(float duration)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            yield return 0;
        }
    }

    void Shot()
    {
        SoldierWeaponScript weapon = GetComponent<SoldierWeaponScript>();
        if (weapon != null)
        {
            // false because the player is not an enemy
            weapon.Attack(false);
        }
    }

}



//using UnityEngine;
//using System.Collections;

///// <summary>
///// Projectile behavior: Bu script genel amaçlı hazırlanıp hem düşman hem de ana karakter mermileri üzerine işlenecek. Şutların yönünü ve ilerleme bilgisi MoveScript altında tutulacak.
///// </summary>

//public class Deneme : MonoBehaviour
//{
//    public Transform Commandoo;

//    public float minAttackCooldownn = 0.5f;
//    public float maxAttackCooldownn = 2f;

//    private float aiCooldownn;


//    private bool isAttackingg;
//    private Vector2 positionTargett;

//    private SoldierMovementScript moveScriptt;
//    private Animator soldierAnimatorr;

//    void Awake()
//    {
//        soldierAnimatorr = GetComponent<Animator>();
//        moveScriptt = GetComponent<SoldierMovementScript>();
//    }

//    void Start()
//    {
//        isAttackingg = false;
//        aiCooldownn = maxAttackCooldownn;


//        StartCoroutine(AI());
//        // USE COROUTINE WITH AICOOLDOWN
//    }

//    IEnumerator AI()
//    {
//        aiCooldownn -= Time.deltaTime;

//        if (aiCooldownn <= 0)
//        {
//            isAttackingg = !isAttackingg;

//            aiCooldownn = Random.RandomRange(minAttackCooldownn, maxAttackCooldownn);
//            positionTargett = Vector2.zero;

//        }

//        // Attack
//        if (isAttackingg)
//        {
//            // Stop any movement
//            moveScriptt.direction = Vector2.zero;

//            // HERE ATTACK CODES......

//            // May be multiple weapon?
//            ////foreach (WeaponScript weapon in weapons)
//            ////{
//            ////    if (weapon != null && weapon.enabled && weapon.CanAttack)
//            ////    {
//            ////        weapon.Attack(true);
//            ////        SoundEffectsHelper.Instance.MakeEnemyShotSound();
//            ////    }
//            ////}

//            soldierAnimatorr.SetBool("SoldierAttack", isAttackingg);
//            Shott();
//            yield return StartCoroutine(Wait(1.5f));

//        }

//        // Move
//        else
//        {
//            // Define a target?
//            if (positionTargett == Vector2.zero)
//            {
//                // Get a point on the screen, convert to world
//                // ATTENTION!!! This code block may cause soldier to move Y-axis, be careful!!!
//                if ((transform.position.x - Commandoo.transform.position.x) < 0)
//                {

//                    Vector2 randomPointt = new Vector2(Random.Range(transform.position.x, Commandoo.transform.position.x), 1f);
//                    moveScriptt.direction.x = -1 * moveScriptt.direction.x;
//                    positionTargett = Camera.main.ViewportToWorldPoint(randomPointt);

//                }
//                else if ((transform.position.x - Commandoo.transform.position.x) > 0)
//                {
//                    Vector2 randomPointt = new Vector2(Random.Range(Commandoo.position.x, transform.transform.position.x), 1f);
//                    moveScriptt.direction.x = -1 * moveScriptt.direction.x;
//                    positionTargett = Camera.main.ViewportToWorldPoint(randomPointt);
//                }


//            }

//            // Are we at the target? If so, find a new one
//            if (collider2D.OverlapPoint(positionTargett))
//            {
//                // Reset, will be set at the next frame
//                positionTargett = Vector2.zero;
//            }

//            // Go to the point
//            Vector3 directionn = ((Vector3)positionTargett - this.transform.position);

//            // Remember to use the move script
//            moveScriptt.direction = Vector3.Normalize(directionn);
//            soldierAnimatorr.SetBool("SoldierAttack", !isAttackingg);
//            yield return StartCoroutine(Wait(1.5f));
//            StartCoroutine(AI());

//        }

//    }

//    void Update()
//    {
//        Debug.Log((transform.position - Commandoo.transform.position).ToString());
//        // Shooting Control: Last Best Working Block
//        //if (Input.GetButtonDown("Fire1"))
//        //{
//        //    Shot();
//        //    soldierAnimator.SetBool("SoldierAttack", true);
//        //}
//        //else if (Input.GetButtonUp("Fire1"))
//        //{
//        //    CancelInvoke("Shot"); // Moustan el çekilince iptal edilecek.

//        //    soldierAnimator.SetBool("SoldierAttack", false);
//        //}

//        if ((transform.position.x - Commandoo.transform.position.x) < 0)
//        {
//            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
//            moveScriptt.direction.x = -1 * moveScriptt.direction.x;

//        }

//        else
//        {
//            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
//            moveScriptt.direction.x = -1 * moveScriptt.direction.x;

//        }


//        // AI
//        // ------------------------
//        // Move or Attack.
//    }

//    void Shott()
//    {
//        SoldierWeaponScript weaponn = GetComponent<SoldierWeaponScript>();
//        if (weaponn != null)
//        {
//            // false because the player is not an enemy
//            weaponn.Attack(false);
//        }
//    }


//    // Yapay zekanın nereye gideceğini gösteren yardımcı
//    void OnDrawGizmos()
//    {
//        //if (hasSpawn && isAttacking == false)
//        //{
//        Gizmos.DrawSphere(positionTargett, 0.25f);
//        //}
//    }

//    IEnumerator Wait(float duration)
//    {
//        for (float timer = 0; timer < duration; timer += Time.deltaTime)
//        {
//            yield return 0;
//        }
//    }


//}
