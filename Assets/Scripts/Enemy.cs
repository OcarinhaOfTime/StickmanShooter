using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    GameObject head;
    [SerializeField]
    GameObject bloodPrefab;
    AudioSource audioSource;
    public float speed = 3;

    private float timer = 0;
    private bool dead = false;
    private Animator animator;

    public int enemyID { get; set; }

    private void Start() {
        head = transform.GetChild(0).GetChild(0).FindChild("head").gameObject;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        timer += Time.deltaTime;
        if(!dead && timer > speed) {
            animator.SetTrigger("aim");
            timer = 0;
        }
    }

    public void HitPlayer() {
        GameController.Instance.HitPlayer();
        audioSource.Play();
    }

    public void Die() {
    }

    public void TakeHit() {
        var blood = Instantiate(bloodPrefab);
        blood.transform.position = head.transform.position;
        head.GetComponent<SpriteRenderer>().color = Color.clear;
        blood.SetActive(true);
        var z = Quaternion.LookRotation(-transform.position, Vector3.forward).eulerAngles.z + 90;
        blood.transform.position = head.transform.position;
        blood.transform.rotation = Quaternion.Euler(0, 0, z);

        animator.SetTrigger("die");
        Destroy(blood, 1);
        Destroy(gameObject, 1);
    }

    private void OnDestroy() {
        EnemySpawner.Instance.OnEnemyDie(this);
    }
}
