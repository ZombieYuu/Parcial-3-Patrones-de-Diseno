// Assets/Scripts/Common/BackButtonManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para Button

public class BackButtonManager : MonoBehaviour
{
    public string masterSceneName = "MasterScene";

    public void GoToMasterScene()
    {
        SceneManager.LoadScene(masterSceneName);
    }


    public static void SetupBackButton(Button buttonInstance, string sceneToLoad = "MasterScene")
    {
        if (buttonInstance != null)
        {
            buttonInstance.onClick.RemoveAllListeners(); // Limpiar listeners anteriores
            buttonInstance.onClick.AddListener(() => SceneManager.LoadScene(sceneToLoad));
        }
    }
}