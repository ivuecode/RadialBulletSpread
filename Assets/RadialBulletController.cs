using UnityEngine;

public class RadialBulletController : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int numberOfProjectiles;             // Number of projectiles to shoot.
    public float projectileSpeed;               // Speed of the projectile.
    public GameObject ProjectilePrefab;         // Prefab to spawn.

    [Header("Private Variables")]
    private Vector3 startPoint;                 // Starting position of the bullet.
    private const float radius = 1F;            // Help us find the move direction.



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startPoint = transform.position;
            SpawnProjectile(numberOfProjectiles);
        }
    }

    // Spawns x number of projectiles.
    private void SpawnProjectile(int _numberOfProjectiles)
    {
        float angleStep = 360f / _numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <=_numberOfProjectiles -1; i++)
        {
            // Direction calculations.
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            // Create vectors.
            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

            // Create game objects.
            GameObject tmpObj = Instantiate(ProjectilePrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.y);

            // Destory the gameobject after 10 seconds.
            Destroy(tmpObj, 10F);

            angle += angleStep;
        }
    }
}