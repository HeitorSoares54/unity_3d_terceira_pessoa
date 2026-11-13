using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("human"))
        {
            anim.Play("DoorAnimation");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("human"))
        {
            anim.Play("DoorAnimationNegative");
        }
    }
}
