using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnpoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float cooldown = 0.3f;
    private float currentCooldown;


    private void Start()
    {
        currentCooldown = cooldown;
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && currentCooldown <= 0f)
        {
            Shoot();
            currentCooldown = cooldown;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
    }
}
