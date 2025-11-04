using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject door;   // ide húzod be az ajtót az Inspectorban
    public bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Trigger entered by: {collision.gameObject.name}");
        if (collision.CompareTag("Player") && !isCollected)
        {
            CollectGem();
        }
    }

    void CollectGem()
    {
        isCollected = true;
        gameObject.SetActive(false); // eltûnik a pályáról

        if (door != null)
        {
            door.SetActive(true); // az ajtó megjelenik
        }

        Debug.Log("Gem collected! Door is now active.");
    }
}
