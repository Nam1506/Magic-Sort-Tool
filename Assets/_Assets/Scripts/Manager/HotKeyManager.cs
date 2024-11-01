using UnityEngine;

public class HotKeyManager : MonoBehaviour
{
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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ToolManager.Instance.obstacleController.isHiddenLayer = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ToolManager.Instance.obstacleController.isHiddenTube = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ToolManager.Instance.obstacleController.isLock = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ToolManager.Instance.obstacleController.isKey = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ToolManager.Instance.obstacleController.isTap = true;
            }
            #endregion
        }

        #region Obstacle UnActive
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            ToolManager.Instance.obstacleController.isHiddenLayer = false;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            ToolManager.Instance.obstacleController.isHiddenTube = false;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            ToolManager.Instance.obstacleController.isLock = false;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            ToolManager.Instance.obstacleController.isKey = false;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            ToolManager.Instance.obstacleController.isTap = false;
        }
        #endregion
    }
}
