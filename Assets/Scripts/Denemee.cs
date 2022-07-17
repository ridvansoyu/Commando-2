using UnityEngine;
using System.Collections;

/// <summary>
/// Projectile behavior: Bu script genel amaçlı hazırlanıp hem düşman hem de ana karakter mermileri üzerine işlenecek. Şutların yönünü ve ilerleme bilgisi MoveScript altında tutulacak.
/// </summary>

public class Denemee : MonoBehaviour
{
    public Transform Commando;

    public float minAttackCooldown = 0.5f;
    public float maxAttackCooldown = 2f;

    private float aiCooldown;
    

    private bool isAttacking;
    private Vector2 positionTarget;

    private SoldierMovementScript moveScript;
    private Animator soldierAnimator;

    void Awake()
    {
        soldierAnimator = GetComponent<Animator>();
        moveScript = GetComponent<SoldierMovementScript>();
    }

    void Start()
    {
        isAttacking = false;
        aiCooldown = maxAttackCooldown;

        // USE COROUTINE WITH AICOOLDOWN
    }

    void Update()
    {
        Debug.Log((transform.position - Commando.transform.position).ToString());
        // Shooting Control: Last Best Working Block
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Shot();
        //    soldierAnimator.SetBool("SoldierAttack", true);
        //}
        //else if (Input.GetButtonUp("Fire1"))
        //{
        //    CancelInvoke("Shot"); // Moustan el çekilince iptal edilecek.

        //    soldierAnimator.SetBool("SoldierAttack", false);
        //}

        if ((transform.position.x - Commando.transform.position.x) < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            moveScript.direction.x = -1 * moveScript.direction.x;

        }

        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            moveScript.direction.x = -1 * moveScript.direction.x;

        }


        // AI
        // ------------------------
        // Move or Attack.
        aiCooldown -= Time.deltaTime;

        if (aiCooldown <= 0)
        {
            isAttacking = !isAttacking;

            aiCooldown = Random.RandomRange(minAttackCooldown, maxAttackCooldown);
            positionTarget = Vector2.zero;

        }

        // Attack
        if (isAttacking)
        {
            // Stop any movement
            moveScript.direction = Vector2.zero;

            // HERE ATTACK CODES......

            // May be multiple weapon?
            ////foreach (WeaponScript weapon in weapons)
            ////{
            ////    if (weapon != null && weapon.enabled && weapon.CanAttack)
            ////    {
            ////        weapon.Attack(true);
            ////        SoundEffectsHelper.Instance.MakeEnemyShotSound();
            ////    }
            ////}

            soldierAnimator.SetBool("SoldierAttack", isAttacking);
            Shot();

        }

        // Move
        else
        {
            // Define a target?
            if (positionTarget == Vector2.zero)
            {
                // Get a point on the screen, convert to world
                // ATTENTION!!! This code block may cause soldier to move Y-axis, be careful!!!
                if ((transform.position.x - Commando.transform.position.x) < 0)
                {

                    Vector2 randomPoint = new Vector2(Random.Range(transform.position.x, Commando.transform.position.x), 1f);
                    moveScript.direction.x = -1 * moveScript.direction.x;
                    positionTarget = Camera.main.ViewportToWorldPoint(randomPoint);

                }
                else if ((transform.position.x - Commando.transform.position.x) > 0)
                {
                    Vector2 randomPoint = new Vector2(Random.Range(Commando.position.x, transform.transform.position.x), 1f);
                    moveScript.direction.x = -1 * moveScript.direction.x;
                    positionTarget = Camera.main.ViewportToWorldPoint(randomPoint);
                }


            }

            // Are we at the target? If so, find a new one
            if (collider2D.OverlapPoint(positionTarget))
            {
                // Reset, will be set at the next frame
                positionTarget = Vector2.zero;
            }

            // Go to the point
            Vector3 direction = ((Vector3)positionTarget - this.transform.position);

            // Remember to use the move script
            moveScript.direction = Vector3.Normalize(direction);
            soldierAnimator.SetBool("SoldierAttack", !isAttacking);
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


    // Yapay zekanın nereye gideceğini gösteren yardımcı
    //void OnDrawGizmos()
    //{
    //    //if (hasSpawn && isAttacking == false)
    //    //{
    //    Gizmos.DrawSphere(positionTarget, 0.25f);
    //    //}
    //}

    IEnumerator Wait(float duration)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            yield return 0;
        }
    }


}
