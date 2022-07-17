using UnityEngine;
using System.Collections;

/// <summary>
/// Projectile behavior: Bu script genel amaçlı hazırlanıp hem düşman hem de ana karakter mermileri üzerine işlenecek. Şutların yönünü ve ilerleme bilgisi MoveScript altında tutulacak.
/// </summary>

public class ProjectileScript : MonoBehaviour
{

    void Update()
    {
        //bool shoot = Input.GetButtonDown("Fire1");
        //shoot |= Input.GetButtonDown("Fire2");

        if (Input.GetButtonDown("Fire1"))
        {
            //if (shoot)
            //{
            InvokeRepeating("Shot", 0.1f, 0.00005f); // Mousa basılı tutulduğu sürece shoot fonksiyonu çağrılacak.
            //}
        }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shot"); // Moustan el çekilince iptal edilecek.
            }
        //}
    }

    void Shot()
    {
        // Careful: For Mac users, ctrl + arrow is a bad idea

        //if (shoot)
        //{
        WeaponScript weapon = GetComponent<WeaponScript>();
        if (weapon != null)
        {
            // false because the player is not an enemy
            weapon.Attack(false);
        }
        //}

    }

}
