// Assets/Scripts/Scene3_ObjectPool/PlayerShooter.cs
using UnityEngine;
using System.Collections; // Para Coroutines

public enum ProjectileTypeToShoot { Type1, Type2, Type3 }

public class PlayerShooter : MonoBehaviour
{
    public Transform firePoint; // Un GameObject hijo que indica de dónde sale el proyectil

    private ProjectileType1Pool _poolType1;
    private ProjectileType2Pool _poolType2;
    private ProjectileType3Pool _poolType3;

    private ProjectileTypeToShoot _currentProjectileType = ProjectileTypeToShoot.Type1;
    private bool _canShoot = true;
    private Camera _mainCamera;

    void Start()
    {

        _poolType1 = FindObjectOfType<ProjectileType1Pool>();
        _poolType2 = FindObjectOfType<ProjectileType2Pool>();
        _poolType3 = FindObjectOfType<ProjectileType3Pool>();

        if (_poolType1 == null) Debug.LogError("ProjectileType1Pool no encontrado en la escena.");
        if (_poolType2 == null) Debug.LogError("ProjectileType2Pool no encontrado en la escena.");
        if (_poolType3 == null) Debug.LogError("ProjectileType3Pool no encontrado en la escena.");

        _mainCamera = Camera.main;
        if (_mainCamera == null) Debug.LogError("No se encontró Main Camera.");


        if (firePoint == null)
        {
            GameObject fpGo = new GameObject("FirePoint");
            fpGo.transform.SetParent(transform);
            fpGo.transform.localPosition = new Vector3(0, 0, 1f); 
            firePoint = fpGo.transform;
        }
    }

    void OnEnable()
    {
        ProjectileType2.OnType2ImpactedTarget += HandleType2Impact;
    }

    void OnDisable()
    {
        ProjectileType2.OnType2ImpactedTarget -= HandleType2Impact;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Click derecho para cambiar tipo
        {
            CycleProjectileType();
        }

        if (Input.GetMouseButtonDown(0) && _canShoot) // Click izquierdo para disparar
        {
            Shoot();
        }
    }

    void CycleProjectileType()
    {
        int currentTypeIndex = (int)_currentProjectileType;
        currentTypeIndex = (currentTypeIndex + 1) % System.Enum.GetValues(typeof(ProjectileTypeToShoot)).Length;
        _currentProjectileType = (ProjectileTypeToShoot)currentTypeIndex;
        Debug.Log("Proyectil seleccionado: " + _currentProjectileType);
    }

    void Shoot()
    {
        if (firePoint == null || _mainCamera == null)
        {
            Debug.LogError("FirePoint o MainCamera no están configurados.");
            return;
        }



        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f)) 
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }
        Vector3 direction = (targetPoint - firePoint.position).normalized;


        Projectile projectile = null;
        switch (_currentProjectileType)
        {
            case ProjectileTypeToShoot.Type1:
                if (_poolType1 != null) projectile = _poolType1.GetObject();
                break;
            case ProjectileTypeToShoot.Type2:
                if (_poolType2 != null) projectile = _poolType2.GetObject();
                break;
            case ProjectileTypeToShoot.Type3:
                if (_poolType3 != null) projectile = _poolType3.GetObject();
                break;
        }

        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = Quaternion.LookRotation(direction); 
            projectile.Launch(direction);
        }
        else
        {
            Debug.LogWarning($"No se pudo obtener un proyectil del tipo {_currentProjectileType} del pool.");
        }
    }

    private void HandleType2Impact()
    {
        StartCoroutine(DisableShootingAndReEnableTarget());
    }

    private IEnumerator DisableShootingAndReEnableTarget()
    {
        _canShoot = false;
        Debug.Log("Disparo bloqueado por 1 segundo.");

        yield return new WaitForSeconds(1f);


        GameObject targetObject = GameObject.FindGameObjectWithTag("TargetCollider");
        if (targetObject != null)
        {
            Collider targetCollider = targetObject.GetComponent<Collider>();
            if (targetCollider != null && !targetCollider.enabled)
            {
                targetCollider.enabled = true;
                Debug.Log("Target Collider re-habilitado.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el TargetCollider para re-habilitar.");
        }

        _canShoot = true;
        Debug.Log("Disparo desbloqueado.");
    }
}