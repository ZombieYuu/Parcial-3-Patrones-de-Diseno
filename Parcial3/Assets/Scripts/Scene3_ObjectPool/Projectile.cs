// Assets/Scripts/Scene3_ObjectPool/Projectile.cs
using UnityEngine;
using System; 

public abstract class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public Action<Projectile> OnReturnToPool;
    protected Rigidbody rb;
    protected bool isActiveInScene = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public virtual void Launch(Vector3 direction)
    {
        if (rb == null) Awake(); 
        gameObject.SetActive(true);
        rb.linearVelocity = direction.normalized * speed;
        isActiveInScene = true;
        //Debug.Log($"{this.GetType().Name} lanzado.");
    }

    
    public virtual void PrepareForLaunch()
    {
        // Resetear cualquier estado espec�fico del proyectil si es necesario
        gameObject.SetActive(true);
        isActiveInScene = true;
    }

    protected abstract void HandleImpact(Collider other);

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (!isActiveInScene) return; 

        //Debug.Log($"{this.GetType().Name} colision� con {collision.gameObject.name}");
        HandleImpact(collision.collider);
        ReturnToPool();
    }

    protected void ReturnToPool()
    {
        isActiveInScene = false;
        gameObject.SetActive(false);
        OnReturnToPool?.Invoke(this); 
    }
}