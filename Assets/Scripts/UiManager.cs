using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;

    public GameObject bg;
    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject completePanel;


    private void Awake()
    {
        instance = this;
    }

    public void OnClickStart()
    {
        bg.SetActive(false);
        startPanel.SetActive(false);
    }

    public void OnClicPause()
    {
        //Time.timeScale = 0;
        bg.SetActive(true);
        pausePanel.SetActive(true);
    }

    public void OnClickResume()
    {
        //Time.timeScale = 1;
        bg.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void Restart()
    {
        //Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
         _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
    }
}
