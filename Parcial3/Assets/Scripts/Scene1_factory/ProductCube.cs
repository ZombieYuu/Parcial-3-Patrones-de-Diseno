//  Assets/Scripts/Scene1_FactoryFacade/
using UnityEngine;

public class ProductCube : Product
{
    public override string GetProductType() => "Cubo";
    public override void Initialize()
    {
        gameObject.name = "InstantiatedCube";
        var renderer = GetComponent<Renderer>();
        if (renderer != null) renderer.material.color = Color.red;
        Debug.Log(GetProductType() + " creado y coloreado de rojo.");
    }
}
