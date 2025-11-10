using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SwitchToArenaScene()
    {
        SceneManager.LoadScene("xHamsters");
    }
    public void SwitchToShop()
    {
        SceneManager.LoadScene("ShopScnee");
    }
    public void ExitGame()
    {
        Application.Quit(); // pokud hrajes v buildu

        UnityEditor.EditorApplication.isPlaying = false; // pokud hrajes v editoru (Play Mode)
    }
}
