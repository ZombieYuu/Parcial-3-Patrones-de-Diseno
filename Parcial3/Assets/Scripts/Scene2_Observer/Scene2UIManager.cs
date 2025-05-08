// Assets/Scripts/Scene2_Observer/Scene2UIManager.cs
using UnityEngine;
using UnityEngine.UI;

public class Scene2UIManager : MonoBehaviour
{
    void Start()
    {
        Button backButton = GameObject.Find("BackButton")?.GetComponent<Button>();
        if (backButton != null)
        {
            BackButtonManager.SetupBackButton(backButton, "MasterScene");
        }
        else
        {
            Debug.LogError("BackButton no encontrado en Scene2_Observer.");
        }
    }
}