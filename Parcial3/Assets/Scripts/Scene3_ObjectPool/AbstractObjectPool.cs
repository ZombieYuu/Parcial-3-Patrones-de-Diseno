// Assets/Scripts/Scene3_ObjectPool/AbstractObjectPool.cs
using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractObjectPool<T> : MonoBehaviour where T : Projectile
{
    [Header("Pool Settings")]
    [SerializeField] protected GameObject objectPrefab; // Asignar el prefab espec�fico en el Inspector del GameObject del Pool concreto
    [SerializeField] protected int initialPoolSize = 10;

    protected Queue<T> pooledObjects = new Queue<T>();

    protected virtual void Awake()
    {
        if (objectPrefab == null)
        {
            Debug.LogError($"Object prefab no est� asignado en el pool: {this.GetType().Name}", this);
            return;
        }
        InitializePool();
    }

    protected virtual void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            T newObj = CreateNewObject();
            newObj.gameObject.SetActive(false); // Asegurarse de que est� inactivo
            pooledObjects.Enqueue(newObj);
        }
    }

    protected T CreateNewObject()
    {
        GameObject instance = Instantiate(objectPrefab);
        T projectileComponent = instance.GetComponent<T>();
        if (projectileComponent == null)
        {
            Debug.LogError($"El prefab {objectPrefab.name} no tiene el componente {typeof(T).Name} esperado.", instance);
            // Podr�as intentar a�adirlo, pero es mejor que el prefab ya lo tenga.
            // projectileComponent = instance.AddComponent<T>();
        }
        projectileComponent.OnReturnToPool = ReturnObjectToPool; // Suscribir el m�todo de retorno
        instance.transform.SetParent(this.transform); // Opcional: organizar jerarqu�a
        return projectileComponent;
    }

    public virtual T GetObject()
    {
        if (pooledObjects.Count > 0)
        {
            T obj = pooledObjects.Dequeue();
            obj.gameObject.SetActive(true);
            obj.PrepareForLaunch(); // M�todo para resetear estado antes de usar
            return obj;
        }
        else
        {
            // Opcional: expandir el pool si est� vac�o
            Debug.LogWarning($"Pool de {typeof(T).Name} vac�o. Creando nuevo objeto sobre la marcha.");
            T newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.PrepareForLaunch();
            return newObj;
        }
    }

    public virtual void ReturnObjectToPool(Projectile obj) // Usar Projectile base para el Action
    {
        T typedObj = obj as T;
        if (typedObj != null)
        {
            typedObj.gameObject.SetActive(false);
            // Resetear Rigidbody para evitar movimiento residual
            Rigidbody rb = typedObj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            typedObj.transform.SetParent(this.transform); // Re-parentar al pool
            typedObj.transform.localPosition = Vector3.zero; // Resetear posici�n relativa
            pooledObjects.Enqueue(typedObj);
        }
        else
        {
            Debug.LogError($"Se intent� devolver un objeto de tipo incorrecto ({obj.GetType().Name}) al pool de {typeof(T).Name}");
            Destroy(obj.gameObject); // Destruir si no pertenece a este pool
        }
    }
}