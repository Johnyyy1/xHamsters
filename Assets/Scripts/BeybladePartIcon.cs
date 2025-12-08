using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BeybladePartIcon : MonoBehaviour
{
    public RawImage icon;
    public Button button;
    public TMP_Text price;



    private BeybladePart part;

    public void Init(BeybladePart p, UIShopManager shop)
    {
        part = p;
        price.text = part.price.ToString();
    }
}
