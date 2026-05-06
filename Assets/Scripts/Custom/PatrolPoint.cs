using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public static PatrolPoint Instance;

    public List<Vector3> listPatrol;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPatrolPoint(Vector3 newPosition)
    {
        if (!listPatrol.Contains(newPosition))
        {
            listPatrol.Add(newPosition);
        }
    }

    public void RemovePatrolPoint(Vector3 newPosition)
    {
       listPatrol.Remove(newPosition);
    }
}