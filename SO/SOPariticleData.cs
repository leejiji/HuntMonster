using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOParticle", menuName = "SO/Particle", order = 0)]
public class SOPariticleData : ScriptableObject
{
    [SerializeField] List<ParticleData> particleDatas;
    public List<ParticleData> ParticleDatas => particleDatas;
}
[System.Serializable]
public struct ParticleData {
    [SerializeField] ParticleSystem particle;
    public ParticleSystem Particle => particle;
    [SerializeField] string particleName;
    public string ParticleName => particleName;
}