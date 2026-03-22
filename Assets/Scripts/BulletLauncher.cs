using UnityEngine;

public class BulletLanucher : MonoBehaviour
{
    public GameObject bullet;
    public GameObject muzzleFlash;
    public AudioSource bulletSound;
    private ParticleSystem muzzleFlashParticleSystem;
    public Light muzzleFlashLight; 

    // Start is called before the first frame update
    void Start()
    {
        muzzleFlash.SetActive(false);

        // Get the ParticleSystem component from the muzzleFlash GameObject
        muzzleFlashParticleSystem = muzzleFlash.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Instantiate the bullet prefab at the position & rotation of the bullet launcher
            Instantiate(bullet, transform.position, transform.rotation);

            // Play the bullet sound
            bulletSound.Play();

            muzzleFlash.SetActive(true);

            // Restart the particle system
            muzzleFlashParticleSystem.Stop();
            muzzleFlashParticleSystem.Play();

            // Enable the light
            muzzleFlashLight.enabled = true;

            // Disable the muzzle flash & light after a delay
            Invoke(nameof(DisableMuzzleFlash), 0.1f);
        }
    }

    // Function to disable the muzzle flash & light
    void DisableMuzzleFlash()
    {
        muzzleFlashParticleSystem.Stop(); 
        muzzleFlash.SetActive(false); 
        muzzleFlashLight.enabled = false; 
    }


}
