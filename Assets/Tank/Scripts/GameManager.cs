using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;



public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get{ return instance;} }

    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject titleText;

    enum eState
    {
        TITLE
        , GAME
        , WIN
        , LOSE
    }

    eState state = eState.TITLE;
    float timer = 0;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        switch (state)
        {
            case eState.TITLE:
                titleUI.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    OnStartGame();
                }
                break;
            case eState.GAME:
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    titleText.GetComponent<TextMeshProUGUI>().text = "Paused";
                    titleUI.SetActive(true);
                }
                break;
            case eState.WIN:
                titleText.GetComponent<TextMeshProUGUI>().text = "Victory";
                titleUI.SetActive(true);
                EndScreenTimer();
                break;
            case eState.LOSE:
                titleText.GetComponent<TextMeshProUGUI>().text = "Defeat";
                titleUI.SetActive(true);
                EndScreenTimer();
                break;
            default:
                break;
        }


    }
    public void OnStartGame()
    {
        titleUI.SetActive(false);
        state = eState.GAME;
    }

    public void SetGameOver() => state = eState.LOSE;
    public void SetGameWin() => state = eState.WIN;
    IEnumerator EndScreenTimer()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
