using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopManager : MonoBehaviour
{
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
        // Nejprve smažeme staré ikony
        foreach (Transform child in partsContainer)
        {
            Destroy(child.gameObject);
        }

        // Ujistíme se, že partsContainer má GridLayoutGroup
        GridLayoutGroup grid = partsContainer.GetComponent<GridLayoutGroup>();
        if (grid == null)
        {
            grid = partsContainer.gameObject.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(100, 100);
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 4;  // 4 sloupce
            grid.spacing = new Vector2(5, 5); // mezery mezi ikonami
            grid.childAlignment = TextAnchor.UpperLeft;
        }

        // Pøidáme ikony
        foreach (var part in availableParts)
        {
            if (part.partType.ToString() == type)
            {
                Image iconObj = new GameObject("Icon").AddComponent<Image>();
                iconObj.sprite = part.icon;

                iconObj.transform.SetParent(partsContainer, false);

                RectTransform rt = iconObj.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(100, 100);
            }
        }
    }

    private void ShowDetails(BeybladePart part)
    {
        partPanel.Init(part, this);
    }
}
