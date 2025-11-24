using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private ArenaManager arenaManager;

    [SerializeField]
    private Image countdownImage;

    [SerializeField]
    private Sprite[] countdownSprites;

    void Awake()
    {
        arenaManager.ChangeCountdown += ChangeCountdownSprite;
    }

    private void ChangeCountdownSprite(int spriteNumber)
    {
        if (spriteNumber == -1)
        {
            countdownImage.enabled = false;
        }
        else
        {
            if(spriteNumber == 0)
            {
                countdownImage.enabled = true;
            }
            countdownImage.sprite = countdownSprites[spriteNumber];
        }
    }


    
}
