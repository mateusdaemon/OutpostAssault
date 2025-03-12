using System;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance { get; private set; }

    [Header("Gameplay UI")]
    [SerializeField] GameObject gameplayPanel;
    [SerializeField] private TextMeshProUGUI lifeAmount;

    [Header("Gameover UI")]
    [SerializeField] GameObject gameoverPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SetBaseLife(int baseLives)
    {
        lifeAmount.text = baseLives.ToString();
    }

    public void GameOver()
    {
        gameplayPanel.SetActive(false);
        gameoverPanel.SetActive(true);
    }

    public void ResetGame()
    {
        gameplayPanel.SetActive(true);
        gameoverPanel.SetActive(false);
    }


}
