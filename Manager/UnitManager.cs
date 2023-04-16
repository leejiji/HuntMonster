using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class UnitManager : SingletonBehaviour<UnitManager>
{
    [SerializeField] List<Unit> UnitList = new List<Unit>(); // ���������� ���� ����Ʈ
    Dictionary<string, Unit> UnitDataDic = new Dictionary<string, Unit>();

    List<string> SpawnedUnitNameList = new List<string>();
    Dictionary<string, UnitContainer> UnitContainerDic = new Dictionary<string, UnitContainer>();
    Dictionary<string, List<Unit>> SpawnedUnitDic = new Dictionary<string, List<Unit>>(); // �ʵ忡 ���� �� ���ֵ�

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
    // ���� ���������� ���ָ� ���� �ϴ� �Լ�
    public void SpawnUnit(string UnitName, Vector3 SpawnPos) {
        if (!UnitContainerDic.ContainsKey(UnitName)) {
            Debug.LogWarning(UnitName + "�ش� ������ �����ϴ�.");
            return;
        }

        if (!SpawnedUnitDic.ContainsKey(UnitName)) {
            SpawnedUnitDic.Add(UnitName, new List<Unit>());
            SpawnedUnitNameList.Add(UnitName);
        }

        if(UnitContainerDic[UnitName].LastUnitCount() > 0)
        UnitContainerDic[UnitName].SpawnUnit(SpawnPos);
        else {
            Debug.LogWarning(UnitName + "������ �����̳ʰ� �۾Ƽ� Ȯ���մϴ�");
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
    // ���� ���� �����ϰ� �����Ǹ� SpawnedUnitDic�� �߰����� �����Ѵ�
    public void SpawnUnitEvent(UnitEvent eventType, Component Sender, object param) {
        if (Sender is Unit || Sender is Monster) {
            Unit unit = (Unit)Sender;
            Debug.Log("���� ��ȯ ���� " + unit.SOUnitData.UnitName);
            if (!SpawnedUnitDic.ContainsKey(unit.SOUnitData.UnitName)) {
                SpawnedUnitDic.Add(unit.SOUnitData.UnitName, new List<Unit>());
                SpawnedUnitNameList.Add(unit.SOUnitData.UnitName);
            }

            SpawnedUnitDic[unit.SOUnitData.UnitName].Add(unit);
            //Debug.Log("���� ���� ���� " + unit.SOUnitData.UnitName);
        }
    }
    // ���� ���� �����ϰ� ������ SpawnedUnitDic�� ����
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
