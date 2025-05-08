// Assets/Scripts/Scene1_FactoryFacade/Product.cs
using UnityEngine;

public abstract class Product : MonoBehaviour
{
    public abstract string GetProductType();
    public virtual void Initialize()
    {
        Debug.Log(GetProductType() + " inicializado.");
    }
}