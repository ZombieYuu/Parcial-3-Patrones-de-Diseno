// Assets/Scripts/Scene1_FactoryFacade/CubeFactory.cs
using UnityEngine;

public class CubeFactory : IFactory
{
    private GameObject _cubePrefab; // Se asignará mediante código

    public CubeFactory(GameObject prefab)
    {
        _cubePrefab = prefab;
        if (_cubePrefab == null) Debug.LogError("CubePrefab no asignado a CubeFactory");
    }

    public Product CreateProduct()
    {
        if (_cubePrefab == null) return null;
        GameObject instance = GameObject.Instantiate(_cubePrefab);
        ProductCube product = instance.GetComponent<ProductCube>();
        if (product == null) product = instance.AddComponent<ProductCube>(); // Si el prefab no lo tiene
        product.Initialize();
        return product;
    }
}