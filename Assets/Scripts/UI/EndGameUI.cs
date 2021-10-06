using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EndGameUI : MonoBehaviour
{
    public GameObject UIPanel;
    public TextMeshProUGUI WinLoseTxt;
    public Color WinColor;
    public Color LoseColor;

    private void Start()
    {
        GameEvents.Instance.OnDefeatTriggerTerritory += OnFinishGame;
        UIPanel.SetActive(false);
    }

    public void FinishGame(bool win)
    {
        if (win)
        {
            UIPanel.SetActive(true);
            WinLoseTxt.text = "You Win!";
            WinLoseTxt.color = WinColor;
        }
        else
        {
            UIPanel.SetActive(true);
            WinLoseTxt.text = "You Lose!";
            WinLoseTxt.color = LoseColor;
        }
    }

    private void OnFinishGame()
    {
        if (GamePlayManager.Grid.IsAllMapOfType(TerritoryType.Player))
        {
            FinishGame(true);
        }
        else if (GamePlayManager.Grid.IsAllMapOfType(TerritoryType.Enemy))
        {
            FinishGame(false);
        }
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnDefeatTriggerTerritory -= OnFinishGame;
    }

}
