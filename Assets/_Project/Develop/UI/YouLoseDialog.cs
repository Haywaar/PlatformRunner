using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YouLoseDialog : Dialog
{
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private TextMeshProUGUI _text;

    protected override void Awake()
    {
        base.Awake();
        _tryAgainButton.onClick.AddListener(TryAgain);
    }

    public void Init(int coinScore)
    {
        _text.text = string.Format("Better luck next time! You collected {0} coins!", coinScore);
    }

    private void TryAgain()
    {
        SceneManager.LoadScene(0);
    }
}