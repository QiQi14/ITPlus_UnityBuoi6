using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState gameState;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        UpdateGameState(GameState.Menu);
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;
        OnGameStateChanged?.Invoke(gameState);
    }
}

public enum GameState {
    Menu,
    PlayerTurn,
    EnemyTurn
}
