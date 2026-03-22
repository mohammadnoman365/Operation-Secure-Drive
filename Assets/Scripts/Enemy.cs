using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    public int enemyHealth = 100;

    public Animator animator;

    public void Shoot()
    {

        // Calculate direction to player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 direction = (player.transform.position - firePoint.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            GameObject spawnedProjectile = Instantiate(projectile, firePoint.position, rotation);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy Hit!");
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            animator.SetTrigger("Death");
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }
}
