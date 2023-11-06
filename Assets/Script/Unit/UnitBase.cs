using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public TileNode occupiedTile;
    public Faction faction;
    private float moveSpeed = 1f;

    public void Move(Path path)
    {
        occupiedTile.occupiedUnit = null;
        StartCoroutine(MoveAlongPath(path));
    }

    IEnumerator MoveAlongPath(Path path)
    {
        int currentStep = 0;
        int pathLength = path.tiles.Length - 1;
        TileNode currentTile = path.tiles[currentStep];
        float animationTime = 0f;

        while (currentStep <= pathLength)
        {
            yield return null;

            Vector3 nextTilePosition = path.tiles[currentStep].transform.position;
            float movementTime = animationTime / (moveSpeed + path.tiles[currentStep].tileData.terrainCost);
            MoveAndRotate(currentTile.transform.position, nextTilePosition, movementTime);
            animationTime += Time.deltaTime;

            if (Distance(transform.position, nextTilePosition) > 0.05f)
                continue;

            currentTile = path.tiles[currentStep];
            currentStep++;
            animationTime = 0f;
        }
    }

    private void MoveAndRotate(Vector3 origin, Vector3 target, float duration)
    {
        transform.position = Vector3.Lerp(origin, target, duration);

        Vector2 targetDirection = target - origin;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90F;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, 5f);
    }

    private float Distance(Vector3 a, Vector3 b)
    {
        float x = a.x - b.x;
        float y = a.y - b.y;
        float z = a.z - b.z;
        return Mathf.Sqrt(x * x + y * y + z * z);
    }
}
