using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
public class Barricade : Unit
{
    [SerializeField] SOUnit UnitData;
    [SerializeField] Transform BarricadeContainer;
    public override SOUnit SOUnitData => UnitData;
    [SerializeField] LayerMask LandingCollision;

    [Inject] IPlaySound PlaySound;
    public override void Awake() {
        Init();
        DieEvent += (unit) => { PlaySound.PlayOneShot(SoundType.SFX, "BarricadeBreak");
            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    if (x != 0 || y != 0) {
                        Vector3 spawnPos = transform.position + (Vector3.forward * x * unitCollider.bounds.size.x * 0.5f) * Random.Range(0.8f, 1.2f)
                        + (Vector3.right * y * unitCollider.bounds.size.z * 0.5f) * Random.Range(0.8f, 1.2f);
                        ParticleSpawn.SpawnParticle("BarricadeBreak", spawnPos);
                    }
                }
            }
            //ParticleSpawn.SpawnParticle("DestroyBarricade", transform.position + Vector3.up );
            BarricadeContainer.gameObject.SetActive(false); 
            unitCollider.enabled = false; };
        BarricadeContainer.gameObject.SetActive(false);
        EventManager<ShopEvent>.Instance.AddListener(ShopEvent.BuyWallHealItem, this, HealWall);
        EventManager<ShopEvent>.Instance.AddListener(ShopEvent.BuyWallItem, this, BuyWall);

    }
    void Start() {
        HpValueChangeEvent += (Unit unit) => { EventManager<GameEvent>.Instance.PostEvent(GameEvent.WallHPChange, this, null); };
        unitCollider.enabled = false;
    }
    void BuyWall(ShopEvent eventType, Component sender, object param) {
        HP = SOUnitData.MaxHP;
        transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        unitCollider.enabled = false;
        BarricadeContainer.gameObject.SetActive(true);
        transform.DOMove(GetCollisionGroundPos(), 0.5f).OnComplete(() => { unitCollider.enabled = true; Landing(); });
    }

    void Landing() {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, unitCollider.bounds.size, Vector3.up, Quaternion.identity, 10, LandingCollision);
        StartCoroutine(movePos(hits));
    }
    IEnumerator movePos(RaycastHit[] hits) {
        DOTween.SetTweensCapacity(1000, 1000);
        for (int i = 0; i < hits.Length; i++) {
            Unit hitUnit = hits[i].transform.GetComponent<Unit>();
            LayerMask layer = hits[i].collider.transform.gameObject.layer;
            LayerMask noneLayerMask = LayerMask.NameToLayer("None");

            hits[i].collider.transform.gameObject.layer = noneLayerMask;

            hitUnit.Stun();
            Vector3 dir = (hits[i].transform.position - transform.position).normalized;
            Vector3 movePos = hits[i].transform.position + dir * 6;
            movePos = new Vector3(movePos.x, hits[i].transform.position.y, movePos.z);

            hits[i].collider.transform.DOKill();
            float Y = hits[i].transform.position.y;
            int k = i;
            hits[k].collider.transform.DOMoveY(Y + 5, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() => { hitUnit.gameObject.layer = layer; hitUnit.StunEnd(); });
            hits[i].collider.transform.DOMoveX(movePos.x, 1);
            hits[i].collider.transform.DOMoveZ(movePos.z, 1);
        }
        yield return null;
    }
    void HealWall(ShopEvent eventType, Component sender, object param) {
        if(hp > 0)
        hp += 150;
    }
}
