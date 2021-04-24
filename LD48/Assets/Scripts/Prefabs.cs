using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{

    private static string mainFolder = "Prefabs";
    private static Dictionary<string, GameObject> classPrefabs = new Dictionary<string, GameObject>();

    private static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    public static T Get<T>() {
        return GetPrefab<T>(typeof(T).Name);
    }

    private static GameObject Load(string path) {
        UnityEngine.Object loadedObject = Resources.Load($"{mainFolder}/{path}");
        if (loadedObject == null) {
            Debug.Log($"Couldn't find Prefab at {mainFolder}/{path}!");
            return null;
        }
        return loadedObject as GameObject;
    }

    private static T GetPrefab<T>(string name) {
        GameObject prefab;
        Dictionary<string, GameObject> dict = classPrefabs;
        if (dict.ContainsKey(name)) {
            prefab = dict[name];
        } else {
            prefab = Load(name);
            dict[name] = prefab;
        }
        return Instantiate(prefab, Vector2.zero, Quaternion.identity).GetComponent<T>();
    }
}
