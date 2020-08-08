using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void SwitchScene(int number)
    {
        switch (number)
        {
            case 0:
                Application.Quit();
                break;
            case 1:
                SceneManager.LoadScene("Scenes/ImageRecognition/ImageRecogition");
                break;
            case 2:
                SceneManager.LoadScene("Scenes/SurfaceRecogintion/main");
                break;
        }
    }
}
