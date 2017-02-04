using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController Instance;
    public GameObject x;

    [SerializeField]
    Text scoreTxt;
    [SerializeField]
    Text[] xxxs;
    public GameObject gameOverPanel;
    private Player player;
    private AudioSource audioSource;
    int misses = 0;
    int _score = 0;
    int score {
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
        audioSource = GetComponent<AudioSource>();
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
            Vector2 pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var col = Physics2D.OverlapPoint(pointerPos);
            if(col != null && col.CompareTag("Enemy")) {
                col.GetComponent<Enemy>().TakeHit();
                audioSource.pitch = 1;
                score++;
            } else {
                var x_instance = Instantiate(x);
                x_instance.transform.position = pointerPos;
                Destroy(x_instance, 1);
                audioSource.pitch = 3;
                Miss();
            }

            audioSource.Play();
            player.Shoot(pointerPos);
        }        
    }

    void Miss() {
        if(misses < xxxs.Length - 1) {
            xxxs[misses++].color = Color.red;
        } else {
            xxxs[misses].color = Color.red;
            GameOver();
        }
    }

    public void GameOver() {
        gameOverPanel.SetActive(true);
    }
}
