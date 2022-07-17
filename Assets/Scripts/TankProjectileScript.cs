using UnityEngine;
using System.Collections;

public class TankProjectileScript : MonoBehaviour {

	void Start () {
        StartCoroutine(Fire());

	}
	
    IEnumerator Fire()
    {
        Shot();
        yield return StartCoroutine(Wait(0.3f));
        Shot();
        yield return StartCoroutine(Wait(0.3f));
        Shot();
        yield return StartCoroutine(Wait(1.5f));

        StartCoroutine(Fire());
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
