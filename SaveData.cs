using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData
{
    public SaveData() {
        type = SaveDataType.None;
    }
    public SaveData(int _int) {
        this._int = _int;
        type = SaveDataType.Int;
    }

    public SaveData(float _float) {
        this._float = _float;
        type = SaveDataType.Float;
    }

    public SaveData(string _string) {
        this._string = _string;
        type = SaveDataType.String;
    }
    public SaveData(List<SaveData> _list) {
        this._list = _list;
        type = SaveDataType.List;
    }
    public SaveData(string key, SaveData value) {
        _Dic.Add(key, value);
        type = SaveDataType.Dictionary;
    }

    public int GetInt() {
        return _int;
    }

    public float GetFloat() {
        return _float;
    }
    public string GetString() {
        return _string;
    }

    public List<SaveData> GetList() {
        return _list;
    }
    public void AddData(string key, SaveData data) {
        type = SaveDataType.Dictionary;
        if (!_Dic.ContainsKey(key)) {
            _Dic.Add(key, data);
        }
        else {
            Debug.LogWarning(key + "라는 키는 이미 존재합니다.");
        }
    }
    public SaveData GetData(string key) {
        if (_Dic.ContainsKey(key)) {
            return _Dic[key];
        }
        else {
            Debug.LogWarning(key + "라는 키는 업습니다.");
            return null;
        }
    }
    public object GetSerializedData() {
        switch (type) {
            case SaveDataType.Int:
                return _int;
            case SaveDataType.Float:
                return _float;
            case SaveDataType.String:
                return _string;
            case SaveDataType.List:
                List<object> list = new List<object>();
                for (int i = 0; i < _list.Count; i++)
                    list.Add(_list[i].GetSerializedData());
                return list;
            case SaveDataType.Dictionary:
                DI di = new DI();
                di.kv = new KV[_Dic.Values.Count];
                int k = 0;
                for (var enumerator = _Dic.GetEnumerator(); enumerator.MoveNext();) {
                    KeyValuePair<string, SaveData> key = enumerator.Current;
                    KV kv = new KV(key.Key, key.Value.GetSerializedData());
                    di.kv[k] = kv;
                    k++;
                }
                return di;
            default:
                return null;
        }
    }
    public void LoadFromSerializedData(object data) {
        if(data is int) {
            type = SaveDataType.Int;
            _int = (int)data;
        }
        else if(data is float) {
            type = SaveDataType.Float;
            _float = (float)data;
        }
        else if (data is string) {
            type = SaveDataType.String;
            _string = (string)data;
        }
        else if (data is List<object>) {
            type = SaveDataType.List;
            List<object> list = (List<object>)data;
            for (int i = 0; i < list.Count; i++) {
                SaveData saveData = new SaveData();
                saveData.LoadFromSerializedData(list[i]);
                _list.Add(saveData);
            }
        }
        else if (data is DI) {
            type = SaveDataType.Dictionary;
            _Dic = new Dictionary<string, SaveData>();
            DI di = (DI)data;
            for(int i = 0; i < di.kv.Length; i++) {
                string key = di.kv[i].K;
                object value = di.kv[i].V;
                SaveData savedata = new SaveData();
                savedata.LoadFromSerializedData(value);
                _Dic.Add(key, savedata);
            }
        }
    }

    int _int;
    float _float;
    string _string;
    List<SaveData> _list = new List<SaveData>();
    Dictionary<string, SaveData> _Dic = new Dictionary<string, SaveData>();
    SaveDataType type;
}
[System.Serializable]
public class DI {
    public KV[] kv;
}
[System.Serializable]
public class KV {
    public string K;
    public object V;
    public KV(string key, object value) {
        K = key;
        V = value;
    }
}
public enum SaveDataType {
    None, Int, Float, String, List, Dictionary
}
