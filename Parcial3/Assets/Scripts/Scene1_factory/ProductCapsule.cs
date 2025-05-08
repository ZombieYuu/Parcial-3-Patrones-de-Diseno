//  Assets/Scripts/Scene1_FactoryFacade/

using UnityEngine;


public class ProductCapsule : Product
{
    public override string GetProductType() => "Cápsula";
    public override void Initialize()
    {
        gameObject.name = "InstantiatedCapsule";
        var renderer = GetComponent<Renderer>();
        if (renderer != null) renderer.material.color = Color.blue;
        Debug.Log(GetProductType() + " creado y coloreado de azul.");
    }
}