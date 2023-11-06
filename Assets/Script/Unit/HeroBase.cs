using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : UnitBase
{
    [SerializeField]
    private GameObject highlight;

    public void SetSelected()
    {
        highlight.SetActive(true);
    }

    public void ClearSelected()
    {
        highlight.SetActive(false);
    }
}
