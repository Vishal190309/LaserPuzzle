using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlacementSystem))]
public class PlaceSystemScriptEditor : Editor
{
   public override void OnInspectorGUI()
    {
        PlacementSystem placementSystem = (PlacementSystem)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Add Allies"))
        {
            placementSystem.placeObjectsLocations.Clear();
            GameObject[] listOfAllies = GameObject.FindGameObjectsWithTag("Allies");
            foreach (GameObject go in listOfAllies)
            {
                placementSystem.placeObjectsLocations.Add(go.transform);
            }
            EditorUtility.SetDirty(target);
        }

        
    }
    
}
