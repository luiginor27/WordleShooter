using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public LightsController lightsController;
    public Puzzle puzzle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.SetLevelManager(this);
            GameManager.Instance.StartLevel();
        }
    }
    
}
