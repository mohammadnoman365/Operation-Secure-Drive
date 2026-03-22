using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunScript : MonoBehaviour
{
    public GameObject FpsCam;
    public GameObject bullet; 
    public GameObject muzzleFlash;
    public AudioSource bulletSound;
    public AudioSource ammoSound;
    public TextMeshProUGUI BulletText;
    public int Bullets = 30;
    public GameObject NoBulletsText;
    public Light muzzleFlashLight;
    public float bulletSpeed = 50f; 
    public float range = 500f; 
    private ParticleSystem muzzleFlashParticleSystem;
    private GameObject currentAmmoBox; 
    private GameObject targetBox; 
    public float pickupRange = 3f; 
    public GameObject winingPanel;
    public GameObject ObjectivePanel;
    public GameObject ammoPickupButton;
    public GameObject hardDrivePickupButton;

    void Start()
    {

        muzzleFlash.SetActive(false);
        muzzleFlashParticleSystem = muzzleFlash.GetComponent<ParticleSystem>();

        BulletText.text = "" + Bullets.ToString();
        NoBulletsText.SetActive(false);

        ammoPickupButton.SetActive(false);

        hardDrivePickupButton.SetActive(false);

        // Hide wining panel at start
        winingPanel.SetActive(false);

        // Show Objective panel at start
        ObjectivePanel.SetActive(true);
    }

    void Update()
    {
        // If the player has won the game, do not allow shooting
        if (winingPanel.activeSelf)
            return;

        // If the Objective panel is active, do not allow shooting
        if (ObjectivePanel.activeSelf)
            return;

        CheckForAmmoBox(); // Check if the player is looking at an ammo box
        CheckTarget();    // Check if the player is looking at the target

    }
    void HideReloadBullet()
    {
        NoBulletsText.SetActive(false);
    }

    public void Shoot()
    {
        if (Bullets > 0)
        {
            PlayMuzzleFlash();
            UpdateUI();

            // Check if the bullet hit an enemy
            RaycastHit hit;
            if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, range))
            {
                Debug.Log("Hit: " + hit.transform.name);
                Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(20);
                    return;
                }

            }
        }
        else
        {
            NoBulletsText.SetActive(true);
            Invoke(nameof(HideReloadBullet), 1f);
        }
    }

    public void UpdateUI()
    {
        Bullets--;
        BulletText.text = "" + Bullets.ToString();
    }


    // Function to play muzzle flash effects
    void PlayMuzzleFlash()
    {
        bulletSound.Play();
        muzzleFlash.SetActive(true);
        muzzleFlashParticleSystem.Stop();
        muzzleFlashParticleSystem.Play();
        muzzleFlashLight.enabled = true;
        Invoke(nameof(DisableMuzzleFlash), 0.1f);
    }

    void DisableMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
        muzzleFlashParticleSystem.Stop();
        muzzleFlashLight.enabled = false;
    }

    // Function to check if the player is looking at an ammo box
    void CheckForAmmoBox()
    {
        RaycastHit hit;
        if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, pickupRange))
        {
            if (hit.transform.CompareTag("ammo1") || hit.transform.CompareTag("ammo2") ||
                hit.transform.CompareTag("ammo3") || hit.transform.CompareTag("ammo4"))
            {
                ammoPickupButton.SetActive(true);
                currentAmmoBox = hit.transform.gameObject;
                return;
            }
        }

        // If not looking at an ammo box, hide the text
        ammoPickupButton.SetActive(false);
        currentAmmoBox = null;
    }


    // Function to collect ammo
    void CollectAmmo()
    {
        ammoSound.Play();
        Bullets += 30; // Refill bullets
        BulletText.text = Bullets.ToString();
        ammoPickupButton.SetActive(false);

        if (currentAmmoBox != null)
        {
            Destroy(currentAmmoBox); // Destroy the ammo box
            currentAmmoBox = null;
        }
    }


    // Function to check if the player is looking at the target
    void CheckTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, pickupRange))
        {
            if (hit.transform.CompareTag("Target"))
            {
                hardDrivePickupButton.SetActive(true);
                targetBox = hit.transform.gameObject;
                return;
            }
        }

        hardDrivePickupButton.SetActive(false);
        targetBox = null;
    }


    // Function to collect the target
    void CollectTarget()
    {
        if (targetBox != null)
        {
            hardDrivePickupButton.SetActive(false);
            Destroy(targetBox); // Destroy the ammo box
            targetBox = null;
            winingPanel.SetActive(true);
        }
    }

    public void OnAmmoButtonPressed()
    {
        if (currentAmmoBox != null)
        {
            CollectAmmo();
        }
    }

    public void OnHardDriveButtonPressed()
    {
        if (targetBox != null)
        {
            CollectTarget();
        }
    }
}
