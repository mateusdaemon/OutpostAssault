using UnityEngine;
using UnityEngine.UI;

public class InputUiManager : MonoBehaviour
{
    [Header("Gameover Buttons")]
    public Button playAgainBtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupButtons();
    }

    public void SetupButtons()
    {
        // Menu Principal
        playAgainBtn.onClick.AddListener(() => GameManager.Instance.ReloadGame());
    }
}
