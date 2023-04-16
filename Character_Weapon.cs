using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Zenject;
public class Character_Weapon : MonoBehaviour
{
    [SerializeField] SOJoyStickValue AttackStickValue;
    [SerializeField] Weapon_Unit weaponData;
    public Weapon_Unit WeaponData => weaponData;

    float ShotCul = 0.5f;
    float shotCul = 0;
    public Bullet bullet; //ÃÑ¾Ë Prefab
    public Transform FirePosition; //ÃÑ¾Ë ¹ß»ç À§Ä¡

    public Action ShotBulet;

    [Inject] DiContainer Container;
    [Inject] IPlaySound PlaySound;
    private void Awake() {
        WeaponData.Power = 20;
        StartCoroutine(ShotCulTime());
    }

    public void Weapon_Shot()
    {
        if (shotCul <= 0) {
            if (ShotBulet != null)
                ShotBulet();
            Bullet bullet_object = Container.InstantiatePrefabForComponent<Bullet>(bullet);
            bullet_object.gameObject.transform.position = FirePosition.position;
            bullet_object.transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
            bullet_object.BulletSet(WeaponData, transform);
            bullet_object.GetComponent<Rigidbody>().AddForce(transform.right * WeaponData.Speed);
            PlaySound.PlayOneShot(SoundType.SFX, "GunShot");
            WeaponData.Shot = false;
            shotCul = ShotCul;
            StartCoroutine(ShotCulTime());
        }
    }
    IEnumerator ShotCulTime() {
        while (shotCul > 0) {
            yield return null;
            shotCul -= Time.deltaTime;
        }
    }
    public void Weapon_Upgrade(int value)
    {
        WeaponData.Power += value;
    }
}

