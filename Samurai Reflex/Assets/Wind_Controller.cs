using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Controller : MonoBehaviour
{
    [SerializeField] private Cloth[] _cloth;
    [SerializeField] private float _windStrength;
    [SerializeField] private float _gravityMod;
    [SerializeField] private Vector3 _windDirection;
    [SerializeField] private Vector3 _randomAcceleration;


    private void Start()
    {
        _cloth = GameObject.FindObjectsOfType<Cloth>();

        foreach (Cloth cloth in _cloth)
        {
            cloth.externalAcceleration = _windDirection * _windStrength;
            cloth.randomAcceleration = _randomAcceleration * _windStrength;
        }
    }
}
