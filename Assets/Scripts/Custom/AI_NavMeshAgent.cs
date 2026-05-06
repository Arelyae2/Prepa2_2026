using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.Shapes;

public class AI_NavMeshAgent : MonoBehaviour
{
    public static AI_NavMeshAgent Instance;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent ai_NavMeshAgent;
    [SerializeField] private GameObject sprite;
    [SerializeField] private SphereCollider colliders;

    private bool isfollowing = false;
    private Vector3 origin = new Vector3();

    private void Awake()
    {
        Instance = this;

        origin = transform.position;
        sprite.SetActive(false);
        colliders.enabled = false;
    }

    private void Update()
    {
        if (isfollowing)
        {
            EnnemyFollowing();
        }
    }

    public void StartFollow()
    {
        transform.position = origin;
        sprite.SetActive(true);
        colliders.enabled = true;
        isfollowing = true;
    }

    public void EnnemyFollowing()
    {
        ai_NavMeshAgent.destination = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isfollowing = false;
            sprite.SetActive(false);
            colliders.enabled = false;
            GameManager.Instance.KillPlayer();
        }
    }
}