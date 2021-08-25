using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject projectileGameObject;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> pool;

    private GameObject currentProjectile;

    public GameObject GetProjectile()
    {
        currentProjectile = pool
            .Where(projectile => !projectile.activeSelf)
            .FirstOrDefault<GameObject>();

        if (currentProjectile != null)
        {
            currentProjectile.SetActive(true);
        }

        return currentProjectile;
    }

    public void InactivateProjectile(GameObject projectile)
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        FillPool();
    }

    private void FillPool()
    {
        pool = new List<GameObject>(poolSize);
        for (int index = 0; index < poolSize; index++)
        {
            GameObject projectile = Instantiate(projectileGameObject);
            projectile.SetActive(false);
            projectile.GetComponent<SimpleProjectile>().projectileObjectPool = this;
            
            pool.Add(projectile);
        }
    }
}
