using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public List<BeybladePart> ownedParts = new List<BeybladePart>();
    public int money = 100;

    [SerializeField]
    private BeybladePart equippedBit;
    [SerializeField]
    private BeybladePart equippedBlade;
    [SerializeField]
    private BeybladePart equippedRatchet;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddOwnedPart(BeybladePart part)
    {
        if (!ownedParts.Contains(part))
        {
            ownedParts.Add(part);
        }
    }

    public void EquipPart(BeybladePart part)
    {
        switch (part.partType)
        {
            case PartType.Bit:
                equippedBit = part;
                break;
            case PartType.Blade:
                equippedBlade = part;
                break;
            case PartType.Ratchet:
                equippedRatchet = part;
                break;
        }
    }

    public BeybladePart[] AddParts()
    {
        equippedBit.AddPrefab();
        equippedBlade.AddPrefab();
        equippedRatchet.AddPrefab();
        return new[] { equippedBit, equippedBlade, equippedRatchet };
    }
}
