using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolManager : MonoBehaviour
{
    [SerializeField] private Transform bulletsSpawnPoint;
    [SerializeField] private GameObject bulletsPrefab;

    [SerializeField] private ProtaAnimationManager protaAnimationManager;

    [SerializeField] private float bulletSpeed = 5f;

    Coroutine shootingCoroutine = null;

    private bool shoot = false;

    private IEnumerator ShootColdDown()
    {
        if (shoot)
        {
            var bullet = Instantiate(bulletsPrefab, bulletsSpawnPoint.position, bulletsSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletsSpawnPoint.forward * bulletSpeed;
            protaAnimationManager.ShootPistolAnimation();
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(ShootColdDown());
    }

    public void StartShooting()
    {
        if (shootingCoroutine == null)
        {
            shoot = true;
            shootingCoroutine = StartCoroutine(ShootColdDown());
        }
    }

    public void StopShooting() 
    {
        if (shootingCoroutine != null)
        {
            shoot = false;
            StopCoroutine(ShootColdDown());
            shootingCoroutine = null;
        }
    }
}
