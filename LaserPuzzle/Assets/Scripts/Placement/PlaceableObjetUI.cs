using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaceableObjetUI : MonoBehaviour
{
    [SerializeField]
    private PlacementSystem placementSystem;
    [SerializeField]
    private int id;
    [SerializeField]
    private TextMeshProUGUI count;

    private void Start()
    {
        SetNoOfObjectsAvailable(placementSystem.GetAvailabelObjectCount(id));
        LevelManager.Instance.OnObjectCountUpdated += UpdateCount;
    }

    private void UpdateCount(int objectId)
    {
        if(objectId == id)
        {
            SetNoOfObjectsAvailable(placementSystem.GetAvailabelObjectCount(id));
        }
    }

    void SetNoOfObjectsAvailable(int remaining)
    {
        count.SetText( remaining.ToString());
    }
}
