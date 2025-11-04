using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
    Button infoButton;

    void Start()
    {
        infoButton = GetComponent<Button>();
        if (infoButton != null)
        {
            infoButton.onClick.AddListener(CloseInfoBox);
        }
    }
    public void CloseInfoBox()
    {
        gameObject.SetActive(false);
        Debug.Log("Eltûnt az InfoBox");
    }

}




