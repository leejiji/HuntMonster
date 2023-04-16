using UnityEngine;
using BT;
namespace BT {
    public class AttackTarget : BTNode {
        Unit unit;
        float sensingRange;
        float attakRange;
        LayerMask layerMask;
        int objectMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("Object"));
        bool FIndTarget = false;
        /// <summary>
        /// 인식 범위 안에 타겟이 있으면 타겟을 쫓아감
        /// </summary>
        /// <param name="unit">타겟을 "찾는" 유닛</param>
        /// <param name="range">타겟 인식 범위</param>
        /// <param name="layerMask">타겟의 레이어</param>
        public AttackTarget(Unit unit, float sensingRange, float attakRange, LayerMask layerMask) {
            this.unit = unit;
            this.sensingRange = sensingRange;
            this.attakRange = attakRange;
            this.layerMask = layerMask;
        }
        public override bool Invoke() {
            RaycastHit[] hit = Physics.SphereCastAll
                (unit.transform.position, sensingRange, Vector3.up, sensingRange, layerMask);
            if (hit.Length > 0) {

                Transform trans = hit[0].collider.transform;

                float dst = Vector2.Distance(unit.GetVec2Position(), new Vector2(trans.position.x, trans.position.z));
                Vector3 dir = (trans.position - unit.transform.position).normalized;
                RaycastHit hit2;
                if (Physics.Raycast(unit.transform.position, dir, out hit2, dst, objectMask)) {
                    FIndTarget = false;
                    return false;
                }
                FIndTarget = true;

                if (dst >= unit.SOUnitData.AttackRange) {
                    Vector3 dir3 = (trans.position - unit.transform.position).normalized;
                    Vector3 dir2 = new Vector3(dir3.x, 0, dir3.z);
                    unit.UnitMove(dir2);
                    unit.Rotate(trans);
                }
                else
                    unit.Attack(hit[0].transform.GetComponent<Unit>());
                return true;
            }
            else {
                FIndTarget = false;
                return false;
            }
        }
        public override void DrawGizmos() {
            Color gizmoColor = FIndTarget ? Color.green : Color.red;
            Gizmos.color = gizmoColor;
            MyGizmos.DrawWireCicle(unit.transform.position, sensingRange, 30);
        }
    }
}
public static class MyGizmos {
    public static void DrawWireCicle(Vector3 center,float radius, int smoothNum) {
        float angle = (float)360 / smoothNum;
        Vector3[] dir = new Vector3[smoothNum];
        dir[0] = center + new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)) * radius;
        for (int i = 1; i < smoothNum; i++) {
            float dirAngle = angle * i;
            dirAngle *= Mathf.Deg2Rad;
            Vector3 dirvec = new Vector3(Mathf.Cos(dirAngle), 0, Mathf.Sin(dirAngle)) * radius;
            dir[i] = center + dirvec;
            Gizmos.DrawLine(dir[i - 1], dir[i]);
        }
        Gizmos.DrawLine(dir[smoothNum - 1], dir[0]);
    }
}