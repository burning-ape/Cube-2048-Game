using UnityEngine;

public class ForceFPS : MonoBehaviour
{
    private void Awake() => Application.targetFrameRate = 60;
}
