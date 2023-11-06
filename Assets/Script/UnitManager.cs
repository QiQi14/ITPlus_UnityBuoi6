using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private List<UnitSO> listUnit;
    private void Awake()
    {
        listUnit = Resources.LoadAll<UnitSO>("Unit").ToList();
        GameManager.OnGameStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState state)
    {
        if (state == GameState.SpawnUnit)
        {
            spawnUnit();
        }
    }

    private void spawnUnit()
    {
        for (int i = 0; i < listUnit.Count; i++)
        {
            var spawnedUnit = Instantiate(listUnit[i].unitPrefab);

            TileNode randomSpawnTile = GridManager.instance.GetRandomTile();

            spawnedUnit.transform.position = new Vector3(randomSpawnTile.transform.position.x, randomSpawnTile.transform.position.y, 0f);
            spawnedUnit.occupiedTile = randomSpawnTile;
            spawnedUnit.faction = listUnit[i].faction;
            randomSpawnTile.occupiedUnit = spawnedUnit;
        }
    }
}

public enum Faction {
    Player,
    Enemy
}
