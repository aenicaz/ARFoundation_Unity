using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public enum Scenes
    {
        Exit = 0,
        Menu = 1,
        ImageRecognition = 2,
        SurfaceRecognition = 3,
    }
    public void SwitchScene(int scene)
    {
        switch ((Scenes)scene)
        {
            case ((Scenes)0):
                Application.Quit();
                break;
            case ((Scenes)1):
                SceneManager.LoadScene("Scenes/MainMenu/Menu");
                break;
            case ((Scenes)2):
                SceneManager.LoadScene("Scenes/ImageRecognition/ImageRecogition");
                break;
            case ((Scenes)3):
                SceneManager.LoadScene("Scenes/SurfaceRecogintion/main");
                break;
        }
    }
}
