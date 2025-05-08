// Assets/Scripts/Scene3_ObjectPool/ProjectileType2.cs
using UnityEngine;
using System;

public class ProjectileType2 : Projectile // Deshabilita collider, bloquea disparo 1s
{
    public static event Action OnType2ImpactedTarget; // Evento para notificar al Shooter

    protected override void HandleImpact(Collider other)
    {
        // Asumimos que el target tiene un tag específico o un componente
        if (other.CompareTag("TargetCollider"))
        {
            Debug.Log("Proyectil Tipo 2 impactó con el Target: " + other.name);
            other.enabled = false; // Deshabilitar el collider del target
            OnType2ImpactedTarget?.Invoke(); // Notificar para bloquear disparo y reiniciar
        }
        else
        {
            Debug.Log("Proyectil Tipo 2 impactó con: " + other.name);
        }
    }
}
