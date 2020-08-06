using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ImageRecognitionExample : MonoBehaviour
{
    private GameObject prefabGm = GameObject.Find("Playstation_4");
    private ARTrackedImageManager _arTrackedImageManager;
    private void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }
    private void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (var trackedImage in obj.added)
         
        {
            GameObject textGm = GameObject.Find("TextCaption");
            TMP_Text txt = textGm.GetComponent<TMP_Text>();
            txt.text = trackedImage.referenceImage.name;
            // Debug.Log(trackedImage.name);
        }
    }
}