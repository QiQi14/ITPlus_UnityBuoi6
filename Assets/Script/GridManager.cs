using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tileMap;
    [SerializeField]
    private TextMeshProUGUI tileInfo;

    private Dictionary<Vector2, TileNode> tileList;
    private int width, height;

    public static GridManager instance;

    private void Awake()
    {
        instance = this;
        GameManager.OnGameStateChanged += OnGameStateChanged; //subscribe
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged; //unsubscribe
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.GenerateMap)
        {
            MapRuleTileToTile();
            tileInfo.text = "Start";
        }
    }

    private void MapRuleTileToTile()
    {
        Vector3Int min =  tileMap.cellBounds.min;
        Vector3Int max = tileMap.cellBounds.max;

        width = Mathf.Abs(max.x - min.x);
        height = Mathf.Abs(min.y - min.y);
        tileList = new Dictionary<Vector2, TileNode>();
        foreach (var position in tileMap.cellBounds.allPositionsWithin)
        {
            BaseRuleTile ruleTile = (BaseRuleTile) tileMap.GetTile(position);

            var spawnedObject = tileMap.GetInstantiatedObject(position);
            if (spawnedObject == null) continue;

            var spawnedTile = spawnedObject.GetComponent<TileNode>();

            if (spawnedTile != null)
            {
                tileList[new Vector2(position.x, position.y)] = spawnedTile;
                spawnedTile.name = $"{position.x} {position.y} {ruleTile.name}";
                spawnedTile.Walkable = ruleTile.Walkable;
                spawnedTile.x = position.x;
                spawnedTile.y = position.y;

                TileDataSet tileData = new TileDataSet();
                tileData.terrainCost = ruleTile.TerrainCost;
                spawnedTile.tileData = tileData;
            }
        }

        GameManager.instance.UpdateGameState(GameState.SpawnUnit);
    }

    public bool GetTile(int x, int y, out TileNode newTile)
    {
        newTile = null;
        if (tileList.TryGetValue(new Vector2(x, y), out TileNode tile)) { 
            newTile = tile;
            return true;
        }

        return false;
    }

    public TileNode GetRandomTile()
    {
        return tileList.OrderBy(t => Random.value).First().Value;
    }

    public TileNode farthestToLeft()
    {
        return tileList.OrderBy(t => t.Key.x).First().Value;
    }

    public TileNode farthestToRight()
    {
        return tileList.OrderBy(t => t.Key.x).Last().Value;
    }

    public TileNode farthestToBottom()
    {
        return tileList.OrderBy(t => t.Key.y).First().Value;
    }

    public TileNode farthestToTop()
    {
        return tileList.OrderBy(t => t.Key.y).Last().Value;
    }
}
