using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BeybladePartIcon : MonoBehaviour
{
    public TMP_Text name;
    public Image icon;
    public Button button;


    [SerializeField]
    private BeybladePart part;

    public void Init(BeybladePart p, UIShopManager shop)
    {

    }
}
