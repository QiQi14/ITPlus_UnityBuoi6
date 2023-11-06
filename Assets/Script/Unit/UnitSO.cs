using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class UnitSO : ScriptableObject
{
    public Faction faction;
    public UnitBase unitPrefab;
}
