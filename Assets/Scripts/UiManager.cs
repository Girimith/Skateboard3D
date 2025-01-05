using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;

    [HideInInspector] public bool gameStart = false;

    public GameObject bg;
    public GameObject startPanel;
    public GameObject pausePanel;
    public GameObject completePanel;
    public GameObject gameOverPanel;


    private void Awake()
    {
        instance = this;
    }

    public void OnClickStart()
    {
        bg.SetActive(false);
        startPanel.SetActive(false);
        gameStart = true;
    }

    public void OnClicPause()
    {
        bg.SetActive(true);
        pausePanel.SetActive(true);
    }

    public void OnClickResume()
    {
        bg.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void Restart()
    {

        SceneManager.LoadScene(0);
    }

    public void OnApplicationQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    // Update is called once per frame
    void Update()
    {
         _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
    }
}
