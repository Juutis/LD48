using UnityEngine;

public static class MonoBehaviourExtension {
    public static Transform FindChildObject (this MonoBehaviour monoBehaviour, string name) {
        foreach(Transform child in monoBehaviour.transform.GetComponentsInChildren<Transform>()) {
            if (child.name == name) {
                return child.gameObject.transform;
            }
        }
        return null;
    }
}