using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpanwDataListSO", menuName = "SO/EnemySpanw/EnemySpanwDataListSO")]
public class EnemySpawnDataListSO : ScriptableObject
{
    public List<EnemySpawnDataSO> Stages;
}
