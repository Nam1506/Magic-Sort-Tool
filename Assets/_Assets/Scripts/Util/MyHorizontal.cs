using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MyHorizontal : MonoBehaviour
{
    [SerializeField]
    private HorizontalLayoutGroup horizontalLayoutGroup;
  

    public Canvas canva;

    public List<GameObject> listActive = new();
    public List<GameObject> listObject;

    private const float MAX_SPACING_X = 120f;
    private const float MIN_SPACING_X = 30f;

    public int numTube;
    public int maxTube;
    public Vector3 scaleOffset;
    public float spacingOffset;

    private void Reset()
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    private void Start()
    {
        Calculate();
    }

    public void Calculate()
    {
        var rectTransform = GetComponent<RectTransform>();
        horizontalLayoutGroup.spacing = MIN_SPACING_X;

        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

        var screenWidth = (Screen.width / canva.scaleFactor) - 60;

        var width = rectTransform.rect.width;

        scaleOffset = Vector3.one * Mathf.Min(1f, screenWidth / width);
        rectTransform.localScale = scaleOffset;

        if (screenWidth < width) return;

        var remainingSpace = screenWidth - width;

        spacingOffset = Mathf.Min(remainingSpace / (maxTube - 1), MAX_SPACING_X);
        spacingOffset = Mathf.Max(spacingOffset, MIN_SPACING_X);
        horizontalLayoutGroup.spacing = spacingOffset;
    }

    public void SetTube(int numTube, int maxTube)
    {
        this.maxTube = Mathf.Min(maxTube, GameDefine.MAX_TUBE_OF_ROW);
        numTube = Mathf.Min(numTube, GameDefine.MAX_TUBE_OF_ROW);
        this.numTube = numTube;

        if (listActive.Count == numTube)
            return;

        if (listActive.Count < numTube)
        {
            while (listActive.Count != numTube)
            {
                GameObject go = listObject[listActive.Count];
                go.SetActive(true);
                listActive.Add(go);
            }
        }
        else
        {
            while (listActive.Count != numTube)
            {
                GameObject go = listActive.Last();
                go.SetActive(false);

                listActive.RemoveAt(listActive.Count - 1);
            }
        }

        Calculate();
    }

    public Vector3 GetPosition(int col)
    {
        return listActive[col].transform.position;
    }
}