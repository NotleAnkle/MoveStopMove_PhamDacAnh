using _UI.Scripts;
using _UI.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRank : UICanvas
{
    [SerializeField] private Text txtRank;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtCoin;

    [SerializeField] private GameObject panelRank;
    [SerializeField] private GameObject panelVictory;

    [SerializeField] private Button btnTriple;

    private GameResult gameResult;

    private void OnEnable()
    {
        txtCoin.text = LevelManager.Instance.Player.Score.ToString();
        UIManager.Instance.CloseUI<UIPlay>();
        btnTriple.interactable = true;
    }

    public void OnVictory()
    {
        gameResult = GameResult.Win;
        panelRank.SetActive(false);
        panelVictory.SetActive(true);
        SoundManager.Instance.Play(AudioType.SFX_EndWin);
    }
    public void OnFail()
    {
        gameResult = GameResult.Lose;
        SoundManager.Instance.Play(AudioType.SFX_EndLose);
        panelRank.SetActive(true);
        panelVictory.SetActive(false);
        txtRank.text = "#" + LevelManager.Instance.PlayerRank.ToString();
        txtName.text = LevelManager.Instance.Player.KillerName.ToString();
    }

    public void OnTouch()
    {
        UIManager.Instance.CloseUI<UIRank>();
        UIManager.Instance.OpenUI<UIMainMenu>();
        LevelManager.Instance.OnReset();
        if (gameResult == GameResult.Win)
        {
            LevelManager.Instance.OnNextLevel();
        }
    }

    public void OnTripleButtonClick()
    {
        //+ Xem quang cao
        LevelManager.Instance.Player.TripleScore();
        txtCoin.text = LevelManager.Instance.Player.Score.ToString();
        btnTriple.interactable = false;
    }
}
