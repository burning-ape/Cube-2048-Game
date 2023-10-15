using System.Collections.Generic;
using UnityEngine;
using System;

public class CubeLevelChanger : MonoBehaviour
{
    [SerializeField] private List<CubeLevelData> _levelsData;

    [SerializeField] private bool _deleteSavedMaxLevel;


    private void Awake() { if (_deleteSavedMaxLevel) PlayerPrefs.DeleteKey("maxLevel"); }


    public void MergeCubes(CubeNumber cube1, CubeNumber cube2)
    {
        // Merge only if both numbers are equal and one of the cubes has greater velocity,
        // so we prevent destroying both of them
        if (cube1.Number == cube2.Number
        && cube1.GetRbVelocityMgnt > cube2.GetRbVelocityMgnt)
        {
            if (cube1.CurrentLevel >= _levelsData.Count)
                throw new ArgumentException("No more levels specified");

            Destroy(cube2.gameObject);
            UpgradeCubeLevel(cube1);
        }
    }


    private void CheckIfNewLevel(int level)
    {
        int maxLevel = PlayerPrefs.GetInt("maxLevel");
        if (maxLevel < level)
        {
            Debug.Log("New level achived");
            PlayerPrefs.SetInt("maxLevel", level);
        }
    }


    public void UpgradeCubeLevel(CubeNumber cube)
    {
        var newData = _levelsData[cube.CurrentLevel];

        CheckIfNewLevel(newData.Level);

        cube.ChangeCubeNumber(cube.Number * 2);
        cube.GetComponent<MeshRenderer>().material.SetColor("_Color", newData.Color);

        cube.CurrentLevel = newData.Level;
    }


    public void UpgradeCubeLevel(CubeNumber cube, int level)
    {
        if (level >= _levelsData.Count)
            throw new ArgumentException("No more levels specified");

        var newData = _levelsData[level];

        cube.ChangeCubeNumber((int)Mathf.Pow(cube.Number, level + 1));
        cube.GetComponent<MeshRenderer>().material.SetColor("_Color", newData.Color);

        cube.CurrentLevel = newData.Level;
    }
}
