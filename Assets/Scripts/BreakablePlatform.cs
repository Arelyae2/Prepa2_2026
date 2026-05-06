using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    private void OnEnable()
    {
        EnableBody();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DisableBody();
        }
    }

    [SerializeField] GameObject body;
    [SerializeField] BoxCollider col;

    void EnableBody()
    {
        body.SetActive(true);
        col.enabled = true;
    }

    void DisableBody()
    {
        body.SetActive(false);
        col.enabled = false;
    }
}
