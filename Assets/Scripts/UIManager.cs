using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private ArenaManager arenaManager;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private Image countdownImage;
   
    [SerializeField]
    private Sprite[] countdownSprites;

    [SerializeField] 
    private Image gameoverImage;


    [SerializeField]
    private Sprite gameoverSprite;

    void Awake()
    {
        arenaManager.ChangeCountdown += ChangeCountdownSprite;
        playerMovement.ShowGameover += ChangeGameoverSprite;
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

    private void ChangeGameoverSprite()
    {
        Debug.Log("change sprite");
        gameoverImage.enabled = true;
        gameoverImage.sprite = gameoverSprite;
        
    }


    
}
