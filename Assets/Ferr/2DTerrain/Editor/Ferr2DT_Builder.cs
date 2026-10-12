using UnityEditor;
using UnityEngine;

public partial class TerrainTracker : AssetPostprocessor {
	static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
		for (int i = 0; i < importedAssets.Length; i++) {
			if (importedAssets[i].EndsWith(".prefab")) {
				GameObject prefabAsset = AssetDatabase.LoadAssetAtPath(importedAssets[i], typeof(UnityEngine.Object)) as GameObject;
				if (prefabAsset == null) continue;
				
				// Build the prefab asset
				Ferr2DT_PathTerrain[] terrains = prefabAsset.GetComponentsInChildren<Ferr2DT_PathTerrain>();
				for (int t = 0; t < terrains.Length; t++) {
					MeshFilter filter = terrains[t].gameObject.GetComponent<MeshFilter>();
					if (filter.sharedMesh == null){
						terrains[t].CheckedLegacy = false;
						terrains[t].PathData.SetDirty();
						terrains[t].Build(true);
					}
				}

				// If we were Adding a prefab, or Applying a prefab, make sure we update the active instance to the values we just built
				if (terrains.Length > 0 && Selection.activeGameObject != null) {
					
					#if UNITY_2018_2_OR_NEWER
					UnityEngine.Object prefab = PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeGameObject);
					#else
					UnityEngine.Object prefab = PrefabUtility.GetPrefabParent(Selection.activeGameObject);
					#endif

					Ferr2DT_PathTerrain[] sceneTerrains = null;
					if (prefab == prefabAsset)
						sceneTerrains = Selection.activeGameObject.GetComponentsInChildren<Ferr2DT_PathTerrain>();
					
					if (sceneTerrains != null) {
						for (int t = 0; t < sceneTerrains.Length; t++) {
							#if UNITY_2018_3_OR_NEWER
							PrefabUtility.RevertObjectOverride(sceneTerrains[t].GetComponent<MeshFilter>(), InteractionMode.AutomatedAction);
							#else
							PrefabUtility.ResetToPrefabState(sceneTerrains[t].GetComponent<MeshFilter>());
							#endif
						}
					}
				}
			}
		}
	}
}