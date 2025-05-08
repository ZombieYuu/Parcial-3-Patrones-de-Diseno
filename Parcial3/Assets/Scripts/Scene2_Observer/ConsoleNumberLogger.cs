// Assets/Scripts/Scene2_Observer/ConsoleNumberLogger.cs
using UnityEngine;

public class ConsoleNumberLogger : MonoBehaviour
{
    void OnEnable()
    {
        ClickEventManager.OnButtonClicked += LogNumber;
    }

    void OnDisable()
    {
        ClickEventManager.OnButtonClicked -= LogNumber;
    }

    private void LogNumber(int number)
    {
        Debug.Log("Evento OnButtonClicked recibido. Número actual: " + number);
    }
}