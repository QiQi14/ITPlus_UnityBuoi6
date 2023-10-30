using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CharacterBehavior : MonoBehaviour
{
    [SerializeField]
    private CharacterStat characterStat;

    [SerializeField]
    private Transform hpBar;
    private Vector2 hpScale;

    public float currentHP;

    private void Start()
    {
        hpScale = hpBar.localScale;
        currentHP = characterStat.HP;
    }

    public void ReceiveDamage(float damage)
    {
        float estimateHP = currentHP - damage;
        if (estimateHP <= 0)
        {
            currentHP = 0;
        }
        else
        {
            currentHP = estimateHP;
        }
        float newScale = hpScale.x * (currentHP / characterStat.HP);
        hpBar.localScale = new Vector2(newScale, hpScale.y);
    }
}
