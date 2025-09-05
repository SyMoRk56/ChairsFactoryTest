using UnityEngine;

[CreateAssetMenu()]
public class EnemyScriptableObject : ScriptableObject
{
    public GameObject prefab;
    public int hp;
    public float moveSpeed = 1;
    public int moneyDrop;
    public int damage;
}
