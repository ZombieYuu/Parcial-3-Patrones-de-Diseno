// Assets/Scripts/Scene3_ObjectPool/ProjectileType1.cs
using UnityEngine;

public class ProjectileType1 : Projectile // Imprime mensaje
{
    protected override void HandleImpact(Collider other)
    {
        Debug.Log("Proyectil Tipo 1 impact� con: " + other.name + ". �Mensaje especial!");
    }
}