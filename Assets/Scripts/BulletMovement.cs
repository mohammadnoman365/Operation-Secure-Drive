using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    void Update()
    {
        // Move the bullet forward 
        transform.Translate(Vector3.forward * 20f * Time.deltaTime);
    }
}
