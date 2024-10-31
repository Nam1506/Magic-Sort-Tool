using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelLeft : MonoBehaviour
{
    public TMP_InputField levelInputField;

    [SerializeField] private Button saveBtn;


    private void Awake()
    {
        saveBtn.onClick.AddListener(() =>
        {
            DataManager.Instance.CheckLevelExistsToSave();
        });
    }
}
