using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField]
    private string partType;

    [SerializeField]
    private UIShopManager uiShopManager;

    public void OnClick()
    {
        uiShopManager.ShowShop(partType);
        Debug.Log("ShopButton clicked for part type: " + partType);
    }
}
