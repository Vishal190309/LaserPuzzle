using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDataSO : ScriptableObject
{
   public List<ObjectData> placementObjects;
}

[Serializable]
public class ObjectData
{
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public PlacableObject placementObject { get; private set; }
}
