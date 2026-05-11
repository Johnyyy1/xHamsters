using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    int lastScene = 0;


    public void Respawn()
    {
        if (lastScene == 0)
        {
            SwitchToArenaScene();

        }
        else if (lastScene == 1)
        {
            SwitchToArena2Scene();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SwitchToArenaScene()
    {
        SceneManager.LoadScene("xHamsters");
    }

    public void SwitchToArena2Scene()
    {
        SceneManager.LoadScene("Arena2");
    }
    public void SwitchToShop()
    {
        SceneManager.LoadScene("UpdatedShopScene");
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SwitchToInventory()
    {
        SceneManager.LoadScene("LockerScene");
    }

    public void SwitchToArenaSelection()
    {
        SceneManager.LoadScene("ArenaSelection");
    }

    public void SwitchToDeathScreen(int scene) {
        lastScene = scene;
        SceneManager.LoadScene("DeathScreen");
    }

    public void SwitchToWinScreen() {
        SceneManager.LoadScene("WinScreen");
    }
    public void ExitGame()
    {
        Application.Quit(); // pokud hrajes v buildu

        UnityEditor.EditorApplication.isPlaying = false; // pokud hrajes v editoru (Play Mode)
    }
}
