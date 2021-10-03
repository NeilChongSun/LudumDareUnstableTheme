using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Stage")]
public class StageInformation : ScriptableObject
{
    public float enemyCount;
    public int stageTime;
    public GameObject[] enemyPrefabs;
}
