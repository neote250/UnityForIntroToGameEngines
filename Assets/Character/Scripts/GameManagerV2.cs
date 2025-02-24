 
using UnityEngine;

public class GameManagerV2 : SingletonC<GameManagerV2>
{
    [SerializeField] Event OnDestroyEvent;
    [SerializeField] IntEvent OnScoreEvent;
    [SerializeField] IntData intData;
    int score = 0;
    int highscore = 0;
    private void Start()
    {
        OnDestroyEvent?.Subscribe(OnDestroyed);
        OnScoreEvent?.Subscribe(OnAddScore);

        score = 0;
        highscore = PlayerPrefs.GetInt("highscore", 0);
        print("highscore: " + highscore);
    }
    public void OnDestroyed()
    {

        print("");
    }
    public void OnAddScore(int score)
    {
        this.score += score;
        if (score>highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
        }
        print(score);
    }
}
