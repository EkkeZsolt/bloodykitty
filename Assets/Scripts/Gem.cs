using UnityEngine;
using UnityEngine.SceneManagement;

public class Gem : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectGem();
        }
    }


    void CollectGem()
    {
        // T�lts�k be a k�vetkez� p�lya jelenetet
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set!");
        }
    }
}
