using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class PlaneDetectionToggle : MonoBehaviour
{
    private ARPlaneManager _planeManager;

    [SerializeField] private TMP_Text _tmpText;

    private void Awake()
    {
        _planeManager = GetComponent<ARPlaneManager>();
    }

    public void TogglePlaneDetection()
    {
        _planeManager.enabled = !_planeManager.enabled;
        if (_planeManager.enabled)
        {
            SetAllPlanesActive(true);
            _tmpText.text = "Plane detection is enabled";
        }
        else
        {
            SetAllPlanesActive(false);
            _tmpText.text = "Plane detection is disabled";
        }
    }

    private void SetAllPlanesActive(bool value)
    {
        foreach (var plane in _planeManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }
}
