using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsScreen;
    public GameObject creditsScreen;
    public GameObject mainScreen;

    public void Play()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        mainScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void set2Menu()
    {
        mainScreen.SetActive(true);
        settingsScreen.SetActive(false);
    }
    public void Creadits()
    {
        mainScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void cre2Menu()
    {
        mainScreen.SetActive(true);
        creditsScreen.SetActive(false);
    }
}