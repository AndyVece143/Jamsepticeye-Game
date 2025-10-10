using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("Title");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void CreditsScreen()
    {
        SceneManager.LoadScene("Credits");
    }
}
