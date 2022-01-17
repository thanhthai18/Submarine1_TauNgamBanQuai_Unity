using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniboss2_SubmarineMinigame1 : Miniboss1_SubmarineMinigame1
{
    public Vector2 targetBullet;

   

    private void Start()
    {
        HP = 30;
        startColor = GetComponent<SpriteRenderer>().color;
        transform.DOMoveX(GameController_SubmarineMinigame1.instance.startPosXEnemy + 2, 1).OnComplete(() =>
        {
            isAttack = true;
            Move();
            StartCoroutine(Attack());
        });
    }


    public override void Shoot()
    {
        var tmpSubmarine = GameController_SubmarineMinigame1.instance.submarineObj;
        for (int i = 0; i < 4; i++)
        {
            var tmpBullet = Instantiate(bulletEnemy, transform.position, Quaternion.identity);
            tmpBullet.transform.eulerAngles = new Vector3(0, 180, 0);
            tmpBullet.transform.DOMoveY(tmpBullet.transform.position.y + 2, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                targetBullet = new Vector2(tmpSubmarine.transform.position.x - 10, -1.0f * (tmpBullet.transform.position.y - tmpSubmarine.transform.position.y));
                tmpBullet.transform.DOMove(targetBullet, 5).SetEase(Ease.Linear).OnComplete(() => 
                {
                    Destroy(tmpBullet);
                });
            });
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            if (HP > 0)
            {
                HP--;
                GetComponent<SpriteRenderer>().DOColor(Color.red, 0.5f).OnComplete(() =>
                {
                    GetComponent<SpriteRenderer>().DOColor(startColor, 0.5f);
                });
                if (HP == 0)
                {
                    var tmpVFX = Instantiate(GameController_SubmarineMinigame1.instance.VFXDestroy, gameObject.transform.position, Quaternion.identity);
                    tmpVFX.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() =>
                    {
                        Destroy(tmpVFX);
                    });
                    Destroy(gameObject);
                    GameController_SubmarineMinigame1.instance.level = 3;
                    GameController_SubmarineMinigame1.instance.SetLevel();
                    GameController_SubmarineMinigame1.instance.Level3_Spawn();
                    StopAllCoroutines();
                }
            }


            Destroy(collision.gameObject);

        }
    }


}

