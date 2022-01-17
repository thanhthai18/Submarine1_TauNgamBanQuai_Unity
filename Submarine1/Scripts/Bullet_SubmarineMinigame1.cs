using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_SubmarineMinigame1 : MonoBehaviour
{
    public float speedBullet;

    private void Start()
    {
        speedBullet = 5;
    }

    private void Update()
    {
        speedBullet += 0.1f;
        transform.Translate(Vector3.right * speedBullet * Time.deltaTime);
    }
}
