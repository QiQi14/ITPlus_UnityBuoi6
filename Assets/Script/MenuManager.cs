using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuContainer;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged; //subscribe
    }

    private void OnGameStateChanged(GameState state)
    {
        Debug.Log(state.ToString());
        menuContainer.SetActive(state == GameState.Menu);
    }

    public void OnClickStart()
    {
        GameManager.instance.UpdateGameState(GameState.PlayerTurn);
        AudioManager.instance.PlayBGM("BGM1");
    }
}
