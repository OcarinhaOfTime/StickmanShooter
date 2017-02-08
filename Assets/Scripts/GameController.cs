using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public enum GameState {
        Playing,
        Paused,
        GameOver
    }

    public static GameController Instance;

    [SerializeField]
    Text scoreTxt;
    [SerializeField]
    Text[] xxxs;
    public GameObject gameOverPanel;
    private Player player;
    int misses = 0;
    int _score = 0;
    public int score {
        get {
            return _score;
        }
        set {
            _score = value;
            scoreTxt.text = "Score " + _score;
        }
    }

    void Awake() {
        Instance = this;
    }

    private void Start() {
        player = GameObject.FindObjectOfType<Player>();
    }
    

    public void Miss() {
        if(misses < xxxs.Length - 1) {
            xxxs[misses++].color = Color.red;
        } else {
            xxxs[misses].color = Color.red;
            GameOver();
        }
    }

    public void HitPlayer() {
        player.TakeHit();
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() {
        yield return new WaitForSeconds(2);
        GameOver();
    }

    void GameOver() {
        gameOverPanel.SetActive(true);
    }
}
