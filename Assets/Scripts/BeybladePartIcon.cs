using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BeybladePartIcon : MonoBehaviour
{
    [SerializeField]
    private bool isOwned = false;
    [SerializeField]
    private TMP_Text partName;
    public Button buyButton;
    public TMP_Text price;



    private BeybladePart part;

    public void Init(BeybladePart p, UIShopManager shop)
    {
        part = p;

        if (!isOwned)
        {
            price.text = part.price.ToString();
            partName.text = part.partName;
        }
        else
        {
            partName.text = part.name;

        }

    }

    public void OnBuyButtonClicked()
    {
        if (GameManager.Instance.money >= part.price && !isOwned)
        {
            GameManager.Instance.money -= part.price;
            GameManager.Instance.AddOwnedPart(part);
        }
        else if (isOwned)
        {
            GameManager.Instance.EquipPart(part);
            Debug.Log("equip");
        }
    }
}
