using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour, ICubeObservable
{
    [SerializeField] private CubeLevelChanger _cubeLevelChanger;
    [SerializeField] private GameObject _defaultCubePrefab;
    [SerializeField] private Vector3 _spawnPosition;


    #region Observable Implementation

    private List<ICubeObserver> _observers = new();
    public void AddObserver(ICubeObserver observer) => _observers.Add(observer);
    private void NotifyObservers(CubeNumber cube) { foreach (var observer in _observers) observer.UpdateCube(cube); }

    #endregion


    private void Start() => Spawn();


    public void Spawn()
    {
        var lastSpawnedCube = Instantiate(_defaultCubePrefab, _spawnPosition, Quaternion.identity);

        var cubeNumber = lastSpawnedCube.GetComponent<CubeNumber>();

        // Pass the merge method to the cube, so it can call it upon collision
        cubeNumber.Collided.AddListener(_cubeLevelChanger.MergeCubes);

        RandomizeCubeLevel(cubeNumber);
        NotifyObservers(cubeNumber);
    }

    /// Loads last maximum achived level by player, then gets random number from 0 up to max level-1
    /// and upgrades the level of cube by power of this number
    private void RandomizeCubeLevel(CubeNumber cube)
    {
        int maxLevel = PlayerPrefs.GetInt("maxLevel");

        int randomLevel = UnityEngine.Random.Range(0, maxLevel) - 2;
        if (randomLevel < 0) randomLevel = 0;

        _cubeLevelChanger.UpgradeCubeLevel(cube, randomLevel);
    }
}
