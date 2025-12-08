using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShopManager : MonoBehaviour
{
    [SerializeField]
    private List<BeybladePart> availableParts;

    [SerializeField]
    private Transform partsContainer;



    private void Start()
    {
        
    }

    private void ShowShop()
    {
        foreach (var part in availableParts)
        {
            //vypsani
        }
    }

    private void ShowDetails(BeybladePart part)
    {

    }
}
