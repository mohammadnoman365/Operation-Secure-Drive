using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    Animator anim;
    public GameObject fps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.LookAt(fps.transform);
    }

    public void EnemyDie()
    {
        anim.SetTrigger("dead");
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name.StartsWith("FPSController"))
        {
            anim.SetTrigger("attack");
        }
    }
}
