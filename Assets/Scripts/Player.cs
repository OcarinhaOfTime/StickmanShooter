using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    GameObject x;
    [SerializeField]
    Transform rightArm;
    [SerializeField]
    Transform leftArm;
    [SerializeField]
    GameObject shotFXLeft;
    [SerializeField]
    GameObject shotFXRight;


    [SerializeField]
    GameObject bloodPrefab;

    [SerializeField]
    GameObject head;
    private Animator animator;

    private Vector2 pointerPos;
    private AudioSource audioSource;
    private Collider2D col;
    private bool dead = false;

    private void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if(!dead && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
            pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            col = Physics2D.OverlapPoint(pointerPos);
            if(pointerPos.x < 0) {
                animator.SetTrigger("aim_right");
            } else {
                animator.SetTrigger("aim_left");
            }
        }
    }

    void InstantiateFX(GameObject fxPrefab) {
        var fx = Instantiate(fxPrefab);
        fx.transform.position = fxPrefab.transform.position;
        fx.SetActive(true);
        Destroy(fx, .5f);
    }

    public void TakeHit() {
        if(dead)
            return;
        var blood = Instantiate(bloodPrefab);
        blood.transform.position = head.transform.position;
        blood.transform.rotation = Quaternion.Euler(0, 0, 90);
        head.GetComponent<SpriteRenderer>().color = Color.clear;
        blood.SetActive(true);
        dead = true;
    }

    public void ShotLeft() {
        InstantiateFX(shotFXLeft);
        Shot();
    }

    public void ShotRight() {
        InstantiateFX(shotFXRight);
        Shot();
    }

    void Shot() {
        if(col != null && col.CompareTag("Enemy")) {
            col.GetComponent<Enemy>().TakeHit();
            audioSource.pitch = 1;
            GameController.Instance.score++;
        } else {
            var x_instance = Instantiate(x);
            x_instance.transform.position = pointerPos;
            Destroy(x_instance, 1);
            audioSource.pitch = 3;
            GameController.Instance.Miss();
        }

        audioSource.Play();
    }
}
