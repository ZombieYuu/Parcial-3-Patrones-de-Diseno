// Assets/Scripts/Scene1_FactoryFacade/CapsuleFactory.cs
using UnityEngine;

public class CapsuleFactory : IFactory
{
    private GameObject _capsulePrefab;

    public CapsuleFactory(GameObject prefab)
    {
        _capsulePrefab = prefab;
        if (_capsulePrefab == null) Debug.LogError("CapsulePrefab no asignado a CapsuleFactory");
    }

    public Product CreateProduct()
    {
        if (_capsulePrefab == null) return null;
        GameObject instance = GameObject.Instantiate(_capsulePrefab);
        ProductCapsule product = instance.GetComponent<ProductCapsule>();
        if (product == null) product = instance.AddComponent<ProductCapsule>();
        product.Initialize();
        return product;
    }
}