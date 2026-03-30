using UnityEngine;

public class BeybladePart : MonoBehaviour
{
    public PartType partType;


    public string partName;
    public float damageMult =0;

    public float maxSpeed=0;

    public float acceleration=0;
    public int hp=0;
    public int price;

    public float knockback;

    public GameObject prefab;

    public Sprite icon;

    public void AddPrefab()
    {
        prefab = this.gameObject;
    }

}

public enum PartType
{
    Bit,
    Blade,
    Ratchet
}