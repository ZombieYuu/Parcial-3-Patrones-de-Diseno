// Assets/Scripts/Scene3_ObjectPool/ProjectileType3.cs
using UnityEngine;

public class ProjectileType3 : Projectile // Efecto de partículas
{
    private ParticleSystem _impactParticles;

    protected override void Awake()
    {
        base.Awake();
        _impactParticles = GetComponentInChildren<ParticleSystem>();
        if (_impactParticles == null)
        {
            Debug.LogWarning("ProjectileType3 no tiene un ParticleSystem hijo. Creando uno por defecto.");
            GameObject psGO = new GameObject("ImpactParticles");
            psGO.transform.SetParent(transform);
            psGO.transform.localPosition = Vector3.zero;
            _impactParticles = psGO.AddComponent<ParticleSystem>();

            var main = _impactParticles.main;
            main.duration = 0.5f;
            main.startLifetime = 0.5f;
            main.startSpeed = 5f;
            main.maxParticles = 50;
            _impactParticles.Stop(); 
        }
        else
        {
            _impactParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    public override void PrepareForLaunch()
    {
        base.PrepareForLaunch();
        if (_impactParticles != null)
        {
            _impactParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }


    protected override void HandleImpact(Collider other)
    {
        Debug.Log("Proyectil Tipo 3 impactó con: " + other.name);
        if (_impactParticles != null)
        {
            _impactParticles.transform.SetParent(null); // Desvincular para que no se desactive con el proyectil
            _impactParticles.Play();
            
        }
    }


    private new void ReturnToPool() // 'new' para ocultar el método base intencionadamente
    {
        isActiveInScene = false;
        gameObject.SetActive(false);

        if (_impactParticles != null)
        {
            _impactParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            _impactParticles.transform.SetParent(transform); 
            _impactParticles.transform.localPosition = Vector3.zero; 
        }
        OnReturnToPool?.Invoke(this);
    }
}