using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom_SubmarineMinigame1 : MonoBehaviour
{
    public float speed = 4;


    private void Start()
    {
        Invoke(nameof(MyDestroy), 7);
    }

    void MyDestroy()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<SubmarineObj_SubmarineMinigame1>().OnHit(gameObject);
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            var tmpVFX = Instantiate(GameController_SubmarineMinigame1.instance.VFXDestroy, gameObject.transform.position, Quaternion.identity);
            tmpVFX.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() =>
            {
                Destroy(tmpVFX);
            });
        }
    }
}
