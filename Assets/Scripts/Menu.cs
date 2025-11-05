using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class GameMode
{
    public static bool IsCoop = false;
}
public class Menu : MonoBehaviour
{
    Button singleplayerButton;
    Button coopButton;
    Button creditsButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        singleplayerButton.onClick.AddListener(PlaySinglePlayer);
        coopButton.onClick.AddListener(PlayCoop);
        creditsButton.onClick.AddListener(OpenCredits);
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
    public void PlaySinglePlayer()
    {
        GameMode.IsCoop = false;
        SceneManager.LoadScene("Map1");
    }
    public void PlayCoop()
    {
        GameMode.IsCoop = true;
        SceneManager.LoadScene("Map1");
    }
    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
