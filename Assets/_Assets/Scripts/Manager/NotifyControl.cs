using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotifyControl : SingletonBase<NotifyControl>
{
    public GameObject panelNotify;
    public GameObject panelConfirm;

    public TMP_Text _message;
    public TMP_Text _messageConfirm;

    public Button confirmButton;

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

    public void Cancel()
    {
        panelNotify.SetActive(false);
    }

    public void CancelConfirm()
    {
        panelConfirm.SetActive(false);
    }

}
