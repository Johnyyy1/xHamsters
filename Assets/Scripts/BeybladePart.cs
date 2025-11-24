using UnityEngine;

public class BeybladePart : MonoBehaviour
{
    public PartType partType;

    public string name { get; private set; }
    public float damageMult {get; private set;}
    public float maxSpeed { get; private set; }
    public float acceleration { get; private set; }
    public int hp { get; private set; }
    public float knockback { get; private set; }


}

public enum PartType
{
    Bit,
    Blade,
    Ratchet
}