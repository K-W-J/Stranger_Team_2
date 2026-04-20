using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyData
{
    public int cnt;
    public EnemyUnit enemy;
}
[CreateAssetMenu(fileName = "EnemySpanwDataSO",menuName = "SO/EnemySpanw/EnemySpanwDataSO")]
public class EnemySpawnDataSO : ScriptableObject
{
    public List<EnemyData> enemys;
}
