using System;
using System.Collections.Generic;
using System.Collections;
using _01_Work.HS.Core.GameManagement;
using UnityEngine;
using _01_Work.HS.Core;
using _01_Work.LCM._01.Scripts.Day;

public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
{
    [SerializeField] private EnemySpawnDataListSO _enemySpawnDataList;
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();

    float _waitMinTime = 10f;
    float _waitMaxTime = 15f;

    int stage = 0;

    private void Start()
    {
        DayManager.Instance.OnChangeNight += RandomWaitEnemySpawn;
    }

    private void RandomWaitEnemySpawn() // �̰� ȣ���ϸ� �� 
    {
        if (DayManager.Instance.CurrentDay < 3) return;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_waitMinTime, _waitMaxTime));
        EnemySpawn(stage);
        stage++;
    }

    private void EnemySpawn(int stage) // 1~10 ���� ���� ���� �ö󰥼��� ���̵� �ö�   1�� �� 4���� 10�� �� 30���� 
    {                                     // �ʹ� ���̵� �޻���ϸ� �ȵǴϱ� ���� �ܰ� ������ �ҷ��� �� �� 
        SmallAlarmChat.Instance.AddChatMessage(
            $"어디선가 <color=red>적들</color>이 몰려오고 있습니다. 어서 막을 준비를 하세요.");
        
        stage = Mathf.Clamp(stage, 0, _enemySpawnDataList.Stages.Count - 1);

        foreach (EnemyData enemySpawnData in _enemySpawnDataList.Stages[stage].enemys)
        {
            for (int i = 0; i < enemySpawnData.cnt; i++)
            {
                EnemyUnit enemy = Instantiate(enemySpawnData.enemy);
                enemy.SetTargetBuildObj(GameManager.Instance.Castle);
                enemy.transform.position = GetRandomSpawnPoint();
            }
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        return _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)].position;
    }
}
