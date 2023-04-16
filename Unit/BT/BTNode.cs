using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// 행동트리 (Behaviour Tree) 참고 사이트 : https://kindtis.tistory.com/590
namespace BT {
    [Serializable]
    public abstract class BTNode {
        public int ID = -1;
        public abstract bool Invoke();
        public virtual void DrawGizmos() { }
    }
    // 자식 노드를 만들 수 있는 노드
    public class BTCompositeNode : BTNode {
        List<BTNode> Children = new List<BTNode>();
        public override bool Invoke() {
            throw new NotImplementedException();
        }

        public void AddChild(BTNode node) {
            Children.Add(node);
        }
        public List<BTNode> GetChildren() {
            return Children;
        }
    }
    // 왼쪽에서 부터(먼저 들어온) 자식 노드 순회
    // 자식 중 하나라도 True면 True 리턴
    public class BTSelector : BTCompositeNode {
        public override bool Invoke() {
            foreach (BTNode node in GetChildren()) {
                if (node.Invoke()) {
                    return true;
                }
            }
            return false;
        }
    }

    [Serializable]
    // 자식 중 하나라도 False면 Flase 리턴
    public class BTSequence : BTCompositeNode {
        public override bool Invoke() {
            foreach (BTNode node in GetChildren()) {
                if (!node.Invoke()) {
                    return false;
                }
            }
            return true;
        }
    }
}
