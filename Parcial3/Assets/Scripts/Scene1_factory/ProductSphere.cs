//  Assets/Scripts/Scene1_FactoryFacade/


using UnityEngine;


public class ProductSphere : Product
{
    public override string GetProductType() => "Esfera";
    public override void Initialize()
    {
        gameObject.name = "InstantiatedSphere";
        var renderer = GetComponent<Renderer>();
        if (renderer != null) renderer.material.color = Color.green;
        Debug.Log(GetProductType() + " creado y coloreado de verde.");
    }
}
