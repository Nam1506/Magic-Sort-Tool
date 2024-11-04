using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class KeyObstaclePanel : MonoBehaviour
{
    [SerializeField] Transform tfIdContainer;
    [SerializeField] Button cancelButton;
    [SerializeField] Button idPrefab;

    public Action<int> OnPickBottleLock;
    private List<Button> listButton = new();
    private Queue<Button> buttonQueue = new();

    private void Start()
    {
        cancelButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void CreateNewButton()
    {
        Button btn = Instantiate(idPrefab, tfIdContainer);
        buttonQueue.Enqueue(btn);
    }

    private void OnEnable()
    {
        List<int> listId = ToolManager.Instance.obstacleController.GetListIDBottleLock();

        foreach (int id in listId)
        {
            if (buttonQueue.Count == 0)
                CreateNewButton();

            Button btn = buttonQueue.Dequeue();
            btn.gameObject.SetActive(true);
            btn.onClick.AddListener(() => {OnClickID(id); });
            btn.GetComponentInChildren<TextMeshProUGUI>().text = id.ToString("D2");

            listButton.Add(btn);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < listButton.Count; i++)
        {
            listButton[i].onClick.RemoveAllListeners();
            listButton[i].gameObject.SetActive(false);
            buttonQueue.Enqueue(listButton[i]);
        }
        listButton.Clear();
    }

    private void OnClickID(int idBottle)
    {
        OnPickBottleLock?.Invoke(idBottle);
        gameObject.SetActive(false);
    }
}
