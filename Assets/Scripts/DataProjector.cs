using UnityEditor.Overlays;
using UnityEngine;

public class DataProjector : MonoBehaviour
{
    [SerializeField] SavedData projectedData;

    private void Update()
    {
        projectedData = Registry.data;
    }
}
