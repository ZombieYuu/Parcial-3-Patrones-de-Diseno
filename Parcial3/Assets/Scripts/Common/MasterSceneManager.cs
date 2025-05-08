// Assets/Scripts/Common/MasterSceneManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MasterSceneManager : MonoBehaviour
{
    public Button scene1Button;
    public Button scene2Button;
    public Button scene3Button;

    void Start()
    {
        if (scene1Button != null)
            scene1Button.onClick.AddListener(() => LoadSceneByName("Scene1_FactoryFacade"));
        else
            Debug.LogError("Scene1Button no asignado en MasterSceneManager.");

        if (scene2Button != null)
            scene2Button.onClick.AddListener(() => LoadSceneByName("Scene2_Observer"));
        else
            Debug.LogError("Scene2Button no asignado en MasterSceneManager.");

        if (scene3Button != null)
            scene3Button.onClick.AddListener(() => LoadSceneByName("Scene3_ObjectPool"));
        else
            Debug.LogError("Scene3Button no asignado en MasterSceneManager.");
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}