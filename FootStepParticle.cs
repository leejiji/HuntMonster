using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepParticle : MonoBehaviour
{
    Unit FootStepUnit;
    [SerializeField] ParticleSystem m_Pariticle;
    [SerializeField] float SpawnCul;
    GameObject ParticleContainer;
    float cul;
    Stack<ParticleSystem> ParticleStack;
    List<ParticleObject> ParticleObjectList = new List<ParticleObject>();
    void Start() {
        TryGetComponent(out FootStepUnit);
        ParticleContainer = new GameObject(FootStepUnit.gameObject.name + " StepParticle Container");
        ParticleContainer.transform.position = Vector3.zero;
        FootStepUnit.MoveEvent += (unit) => { CulTimeUpdate(); };
        FootStepUnit.DieEvent += (unit) => { Destroy(ParticleContainer); };

        ParticleStack = new Stack<ParticleSystem>();
        for (int i = 0; i < 15; i++) {
            ParticleSystem particle = Instantiate(m_Pariticle);
            particle.transform.parent = ParticleContainer.transform;
            particle.gameObject.SetActive(false);
            ParticleStack.Push(particle);
        }
        StartCoroutine(C_CulCheck());
    }
    void CulTimeUpdate() {
        if (cul > 0)
            cul -= Time.deltaTime;
        else {
            SpawnParticle();
            cul = SpawnCul;
        }
    }

    void SpawnParticle() {
        if(ParticleStack.Count > 0) {
            ParticleSystem particle = ParticleStack.Pop();
            particle.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 particleEuler = particle.transform.eulerAngles;
            particle.transform.rotation = Quaternion.Euler(particleEuler.x, FootStepUnit.transform.eulerAngles.y, particleEuler.z);
            particle.gameObject.SetActive(true);
            particle.Play();

            ParticleObject particleObject = new ParticleObject();
            particleObject.Particle = particle;
            particleObject.LastCul = particleObject.Particle.main.duration;

            ParticleObjectList.Add(particleObject);
        }
    }

    IEnumerator C_CulCheck() {
        while (true) {
            for(int i = 0; i < ParticleObjectList.Count; i++) {
                ParticleObjectList[i].LastCul -= Time.deltaTime;
                if (ParticleObjectList[i].LastCul <= 0) {
                    ParticleStack.Push(ParticleObjectList[i].Particle);
                    ParticleObjectList[i].Particle.gameObject.SetActive(false);
                    ParticleObjectList.RemoveAt(i);
                }
            }
            yield return null;
        }
    }
    public class ParticleObject {
        public ParticleSystem Particle;
        public float LastCul;
    }
}

public class ObjectPool<T> {

}