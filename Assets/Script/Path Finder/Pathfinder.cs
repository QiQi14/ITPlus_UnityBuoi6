using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Path FindPath(TileNode origin, TileNode target)
    {
        List<TileNode> openSet = new List<TileNode>();
        List<TileNode> closedSet = new List<TileNode>();

        openSet.Add(origin);
        origin.tileData.costFromOrigin = 0;
        origin.tileData.costToDestination = CalculateDistanceCost(origin, target);

        while (openSet.Count > 0)
        {
            openSet.Sort((x, y) => x.tileData.totalCost.CompareTo(y.tileData.totalCost));
            TileNode currentTile = openSet[0];

            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            if (currentTile == target)
            {
                return PathBetween(target, origin);
            }

            foreach (TileNode neighborNode in NeighborTiles(currentTile))
            {
                if (closedSet.Contains(neighborNode))
                    continue;

                float costToNeighbor = currentTile.tileData.costFromOrigin + neighborNode.tileData.terrainCost + currentTile.tileData.costToDestination;
                if (costToNeighbor < neighborNode.tileData.costFromOrigin || !openSet.Contains(neighborNode))
                {
                    neighborNode.tileData.costFromOrigin = costToNeighbor;
                    neighborNode.tileData.costToDestination = CalculateDistanceCost(neighborNode, target);
                    neighborNode.previousTile = currentTile;

                    if (!openSet.Contains(neighborNode))
                    {
                        openSet.Add(neighborNode);
                    }
                }
            }
        }

        return null;
    }

    public int CalculateDistanceCost(TileNode a, TileNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remain = Mathf.Abs(xDistance - yDistance);
        return Mathf.Min(xDistance, yDistance) + 10 * remain;
    }

    public List<TileNode> NeighborTiles(TileNode origin)
    {
        List<TileNode> tiles = new List<TileNode>();
        GridManager grid = GridManager.instance;

        //Left Down
        if (grid.GetTile(origin.x - 1, origin.y - 1, out TileNode tileLD))
            tiles.Add(tileLD);
        //Left Up
        if (grid.GetTile(origin.x, origin.y - 1, out TileNode tileLU))
            tiles.Add(tileLU);

        //Right Down
        if (grid.GetTile(origin.x - 1, origin.y + 1, out TileNode tileRD))
            tiles.Add(tileRD);

        //Right Up
        if(grid.GetTile(origin.x, origin.y + 1, out TileNode tileRU))
            tiles.Add(tileRU);

        //Up

        if (grid.GetTile(origin.x + 1, origin.y , out TileNode tileU))
            tiles.Add(tileU);
        //Down
        if (grid.GetTile(origin.x - 1, origin.y, out TileNode tileD))
            tiles.Add(tileD);

        return tiles;
    }

    public Path PathBetween(TileNode target,  TileNode origin)
    {
        Path path = new Path();
        List<TileNode> tiles = new List<TileNode>();
        TileNode currentTile = target;

        while (currentTile != origin)
        {
            tiles.Add(currentTile);
            if (currentTile.previousTile != null)
                currentTile = currentTile.previousTile;
            else
                break;
        }

        tiles.Add(origin);
        tiles.Reverse();

        path.tiles = tiles.ToArray();

        return path;
    }
}
