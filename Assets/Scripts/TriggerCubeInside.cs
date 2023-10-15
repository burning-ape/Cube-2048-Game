using UnityEngine;

public class TriggerCubeInside : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube")) other.gameObject.layer = 7;
    }
}
