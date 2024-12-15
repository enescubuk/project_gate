using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pausePnl;
    public bool isPnlOpen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPnlOpen)
            {
                isPnlOpen = true;
                pausePnl.SetActive(true);
                Time.timeScale = 0.0f;
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                isPnlOpen = false;
                pausePnl.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
}
