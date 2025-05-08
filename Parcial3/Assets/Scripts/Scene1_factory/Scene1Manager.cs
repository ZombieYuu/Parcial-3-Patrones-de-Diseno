// Assets/Scripts/Scene1_FactoryFacade/Scene1Manager.cs
using UnityEngine;
using UnityEngine.UI;

public class Scene1Manager : MonoBehaviour
{
    private ObjectCreationFacade _facade;

    // Variables para cargar los prefabs desde la carpeta Resources
    private GameObject _cubePrefabResource;
    private GameObject _spherePrefabResource;
    private GameObject _capsulePrefabResource;

    // Referencias a los botones de UI (se buscarán por nombre)
    private Button _selectCubeButton;
    private Button _selectSphereButton;
    private Button _selectCapsuleButton;
    private Button _createObjectButton;
    private Button _backButton;

    void Start()
    {
        _cubePrefabResource = Resources.Load<GameObject>("PrefabsForScene1/CubePrefab");
        _spherePrefabResource = Resources.Load<GameObject>("PrefabsForScene1/SpherePrefab");
        _capsulePrefabResource = Resources.Load<GameObject>("PrefabsForScene1/CapsulePrefab");

        if (_cubePrefabResource == null || _spherePrefabResource == null || _capsulePrefabResource == null)
        {
            Debug.LogError("Error al cargar uno o más prefabs desde Resources. Verifica las rutas.");
            return;
        }

        _facade = new ObjectCreationFacade(_cubePrefabResource, _spherePrefabResource, _capsulePrefabResource);

        _selectCubeButton = GameObject.Find("SelectCubeButton")?.GetComponent<Button>();
        _selectSphereButton = GameObject.Find("SelectSphereButton")?.GetComponent<Button>();
        _selectCapsuleButton = GameObject.Find("SelectCapsuleButton")?.GetComponent<Button>();
        _createObjectButton = GameObject.Find("CreateObjectButton")?.GetComponent<Button>();
        _backButton = GameObject.Find("BackButton")?.GetComponent<Button>();

        if (_selectCubeButton != null)
            _selectCubeButton.onClick.AddListener(() => _facade.SetProductType(ProductType.Cube));
        else Debug.LogError("SelectCubeButton no encontrado.");

        if (_selectSphereButton != null)
            _selectSphereButton.onClick.AddListener(() => _facade.SetProductType(ProductType.Sphere));
        else Debug.LogError("SelectSphereButton no encontrado.");

        if (_selectCapsuleButton != null)
            _selectCapsuleButton.onClick.AddListener(() => _facade.SetProductType(ProductType.Capsule));
        else Debug.LogError("SelectCapsuleButton no encontrado.");

        if (_createObjectButton != null)
            _createObjectButton.onClick.AddListener(() => _facade.CreateSelectedProduct());
        else Debug.LogError("CreateObjectButton no encontrado.");

        // Configurar botón de regreso
        if (_backButton != null)
        {
            BackButtonManager.SetupBackButton(_backButton, "MasterScene");
        }
        else Debug.LogError("BackButton no encontrado.");

        // Crear un plano para que los objetos no caigan al vacío
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = Vector3.zero;
        plane.transform.localScale = new Vector3(2, 1, 2); // Plano de 20x20 unidades
    }
}