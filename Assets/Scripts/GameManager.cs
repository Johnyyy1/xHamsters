using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<BeybladePart> ownedParts = new List<BeybladePart>();

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
}
