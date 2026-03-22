using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth: MonoBehaviour
{
    public GameObject FpsCam;
    public float detectionRange = 15f;
    public float attackRange = 1f;
    public float moveSpeed = 4f;
    public Slider healthSlider;
    public int Health = 100;
    public GameObject DamagePanel;
    public GameObject GameOverPanel;
    public AudioSource audioSource;
    public AudioClip DeathSound;
    public AudioClip HealthSound;
    public GameObject healthPickupButton;
    private GameObject currentHealthBox;
    public float pickupRange = 3f;
    public GameObject HealthSufficientText;
    private Transform player;

    void Start()
    {
        // Hide the damage panel at start  
        DamagePanel.SetActive(false);
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // Find the player object  
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        healthSlider.maxValue = 100;
        UpdateHealthUI();

        // Hide the noHealth text at start  
        HealthSufficientText.SetActive(false);
        DamagePanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    void Update()
    {

        // If the player has won the game, do not allow shooting  
        if (player == null) return;

        if (Health <= 0)
        {
            GameOverPanel.SetActive(true);
        }

        // Check if the player is looking at an ammo box  
        CheckForHealthBox();
    }

    public void HideNoHealth()
    {
        HealthSufficientText.SetActive(false);
    }

    // Check if the player is looking at an ammo box  
    void CheckForHealthBox()
    {
        RaycastHit hit;
        if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, pickupRange))
        {
            if (hit.transform.CompareTag("aid1") || hit.transform.CompareTag("aid2") ||
                hit.transform.CompareTag("aid3") || hit.transform.CompareTag("aid4"))
            {
                healthPickupButton.SetActive(true);
                currentHealthBox = hit.transform.gameObject;
                return;
            }
        }

        // If not looking at an ammo box, hide the text  
        healthPickupButton.SetActive(false);
        currentHealthBox = null;
    }

    // Collect ammo from the ammo box  
    void CollectHealth()
    {
        audioSource.PlayOneShot(HealthSound); // Play health pickup sound

        Health = Mathf.Min(Health + 10, 100); // Ensures Health never exceeds 100  
        UpdateHealthUI();
        healthPickupButton.SetActive(false);

        if (currentHealthBox != null)
        {
            Destroy(currentHealthBox); // Destroy the health box  
            currentHealthBox = null;
        }
    }
    public void OnHealthButtonPressed()
    {
        if (currentHealthBox == null) return;

        if (Health >= 100)
        {
            HealthSufficientText.SetActive(true);
            Invoke(nameof(HideNoHealth), 1f);
        }
        else
        {
            CollectHealth();
        }
    }


    public void UpdateUI()
    {
        Health = Health - 10;
        UpdateHealthUI();
    }

    // Add this method to PlayerHealth.cs
    public void TakeDamage(int damage)
    {
        Health -= damage;
        healthSlider.value = Health;

        // Show damage panel briefly
        DamagePanel.SetActive(true);
        Invoke(nameof(HideDamagePanel), 0.3f);

        // Play damage sound if available
        if (DeathSound != null)
        {
            audioSource.PlayOneShot(DeathSound);
        }

        Debug.Log("Player took " + damage + " damage. Health: " + Health);
    }

    private void HideDamagePanel()
    {
        DamagePanel.SetActive(false);
    }

    void UpdateHealthUI()
    {
        healthSlider.value = Health;

        if (Health <= 30)
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
        else
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.green;
        }
    }

}
