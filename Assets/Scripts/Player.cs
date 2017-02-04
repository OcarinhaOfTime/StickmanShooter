using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    Transform rightArm;
    [SerializeField]
    Transform leftArm;
    public void Shoot(Vector2 shootPos) {
        var look = (shootPos - (Vector2)transform.position).normalized;
        if(shootPos.x < 0) {
            var angle = Mathf.Acos(Vector2.Dot(Vector2.down, -look)) * Mathf.Rad2Deg;
            rightArm.rotation = Quaternion.Euler(0, 0, angle + 180);
        } else {
            var angle = Mathf.Acos(Vector2.Dot(Vector2.down, look)) * Mathf.Rad2Deg;
            leftArm.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
