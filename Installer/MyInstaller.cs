using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class MyInstaller : MonoInstaller
{
    [SerializeField] MinimapIcon minimapIcon;
    [SerializeField] OptionData m_OptionData;
    [SerializeField] SoundPlay soundPlay;
    [SerializeField] ParticleSpawner particleSpawner;
    [SerializeField] MinimapCaptureCamera minimapCaptureCamera;
    [SerializeField] MapManager m_MapManager;
    public override void InstallBindings() {
        Container.Bind<MinimapIcon>().FromInstance(minimapIcon).AsSingle();
        Container.Bind<OptionData>().FromInstance(m_OptionData).AsSingle();
        Container.Bind<IPlaySound>().FromMethod(GetResourceManager);
        if(particleSpawner != null)
        Container.Bind<ParticleSpawner>().FromInstance(particleSpawner);
        if (minimapCaptureCamera != null)
            Container.Bind<MinimapCaptureCamera>().FromInstance(minimapCaptureCamera);
        if (m_MapManager != null)
            Container.Bind<MapManager>().FromInstance(m_MapManager);
        //Container.BindFactory<Actor, Actor.ActorFactory>().FromComponentInNewPrefab(actor);
    }

    IPlaySound GetResourceManager(InjectContext context) {
        return soundPlay;

    }
}
public class Actor : MonoBehaviour {
    [System.Obsolete]
    public class Factory : Factory<Actor> {
        }
}

public class ActorSpawner {
    readonly Actor.Factory actorSpawner;
}
