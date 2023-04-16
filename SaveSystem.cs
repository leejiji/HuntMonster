using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class SaveSystem
{
    static string SaveFolderPath() {
        return Application.persistentDataPath;
    }
    static string GetSaveSlotPath(int slotNum) {
        return SaveFolderPath() + '/' + slotNum;
    }
    public static void Save(int slotNum, string fileName, SaveData saveData) {
        fileName = fileName + ".dat";
        string directoryPath = GetSaveSlotPath(slotNum);
        object serializedData = saveData.GetSerializedData();
        if (!Directory.Exists(directoryPath)) {
            Directory.CreateDirectory(directoryPath);
        }
        string path = directoryPath + '/' + fileName;

        using (Stream SW = new FileStream(path, FileMode.OpenOrCreate)) {
            new BinaryFormatter().Serialize(SW, serializedData);
        }
    }
    public static SaveData Load(int slotNum, string fileName) {
        fileName = fileName + ".dat";
        string directoryPath = GetSaveSlotPath(slotNum);

        if (!Directory.Exists(directoryPath)) {
            Debug.Log("�ε��ҷ��� ���丮�� �����ϴ�\n" + directoryPath);
            return null;
        }
        string path = directoryPath + '/' + fileName;
        if (!File.Exists(path)){
            Debug.Log("������ �������� �ʽ��ϴ�\n" + path);
            return null;
        }

        object serializedData;
        using (Stream stream = new FileStream(path, FileMode.Open)) {
            serializedData = new BinaryFormatter().Deserialize(stream);
        }
        SaveData saveData = new SaveData();
        saveData.LoadFromSerializedData(serializedData);
        return saveData;
    }
}
