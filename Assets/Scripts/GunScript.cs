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

        winingPanel.SetActive(false);

        ObjectivePanel.SetActive(true);
    }

    void Update()
    {
        if (winingPanel.activeSelf)
            return;

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

        ammoPickupButton.SetActive(false);
        currentAmmoBox = null;
    }


    void CollectAmmo()
    {
        ammoSound.Play();
        Bullets += 30; 
        BulletText.text = Bullets.ToString();
        ammoPickupButton.SetActive(false);

        if (currentAmmoBox != null)
        {
            Destroy(currentAmmoBox); 
            currentAmmoBox = null;
        }
    }


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


    void CollectTarget()
    {
        if (targetBox != null)
        {
            hardDrivePickupButton.SetActive(false);
            Destroy(targetBox); 
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
