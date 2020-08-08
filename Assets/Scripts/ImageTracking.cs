using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField] private GameObject[] placeablePrefabs;
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private Vector3 rotationSpeed = Vector3.zero;
    [SerializeField] private Toggle rotationToggle;
    
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager _trackedImageManager;
    private GameObject _tmpGameObject = null;
    private Slider _sliderScrollbar;
    
    private void Awake()
    {
        _trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        _sliderScrollbar = FindObjectOfType<Slider>();
    }
    private void Update()
    {
        if (rotationToggle.isOn)
        {
            rotationSpeed = new Vector3(0, _sliderScrollbar.value, 0);
            RotationController.Rotate(rotationSpeed, _tmpGameObject);
            _tmpText.text += _sliderScrollbar.value;   
        }
    }
    private void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        ARTrackedImage trackedImage = null;
        for (int i = 0; i < obj.added.Count; i++)
        {
            PrefabsInitialization();
        }
        for (int i = 0; i < obj.updated.Count; i++)
        {
            trackedImage = obj.updated[i];
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                string tmpName = trackedImage.referenceImage.name;
                Vector3 tmpPose = trackedImage.transform.position;
                _tmpText.text = tmpName;

                _tmpGameObject = spawnedPrefabs[tmpName];
                _tmpGameObject.transform.position = tmpPose;
                HideAllPrefabs();
                _tmpGameObject.SetActive(true);
            }
        }
        for (int i = 0; i < obj.removed.Count; i++)
        {
            Destroy(_tmpGameObject);
        }
    }
    private void PrefabsInitialization()
    {
        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
            spawnedPrefabs[prefab.name].SetActive(false);
        }
    }
    private void HideAllPrefabs()
    {
        foreach (var go in spawnedPrefabs.Values)
        {
            if (go.name != _tmpGameObject.name)
            {
                go.SetActive(false);
            }
        }
    }
}