using UnityEngine;

public class BulletLanucher : MonoBehaviour
{
    public GameObject bullet;
    public GameObject muzzleFlash;
    public AudioSource bulletSound;
    private ParticleSystem muzzleFlashParticleSystem;
    public Light muzzleFlashLight; 

    void Start()
    {
        muzzleFlash.SetActive(false);

        muzzleFlashParticleSystem = muzzleFlash.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, transform.position, transform.rotation);

            bulletSound.Play();

            muzzleFlash.SetActive(true);

            muzzleFlashParticleSystem.Stop();
            muzzleFlashParticleSystem.Play();

            muzzleFlashLight.enabled = true;

            Invoke(nameof(DisableMuzzleFlash), 0.1f);
        }
    }

    void DisableMuzzleFlash()
    {
        muzzleFlashParticleSystem.Stop(); 
        muzzleFlash.SetActive(false); 
        muzzleFlashLight.enabled = false; 
    }


}
