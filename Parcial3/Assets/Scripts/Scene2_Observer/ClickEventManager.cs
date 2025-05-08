// Assets/Scripts/Scene2_Observer/ClickEventManager.cs
using UnityEngine;
using System; // Necesario para Action

public class ClickEventManager : MonoBehaviour
{
    public static event Action<int> OnButtonClicked; // Evento estático

    private int _clickCount = 0;
    private const int MAX_VALUE = 4;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            _clickCount = (_clickCount % MAX_VALUE) + 1; // Ciclo 1, 2, 3, 4, 1, ...
            OnButtonClicked?.Invoke(_clickCount); // Invocar el evento si hay suscriptores
        }
    }
}