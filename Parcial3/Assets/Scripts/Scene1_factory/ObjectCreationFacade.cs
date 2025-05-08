// Assets/Scripts/Scene1_FactoryFacade/ObjectCreationFacade.cs
using UnityEngine;
using System.Collections.Generic;

public enum ProductType { Cube, Sphere, Capsule }

public class ObjectCreationFacade
{
    private Dictionary<ProductType, IFactory> _factories;
    private ProductType _selectedProductType = ProductType.Cube; // Default

    public ObjectCreationFacade(GameObject cubePrefab, GameObject spherePrefab, GameObject capsulePrefab)
    {
        _factories = new Dictionary<ProductType, IFactory>
        {
            { ProductType.Cube, new CubeFactory(cubePrefab) },
            { ProductType.Sphere, new SphereFactory(spherePrefab) },
            { ProductType.Capsule, new CapsuleFactory(capsulePrefab) }
        };
    }

    public void SetProductType(ProductType type)
    {
        _selectedProductType = type;
        Debug.Log("Tipo de producto seleccionado: " + type);
    }

    public Product CreateSelectedProduct()
    {
        if (_factories.TryGetValue(_selectedProductType, out IFactory factory))
        {
            Product product = factory.CreateProduct();
            if (product != null)
            {
                // Posicionar el objeto para que sea visible
                product.transform.position = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
            }
            return product;
        }
        Debug.LogError("No se encontró factory para el tipo: " + _selectedProductType);
        return null;
    }
}