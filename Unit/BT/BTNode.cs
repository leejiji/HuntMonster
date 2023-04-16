using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// �ൿƮ�� (Behaviour Tree) ���� ����Ʈ : https://kindtis.tistory.com/590
namespace BT {
    [Serializable]
    public abstract class BTNode {
        public int ID = -1;
        public abstract bool Invoke();
        public virtual void DrawGizmos() { }
    }
    // �ڽ� ��带 ���� �� �ִ� ���
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
    // ���ʿ��� ����(���� ����) �ڽ� ��� ��ȸ
    // �ڽ� �� �ϳ��� True�� True ����
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
    // �ڽ� �� �ϳ��� False�� Flase ����
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
