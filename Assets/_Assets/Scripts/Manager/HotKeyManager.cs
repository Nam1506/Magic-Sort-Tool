using UnityEngine;

public class HotKeyManager : SingletonBase<HotKeyManager>
{
    private int key0Value;
    private int keyPad0Value;

    public bool IsHoldShift {  get; private set; }

    private void Start()
    {
        key0Value = (int)KeyCode.Alpha0;
        keyPad0Value = (int)KeyCode.Keypad0;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            #region Add Delete Tube
            if (Input.GetKeyDown(KeyCode.A))
            {
                TubeManager.Instance.AddBottle();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                TubeManager.Instance.RemoveBottle(TubeManager.Instance.bottleSelected);
            }
            #endregion
            // ------------------------------------
            #region Obstacle Active
            for (int i = 1; i < 6; i++)
            {
                if (Input.GetKeyDown((KeyCode)(key0Value + i)) || Input.GetKeyDown((KeyCode)(keyPad0Value + i)))
                {
                    ToolManager.Instance.obstacleController.ChangeStateToggle(i - 1, true);
                }
            }
            #endregion

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                IsHoldShift = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            IsHoldShift = false;

        #region Obstacle UnActive
        for (int i = 1; i < 6; i++)
        {
            if (Input.GetKeyUp((KeyCode)(key0Value + i)) || Input.GetKeyUp((KeyCode)(keyPad0Value + i)))
            {
                ToolManager.Instance.obstacleController.ChangeStateToggle(i - 1, false);
            }
        }
        #endregion
    }
}
