using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    [SerializeField] private List<MyHorizontal> listHorizontal;
    public int numRow = 1;
    public int maxCol = 1;
    public int numColAdd = 0;
    public float scaleSize = 1f;
    public bool[,] marks = new bool[2, GameDefine.MAX_TUBE_OF_ROW];

    public void SetGrid(int numtube)
    {
        ResetData();

        numRow = (numtube < 6) ? 1 : 2;
        int numTubeOfFirstRow = (numRow == 1) ? numtube : Mathf.RoundToInt(numtube / 2f);
        maxCol = numTubeOfFirstRow;
        listHorizontal[0].SetTube(numTubeOfFirstRow, numTubeOfFirstRow);

        if (numRow == 2)
        {
            listHorizontal[1].gameObject.SetActive(true);
            listHorizontal[1].SetTube(numtube - numTubeOfFirstRow, numTubeOfFirstRow);
        } else
        {
            listHorizontal[1].gameObject.SetActive(false);
        }

        UpdateScaleSize();
    }

    public void AddNewTube(int numTubeNeed)
    {
        if (numTubeNeed >= 6 && numRow == 1)
        {
            SetGrid(numTubeNeed);
            return;
        }

        int totalTube = 0;
        for (int i = 0; i < numRow; i++)
            totalTube += listHorizontal[i].numTube;

        if (totalTube >= numTubeNeed)
            return;

        for (int i = numRow - 1; i >= 0; i--)
        {
            if (listHorizontal[i].numTube < listHorizontal[i].maxTube)
            {
                listHorizontal[i].SetTube(listHorizontal[i].numTube + 1, listHorizontal[i].maxTube);
                return;
            }
        }

        numColAdd++;
        maxCol = listHorizontal[0].maxTube + 1;

        for (int i = 0; i < listHorizontal.Count; i++)
        {
            listHorizontal[i].SetTube(maxCol, maxCol);
        }

        UpdateScaleSize();
    }


    public Vector3 GetPosition(int indexTube)
    {
        int counter = 0;
        for (int i = 0; i < numRow; i++)
        {
            for (int j = 0; j < listHorizontal[i].numTube - numColAdd; j++)
            {
                if (counter == indexTube)
                    return GetPosition(i, j);
                counter++;
            }
        }

        for (int j = 0; j < numColAdd; j++)
        {
            for (int i = 0; i < numRow; i++)
            {
                if (counter == indexTube)
                    return GetPosition(i, listHorizontal[i].numTube - numColAdd + j);
                counter++;
            }
        }

        return Vector3.zero;
    }

    private Vector3 GetPosition(int row, int col)
    {
        marks[row, col] = true;
        return listHorizontal[row].GetPosition(col);
    }

    private void UpdateScaleSize()
    {
        scaleSize = listHorizontal[0].scaleOffset.x;

        for (int i = 1; i < numRow; i++)
        {
            if (scaleSize > listHorizontal[i].scaleOffset.x)
            {
                scaleSize = listHorizontal[i].scaleOffset.x;
            }
        }
    }

    private void ResetData()
    {
        for (int i = 0; i < marks.GetLength(0); i++)
        {
            for (int j = 0; j < marks.GetLength(1); j++)
            {
                marks[i, j] = false;
            }
        }

        numColAdd = 0;
    }
}
