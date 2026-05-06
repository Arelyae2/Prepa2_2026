using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private BoxCollider boxCollider2;

    [SerializeField] private float speed;

    [HideInInspector] public bool isChasing = false;

    private int currentPatrolIndex = 0;
    private Vector3 origin = new Vector3();
    
    private void Awake()
    {
        Instance = this;

        origin = transform.position;
    }

    public void StartChase()
    {
        if (!isChasing)
        {
            isChasing = true;

            meshRenderer.enabled = true;
            boxCollider.enabled = true;
            boxCollider2.enabled = true;

            StartCoroutine(Chase());
        }
    }

    private void StopChase()
    {
        isChasing = false;
        currentPatrolIndex = 0;

        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        boxCollider2.enabled = false;

        transform.position = origin;

        PatrolPoint.Instance.listPatrol.Clear();
    }

    private IEnumerator Chase()
    {
        yield return null;

        while (isChasing)
        {
            Vector3 targetPoint = PatrolPoint.Instance.listPatrol[currentPatrolIndex];

            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime );

            if (Vector3.Distance(transform.position, targetPoint) < 0.01f)
            {
                currentPatrolIndex++;

                if (currentPatrolIndex >= PatrolPoint.Instance.listPatrol.Count)
                {
                    StopChase();
                    break;
                }
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopChase();

            GameManager.Instance.KillPlayer();
        }
    }
}
