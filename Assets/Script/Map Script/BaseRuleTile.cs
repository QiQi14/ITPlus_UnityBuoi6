using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Rule Tile", menuName = "Tiles/Rule Tile")]
public class BaseRuleTile : RuleTile
{
    public bool Walkable;
    public TileType TileType;
    public int TerrainCost;
    
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
    }
}

[SerializeField]
public enum TileType {
    Grass,
    Forest,
    Water,
    Mountain
}
