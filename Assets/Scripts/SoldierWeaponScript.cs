using UnityEngine;
using System.Collections;

public class SoldierWeaponScript : MonoBehaviour
{

    public Transform soldierShotPrefab;
    public float shootingRate = 0.25f;
    public float shootCoolDown;
    // Use this for initialization
    void Start()
    {
        shootCoolDown = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootCoolDown > 0)
            shootCoolDown -= Time.deltaTime;
    }

    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCoolDown = shootingRate;

            var soldierShotTransform = Instantiate(soldierShotPrefab) as Transform;

            soldierShotTransform.position = transform.Find("Shot").position;

            MovementScript move = soldierShotTransform.gameObject.GetComponent<MovementScript>();
            if (move != null)
            {
                move.direction = this.transform.right; // towards in 2D space is the right of the sprite
            }
        }
    }

    public bool CanAttack
    {
        get
        {
            return shootCoolDown <= 0f;
        }
    }
}
