using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopManager : MonoBehaviour
{
    [SerializeField]
    private bool isLocker = false;

    [SerializeField]
    private List<BeybladePart> availableParts;

    [SerializeField]
    private Transform partsContainer;

    [SerializeField]
    private BeybladePartIcon partPanel;
    private string type;



    private void Start()
    {
    }



    public void ShowShop(string type)
    {
        if (isLocker)
        {
            availableParts = GameManager.Instance.ownedParts;
        }
        else
        {
            availableParts = GameManager.Instance.availableParts;
        }


        foreach (Transform child in partsContainer)
        {
            Destroy(child.gameObject);
        }


        GridLayoutGroup grid = partsContainer.GetComponent<GridLayoutGroup>();
        if (grid == null)
        {
            grid = partsContainer.gameObject.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(80, 80);
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 5; 
            grid.spacing = new Vector2(10, 10); 
            grid.childAlignment = TextAnchor.UpperLeft;
        }


        foreach (var part in availableParts)
        {
            if (part.partType.ToString() == type)
            {
                GameObject buttonGO = new GameObject("PartButton", typeof(RectTransform), typeof(Image), typeof(Button));
                buttonGO.transform.SetParent(partsContainer, false);

                RectTransform rt = buttonGO.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(160, 40);

                Image bg = buttonGO.GetComponent<Image>();
                bg.color = Color.white;

                Button buttonObj = buttonGO.GetComponent<Button>();

                GameObject textObj = new GameObject("Text", typeof(RectTransform));
                textObj.transform.SetParent(buttonGO.transform, false);

                TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
                tmp.text = part.partName;
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.color = Color.black;
                tmp.fontSize = 24;

                RectTransform textRT = textObj.GetComponent<RectTransform>();
                textRT.anchorMin = Vector2.zero;
                textRT.anchorMax = Vector2.one;
                textRT.offsetMin = Vector2.zero;
                textRT.offsetMax = Vector2.zero;

                buttonObj.onClick.AddListener(() => ShowDetails(part));
            }
        }
    }

    private void ShowDetails(BeybladePart part)
    {
        partPanel.Init(part, this);
    }
}
