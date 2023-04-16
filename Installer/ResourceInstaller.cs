using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public interface IPlayResource {
   public void PlayResource(string resourceKey);
}
public class UnitResource : MonoInstaller
{
    public override void InstallBindings() {
        base.InstallBindings();
        Container.Bind<IPlayResource[]>().FromMethodMultiple(GetResource);
    }

    IEnumerable<IPlayResource[]> GetResource(InjectContext context) {
        yield return new IPlayResource[] {
            new SoundManager()
        };
    }
}
public class SoundManager : IPlayResource {
    public void PlayResource(string resourceKey) {
        Debug.Log("»ç¿îµå");
    }
}
