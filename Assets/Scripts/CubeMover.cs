using BurningApe.Touch;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CubeMover : MonoBehaviour, ICubeObserver
{
    [Header("Cube Settings")]
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _force;
    private CubeNumber _cube;

    [Space(15)]
    [Header("Mover Settings")]
    [SerializeField] private TouchControlls _touchControlls;
    [SerializeField] private float _xClamp, _step;
    private float _xCubePos = 0.0f;


    public void UpdateCube(CubeNumber cube) => _cube = cube;


    private void Awake()
    {
        // Subsctibe to observable
        _cubeSpawner.AddObserver(this);

        // Subscribe controls events
        _touchControlls.OnSwipeEvent.AddListener(MovingCube);
        _touchControlls.OnTouchEndEvent.AddListener(ReleaseCube);
    }


    private void MovingCube(TouchData data)
    {
        if (_cube is null) return;

        if (data.CurrentSwipeDirection == TouchData.SwipeDirections.Right) _xCubePos += _step * Time.deltaTime;
        else if (data.CurrentSwipeDirection == TouchData.SwipeDirections.Left) _xCubePos -= _step * Time.deltaTime;

        _xCubePos = Mathf.Clamp(_xCubePos, -_xClamp, _xClamp);

        _cube.transform.position = new Vector3(_xCubePos,
                                                _cube.transform.position.y,
                                                _cube.transform.position.z);
    }


    private async void ReleaseCube(TouchData data)
    {
        if (_cube is not null)
        {
            // Add force
            _cube
                .GetComponent<Rigidbody>()
                .AddForce(transform.forward * _force, ForceMode.Impulse);

            // Clear previous cube data
            _cube = null;
            _xCubePos = 0.0f;

            // Spawn another cube with delay
            await UniTask.Delay(300);
            _cubeSpawner.Spawn();
        }
    }
}
