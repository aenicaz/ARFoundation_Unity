using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnObjectOnPlane : MonoBehaviour
{
    private ARRaycastManager _raycastManager;
    private GameObject _spawnedGameObject;
    private List<GameObject> _placedPrefab = new List<GameObject>();

    [SerializeField] private int maxPrefabSpawnCount = 0;

    [SerializeField] private TMP_Text _tmpText;
    private int placedPrefabCount;
    
    public GameObject placebleGameObject;
    
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {   
            return;
        }

        if (_raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPos = s_Hits[0].pose;
            if (placedPrefabCount < maxPrefabSpawnCount)
            {
                SpawnPrefab(hitPos);
            }
        }
    }

    public void SetPrefabType(GameObject prefabType)
    {
        placebleGameObject = prefabType;
        _tmpText.text = placebleGameObject.name;
    }

    private void SpawnPrefab(Pose hitPose)
    {
        _spawnedGameObject = Instantiate(placebleGameObject, hitPose.position, hitPose.rotation);
        _placedPrefab.Add(_spawnedGameObject);
        placedPrefabCount++;
    }

    public void Clear()
    {
        _tmpText.text = "Clear";
        foreach (var prefab in _placedPrefab)
        {
            prefab.SetActive(false);
        }
        _placedPrefab.Clear();
        placedPrefabCount = 0;
    }
}
