using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
public class Bullet : MonoBehaviour
{
    Weapon_Unit WeaponData;
    Transform Shoter;
    int Damage;

    [Inject] ParticleSpawner ParticleSpawn;
    private void Awake() {
        Destroy(gameObject, 5f);
    }
    public void BulletSet(Weapon_Unit weaponData, Transform shoter) {
        WeaponData = weaponData;
        Shoter = shoter;
    }
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Enemy") {
            Unit unit = collision.gameObject.GetComponent<Unit>();
            unit.Damaged((int)WeaponData.Power);
            Rigidbody rigid = collision.gameObject.GetComponent<Rigidbody>();
            unit.Stun();
            Vector3 dir = (collision.transform.position - Shoter.position).normalized;
            dir = new Vector3(dir.x, 0, dir.z);
            unit.transform.DOMove(unit.transform.position + dir * 0.3f, 0.5f).OnComplete(()=> { unit.StunEnd(); });
            ParticleSpawn.SpawnParticle("BulletHit", collision.transform.position);
            Destroy(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
