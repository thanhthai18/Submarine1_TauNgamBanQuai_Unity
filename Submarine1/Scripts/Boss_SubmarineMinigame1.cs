using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SubmarineMinigame1 : Miniboss2_SubmarineMinigame1
{
    private void Start()
    {
        HP = 50;
        startColor = GetComponent<SpriteRenderer>().color;
        transform.DOMoveX(GameController_SubmarineMinigame1.instance.startPosXEnemy + 2, 1).OnComplete(() =>
        {
            isAttack = true;
            Move();
            StartCoroutine(Attack());
            StartCoroutine(MoveAtk());
        });
    }

    IEnumerator MoveAtk()
    {
        while(gameObject != null && isAttack)
        {
            transform.DOMove(GameController_SubmarineMinigame1.instance.submarineObj.transform.position, 4).SetEase(Ease.InQuad).OnComplete(() => 
            {
                transform.DOMoveX(GameController_SubmarineMinigame1.instance.startPosXEnemy, 0.5f).SetEase(Ease.InQuad);
            });

            yield return new WaitForSeconds(7);
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
                    GameController_SubmarineMinigame1.instance.Win();
                    StopAllCoroutines();
              
                }
            }


            Destroy(collision.gameObject);

        }
    }
}
