// Assets/Scripts/Scene2_Observer/CubeColorChanger.cs
using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private Color[] _colors = { Color.white, Color.red, Color.green, Color.blue, Color.yellow }; // Colores para 1-4 (�ndice 0 no se usa para el n�mero)

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer == null)
        {
            Debug.LogError("CubeColorChanger necesita un componente Renderer.", this);
            enabled = false; // Deshabilitar script si no hay renderer
        }
    }

    void OnEnable()
    {
        ClickEventManager.OnButtonClicked += HandleColorChange;
    }

    void OnDisable()
    {
        ClickEventManager.OnButtonClicked -= HandleColorChange;
    }

    private void HandleColorChange(int number)
    {
        if (_renderer != null && number >= 1 && number <= 4)
        {
            // Asignamos colores: 1=rojo, 2=verde, 3=azul, 4=amarillo
            // Los colores en el array est�n en �ndice 1, 2, 3, 4
            // Para mapear n�mero 1 al �ndice 1, etc.
            _renderer.material.color = _colors[number];
            Debug.Log($"Cubo cambi� color a: {_colors[number]} basado en el n�mero: {number}");
        }
    }
}