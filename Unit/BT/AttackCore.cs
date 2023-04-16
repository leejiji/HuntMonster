using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

namespace BT {
    public class AttackCore : BTNode {
        Monster monster;
        float UpdateCul = 15;
        float updateCul = 0;
        Node[] Way;
        int CurrentWayIndex;
        int layerMask = (1 << LayerMask.NameToLayer("Object")) + (1 << LayerMask.NameToLayer("Wall"));
        float DirectCoreCul = 2;
        float directCoreCul = 0;

        bool FollowingPath = true;
        bool needFindNewPath = false;
        public AttackCore(Monster unit) {
            monster = unit;
        }
        void PathfindCallback(Node[] nodes) {
            CurrentWayIndex = 4;
            Way = nodes;
        }
        public override bool Invoke() {          
            FollowingPath = false;
            if (GameManager.Instance.core != null) {

                float Dst = Vector2.Distance(monster.GetVec2Position(), GameManager.Instance.core.GetVec2Position());
                if (Dst < monster.SOUnitData.AttackRange)
                    monster.Attack(GameManager.Instance.core);
                else {
                    if (CheckWall()) {
                        needFindNewPath = true;
                        return true;
                        
                    }
                    if (needFindNewPath) {
                        updateCul = UpdateCul;
                        PathfindingManager.Instance.PathFind(monster.transform.position, GameManager.Instance.core.transform.position, PathfindCallback);
                        needFindNewPath = false;
                    }
                    if (updateCul >= 0) {
                        updateCul -= Time.deltaTime;
                    }
                    else {
                        updateCul = UpdateCul;
                        PathfindingManager.Instance.PathFind(monster.transform.position, GameManager.Instance.core.transform.position, PathfindCallback);
                        needFindNewPath = false;
                    }
                    needFindNewPath = false;
                    FollowPath();
                }
                return true;
            }
            else{
                Debug.LogError("코어가 없습니다");
            }
            return false;
        }
        bool CheckWall() {
            Vector3 dirToCore = (GameManager.Instance.core.transform.position - monster.transform.position).normalized;
            float dst = Vector2.Distance(monster.GetVec2Position(), GameManager.Instance.core.GetVec2Position());
            RaycastHit ray;
            if (!Physics.Raycast(monster.transform.position, dirToCore, out ray, dst, layerMask)) {
                if (directCoreCul <= 0) {
                    monster.MoveAndRotate(GameManager.Instance.core.transform);
                    updateCul = 0;
                    return true;
                }
                else {
                    directCoreCul -= Time.deltaTime;
                    return false;
                }
            }
            else {
                if (ray.collider.tag == "Wall") {
                    FollowingPath = false;
                    updateCul = 0;
                    monster.MoveAndRotate(ray.collider.transform);
                    float distanceToWall = Vector3.Distance(ray.point, monster.transform.position);
                    if(distanceToWall < monster.SOUnitData.AttackRange) {
                        monster.Attack(UnitManager.Instance.GetSpawnedUnitList("Wall")[0]);
                    }
                    return true;
                }
            }
            directCoreCul = DirectCoreCul;
            return false;
        }
        public void FollowPath() {
            FollowingPath = true;
            if (Way != null) {
                if (CurrentWayIndex < Way.Length) {
                    float Dst = Vector2.Distance(Way[CurrentWayIndex].WorldVec2Position, monster.GetVec2Position());

                    if (Dst < 0.5f) {
                        CurrentWayIndex++;
                        return;
                    }

                    if (Dst > 4) {
                        needFindNewPath = true;
                        return;
                    }
                    Vector2 vec2Dir = (Way[CurrentWayIndex].WorldVec2Position - monster.GetVec2Position()).normalized;
                    Vector3 dir = new Vector3(vec2Dir.x, 0, vec2Dir.y);
                    monster.MoveAndRotate(dir);
                }
            }
        }
        public override void DrawGizmos() {
            Gizmos.color = Color.white;
            MyGizmos.DrawWireCicle(monster.transform.position, monster.SOUnitData.AttackRange, 30);
            if (FollowingPath) {
                if (Way != null) {
                    Gizmos.color = Color.green;
                    for (int i = CurrentWayIndex; i < Way.Length; i++) {
                        Gizmos.DrawLine(Way[i - 1].WorldPosition, Way[i].WorldPosition);
                    }
                }
            }
        }
    }
}
