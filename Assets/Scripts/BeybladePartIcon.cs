using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BeybladePartIcon : MonoBehaviour
{
    [SerializeField]
    private bool isOwned = false;
    public RawImage icon;
    public Button buyButton;
    public TMP_Text price;



    private BeybladePart part;

    public void Init(BeybladePart p, UIShopManager shop)
    {
        if (!isOwned)
        {
            part = p;
            price.text = part.price.ToString();
        }
        else
        {
            
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
            
        }
    }
}
