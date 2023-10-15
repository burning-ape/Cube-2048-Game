using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CubeNumber : MonoBehaviour
{
    [Header("Numbers Settings")]
    public int Number;
    [SerializeField] private TMP_Text[] _textNumbers;

    [HideInInspector] public int CurrentLevel = 1;

    [HideInInspector] public UnityEvent<CubeNumber, CubeNumber> Collided;

    private Rigidbody _rb;
    public float GetRbVelocityMgnt { get { return _rb.velocity.magnitude; } }


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        ChangeCubeNumber(Number);
    }


    /// Send event about collision with another cube to Cube Level Changer
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cube") && Collided is not null)
            Collided.Invoke(this, other.transform.GetComponent<CubeNumber>());
    }


    public void ChangeCubeNumber(int number)
    {
        Number = number;

        foreach (var text in _textNumbers)
            text.text = NumberSuffix.SetValueWithSuffix(number);
    }
}



