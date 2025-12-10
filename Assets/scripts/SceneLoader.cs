using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Chuyển scene theo tên
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Chuyển scene theo index
    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    // Thoát game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
