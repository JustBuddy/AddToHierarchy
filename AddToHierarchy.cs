using UnityEngine;
using UnityEditor;

// Simple script I use to spawn prefabs via Toolbar menu items.

namespace BUDDYWORKS.Scripts.AddToHierarchy
{
    public class InstantiateAndPingPrefabByGUID : MonoBehaviour
    {
        public static string prefabGUID = "INSERT GUID";

        [MenuItem("BUDDYWORKS/Scripts/Spawn Prefab...")]
        public static void SpawnCustomPrefab()
        {
            SpawnPrefab(prefabGUID);
        }

        private static void SpawnPrefab(string guid)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(guid);

            if (string.IsNullOrEmpty(prefabPath))
            {
                Debug.LogError("Prefab with GUID " + guid + " not found.");
                return;
            }

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            GameObject selectedObject = Selection.activeGameObject;

            if (prefab == null)
            {
                Debug.LogError("Failed to load prefab with GUID " + guid + " at path " + prefabPath);
                return;
            }

            GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            if (selectedObject != null)
            {
                instantiatedPrefab.transform.parent = selectedObject.transform;
            }

            if (instantiatedPrefab != null)
            {
                EditorGUIUtility.PingObject(instantiatedPrefab);
            }
            else
            {
                Debug.LogError("Failed to instantiate prefab with GUID " + guid);
            }
        }
    }
}
