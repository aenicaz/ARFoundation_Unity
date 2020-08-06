using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField] private GameObject[] placeablePrefabs;
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private TMP_Text _tmpText2;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager _trackedImageManager;

    private void Awake()
    {
        _trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        
        foreach (GameObject prefab in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
            spawnedPrefabs[prefab.name].SetActive(false);
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
        foreach (ARTrackedImage trackedImage in obj.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in obj.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in obj.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }
    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        foreach (var go in spawnedPrefabs.Values)
        {
            if (go.name != name)
            {
                go.SetActive(false);
            }
        }
        prefab.SetActive(true);
        _tmpText2.text = $"Prefab name: {prefab.name}\n Image name: {trackedImage.referenceImage.name}";
       
    }

    public void Clear()
    {
        _tmpText.text = "All object has been removed";
        foreach (var prefab in spawnedPrefabs.Values)
        {
            prefab.SetActive(false);
        }
    }
}