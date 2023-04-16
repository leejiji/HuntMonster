using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class UnitManager : SingletonBehaviour<UnitManager>
{
    [SerializeField] List<Unit> UnitList = new List<Unit>(); // 스폰가능한 유닛 리스트
    Dictionary<string, Unit> UnitDataDic = new Dictionary<string, Unit>();

    List<string> SpawnedUnitNameList = new List<string>();
    Dictionary<string, UnitContainer> UnitContainerDic = new Dictionary<string, UnitContainer>();
    Dictionary<string, List<Unit>> SpawnedUnitDic = new Dictionary<string, List<Unit>>(); // 필드에 스폰 된 유닛들

    [Inject] DiContainer Container;
    protected override void Awake() {
        base.Awake();

        GameObject unitContiner = new GameObject("UnitContainer");
        for (int i = 0; i < UnitList.Count; i++) {
            string unitName = UnitList[i].SOUnitData.UnitName;
            UnitDataDic.Add(unitName, UnitList[i]);

            GameObject container = new GameObject(unitName + "Container");
            container.transform.parent = unitContiner.transform;
            UnitContainer ccontainer = new UnitContainer(container.transform);
            UnitContainerDic.Add(unitName, ccontainer);

            for(int k = 0; k < 200; k++) {
                UnitContainerDic[unitName].AddUnit(Container.InstantiatePrefabForComponent<Unit>(UnitList[i]));
            }
        }
        EventManager<UnitEvent>.Instance.AddListener(UnitEvent.Spawn, this, SpawnUnitEvent);
        EventManager<UnitEvent>.Instance.AddListener(UnitEvent.Die, this, DieUnitEvent);
    }
    // 유닛 스폰가능한 유닛만 스폰 하는 함수
    public void SpawnUnit(string UnitName, Vector3 SpawnPos) {
        if (!UnitContainerDic.ContainsKey(UnitName)) {
            Debug.LogWarning(UnitName + "해당 유닛은 없습니다.");
            return;
        }

        if (!SpawnedUnitDic.ContainsKey(UnitName)) {
            SpawnedUnitDic.Add(UnitName, new List<Unit>());
            SpawnedUnitNameList.Add(UnitName);
        }

        if(UnitContainerDic[UnitName].LastUnitCount() > 0)
        UnitContainerDic[UnitName].SpawnUnit(SpawnPos);
        else {
            Debug.LogWarning(UnitName + "유닛의 콘테이너가 작아서 확장합니다");
            for (int k = 0; k < 50; k++) {
                UnitContainerDic[UnitName].AddUnit(Container.InstantiatePrefabForComponent<Unit>(UnitDataDic[UnitName]));
            }
            Unit spawnUnit = UnitContainerDic[UnitName].SpawnUnit(SpawnPos);
            EventManager<UnitEvent>.Instance.PostEvent(UnitEvent.Spawn, spawnUnit, null);
        }
    }
    public List<Unit> GetSpawnableUnitList() {
        return UnitList;
    }
    public List<Unit> GetSpawnedUnitList(string UnitName) {
        if (SpawnedUnitDic.ContainsKey(UnitName)) {
            return SpawnedUnitDic[UnitName];
        }
        else
            return null;
    }
    // 유닛 스폰 감지하고 스폰되면 SpawnedUnitDic에 추가시켜 관리한다
    public void SpawnUnitEvent(UnitEvent eventType, Component Sender, object param) {
        if (Sender is Unit || Sender is Monster) {
            Unit unit = (Unit)Sender;
            Debug.Log("유닛 소환 감지 " + unit.SOUnitData.UnitName);
            if (!SpawnedUnitDic.ContainsKey(unit.SOUnitData.UnitName)) {
                SpawnedUnitDic.Add(unit.SOUnitData.UnitName, new List<Unit>());
                SpawnedUnitNameList.Add(unit.SOUnitData.UnitName);
            }

            SpawnedUnitDic[unit.SOUnitData.UnitName].Add(unit);
            //Debug.Log("유닛 스폰 감지 " + unit.SOUnitData.UnitName);
        }
    }
    // 유닛 죽음 감지하고 죽으면 SpawnedUnitDic에 뺀다
    public void DieUnitEvent(UnitEvent eventType, Component Sender, object param) {
        if (Sender is Unit) {
            Unit unit = (Unit)Sender;
            SpawnedUnitDic[unit.SOUnitData.UnitName].Remove(unit);

            if(SpawnedUnitDic[unit.SOUnitData.UnitName].Count == 0) {
                SpawnedUnitNameList.Remove(unit.SOUnitData.UnitName);
            }
        }
    }

    public List<string> GetSpawnedUnitNameList() {
        return SpawnedUnitNameList;
    }
}

public class UnitContainer {
    Queue<Unit> ReadyUnit = new Queue<Unit>();
    Transform Continer;
    public UnitContainer(Transform container) {
        Continer = container;
    }
    public void AddUnit(Unit unit) {
        ReadyUnit.Enqueue(unit);
        unit.DieEvent += DieUnit;
        unit.transform.parent = Continer;
        unit.gameObject.SetActive(false);
    }
    void DieUnit(Unit unit) {
        unit.DieEvent -= DieUnit;
        AddUnit(unit);
    }
    public Unit SpawnUnit(Vector3 pos) {
        Unit unit = ReadyUnit.Dequeue();
        unit.transform.position = pos;
        unit.gameObject.SetActive(true);
        return unit;
    }

    public int LastUnitCount() {
        return ReadyUnit.Count;
    }
}
