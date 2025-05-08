// Assets/Scripts/Scene3_ObjectPool/Scene3UIManager.cs
using UnityEngine;
using UnityEngine.UI;

public class Scene3UIManager : MonoBehaviour
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
            Debug.LogError("BackButton no encontrado en Scene3_ObjectPool.");
        }
    }
}