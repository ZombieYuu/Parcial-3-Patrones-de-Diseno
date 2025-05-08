// Assets/Scripts/Scene1_FactoryFacade/SphereFactory.cs
using UnityEngine;

public class SphereFactory : IFactory
{
    private GameObject _spherePrefab;

    public SphereFactory(GameObject prefab)
    {
        _spherePrefab = prefab;
        if (_spherePrefab == null) Debug.LogError("SpherePrefab no asignado a SphereFactory");
    }

    public Product CreateProduct()
    {
        if (_spherePrefab == null) return null;
        GameObject instance = GameObject.Instantiate(_spherePrefab);
        ProductSphere product = instance.GetComponent<ProductSphere>();
        if (product == null) product = instance.AddComponent<ProductSphere>();
        product.Initialize();
        return product;
    }
}
