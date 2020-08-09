using System;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class RotationController : MonoBehaviour
{
    public static void Rotate(Vector3 rotationSpeed, GameObject gameObject)
    {
        gameObject.transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
    }
}
