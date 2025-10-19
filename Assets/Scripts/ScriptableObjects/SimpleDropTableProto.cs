using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="table")]
public class SimpleDropTableProto : ScriptableObject
{
    [SerializedDictionary("Prefab", "Drop Rate")]
    public SerializedDictionary<GameObject, float> Table;
}
