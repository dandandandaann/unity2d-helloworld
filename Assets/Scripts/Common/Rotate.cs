using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    public float RotationSpeed = 2f;

    void Update()
    {
        transform.Rotate(0, 0, 180 * RotationSpeed * Time.deltaTime);
    }
}
