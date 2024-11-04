using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotifyControl : SingletonBase<NotifyControl>
{
    public GameObject panelNotify;
    public GameObject panelConfirm;

    [Header("Panel Obstacles")]
    public HiddenTubePanel tubePanel;
    public KeyObstaclePanel keyObstaclePanel;
    [Space]

    public TextMeshProUGUI _messageConsole;
    public TMP_Text _message;
    public TMP_Text _messageConfirm;

    public Button confirmButton;

    public void NotifyConsole(string message)
    {
        _messageConsole.text = message;
        DOVirtual.DelayedCall(2, () => _messageConsole.text = "");
    }

    public void Notify(string message)
    {
        panelNotify.SetActive(true);

        _message.text = message;
    }

    public void NotifyConfirm(string message, Action action)
    {
        panelConfirm.SetActive(true);

        _messageConfirm.text = message;

        confirmButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() =>
        {
            action?.Invoke();

            CancelConfirm();
        });
    }

    public void NotifyColorHiddenTube(Action<ColorData> action)
    {
        tubePanel.gameObject.SetActive(true);

        tubePanel.OnPickColorHiddenTube = null;
        tubePanel.OnPickColorHiddenTube += action;
    }

    public void NotifySelectBottleLock(Action<int> action)
    {
        keyObstaclePanel.gameObject.SetActive(true);

        keyObstaclePanel.OnPickBottleLock = null;
        keyObstaclePanel.OnPickBottleLock += action;
    }

    public void Cancel()
    {
        panelNotify.SetActive(false);
    }

    public void CancelConfirm()
    {
        panelConfirm.SetActive(false);
    }

}
