using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] SOPariticleData soParticleData;
    Dictionary<string, ParticleSystem> ParticleDataDIc = new Dictionary<string, ParticleSystem>();
    void Start()
    {
        for (int i = 0; i < soParticleData.ParticleDatas.Count; i++) {
            string name = soParticleData.ParticleDatas[i].ParticleName;
            ParticleSystem particle = soParticleData.ParticleDatas[i].Particle;
            ParticleDataDIc.Add(name, particle);
        }
    }

    public void SpawnParticle(string particleName, Vector3 pos) {
        if (ParticleDataDIc.ContainsKey(particleName)) {
            ParticleSystem particle = Instantiate(ParticleDataDIc[particleName], pos, Quaternion.identity);
            Destroy(particle.gameObject, particle.main.duration);
        }
    }
}
