using System;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leveuUpTxt;

    private void OnEnable()
    {
        PlayerEvents.OnLevelUp += LevelUpPlayer;   
    }

    private void LevelUpPlayer()
    {
        leveuUpTxt.enabled = true;
    }

    private void OnDisable()
    {
        PlayerEvents.OnLevelUp -= LevelUpPlayer;
    }
}
