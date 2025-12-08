using UnityEngine;

public class BeybladePart : MonoBehaviour
{
    public PartType partType;


    public string name;
    public float damageMult;

    public float maxSpeed;

    public float acceleration;
    public int hp;
    public int price;

    public float knockback;


    public Sprite icon;

}

public enum PartType
{
    Bit,
    Blade,
    Ratchet
}