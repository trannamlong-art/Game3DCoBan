using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // Hàm gọi khi bấm nút Chơi Lại
    public void RestartGame()
    {
        Time.timeScale = 1f; // phòng trường hợp bạn có pause game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // (Nếu bạn muốn thêm nút Thoát game sau này)
    public void QuitGame()
    {
        Application.Quit();
    }
}
