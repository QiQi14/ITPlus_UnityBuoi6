using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private Pathfinder pathfinder;

    public static CombatManager instance;

    public TileNode currentTile;
    private HeroBase selectedCharacter;

    private void Awake()
    {
        instance = this;
    }

    public void inspectTile()
    {
        if (currentTile.occupiedUnit != null)
        {
            if (currentTile.occupiedUnit.faction != Faction.Player) return;
            if (selectedCharacter == null) selectedCharacter = (HeroBase)currentTile.occupiedUnit;
            inspectCharacter();
        } else
        {
            navigateToTile();
        }
    }

    public void clear()
    {
        if (currentTile == null || selectedCharacter == null)
            return;

        selectedCharacter.ClearSelected();
        currentTile = null;
    }

    public void inspectCharacter()
    {
        selectedCharacter.SetSelected();
    }

    public void navigateToTile()
    {
        if (selectedCharacter == null)
            return;

        if (retrievePath(out Path newPath))
        {
            selectedCharacter.Move(newPath);
            selectedCharacter = null;
        }
    }

    private bool retrievePath(out Path path)
    {
        path = pathfinder.FindPath(selectedCharacter.occupiedTile, currentTile);

        if (path == null)
            return false;

        return true;
    }
}
