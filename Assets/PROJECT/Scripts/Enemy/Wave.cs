using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Wave : ScriptableObject
{
    public List<EnemyScriptableObject> enemies = new();
    public List<float> delays = new();
}
