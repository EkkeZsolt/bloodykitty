using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(BackToMenu);
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }
}
