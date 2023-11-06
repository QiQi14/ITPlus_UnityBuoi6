using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuContainer;
    [SerializeField]
    private GameObject mainMenuContainer;
    [SerializeField]
    private GameObject optionMenuContainer;
    [SerializeField]
    private GameObject Background;
    [SerializeField] 
    private Slider bgmSlider;


    [SerializeField]
    private GameObject endGameUIContainer;
    [SerializeField]
    private TextMeshProUGUI endGameText;

    [SerializeField]
    private GameObject combatUI;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged; //subscribe
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged; //unsubscribe
    }

    private void OnGameStateChanged(GameState state)
    {
        Debug.Log(state.ToString());
        menuContainer.SetActive(state == GameState.Menu);
        Background.SetActive(state == GameState.Menu || state == GameState.Victory || state == GameState.Defeat);

        if (state == GameState.Victory || state == GameState.Defeat)
        {
            handleEndGame(state == GameState.Victory);
        }
    }

    public void OnClickStart()
    {
        GameManager.instance.UpdateGameState(GameState.GenerateMap);
        combatUI.SetActive(true);
    }

    public void OnClickOption()
    {
        mainMenuContainer.SetActive(false);
        optionMenuContainer.SetActive(true);
    }

    public void OnClickBack()
    {
        mainMenuContainer.SetActive(true);
        optionMenuContainer.SetActive(false);
    }

    public void BGMVolume()
    {
        AudioManager.instance.BGMVolume(bgmSlider.value);
    }

    public void OnClickPlayAgain()
    {
        endGameUIContainer.SetActive(false); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void handleEndGame(bool isVictory)
    {
        endGameText.text = isVictory ? "Victory" : "Defeat";
        endGameUIContainer.SetActive(true);
    }
}
