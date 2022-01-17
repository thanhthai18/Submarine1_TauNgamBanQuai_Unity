using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel2_SubmarineMinigame1 : EnemyLevel1_SubmarineMinigame1
{


    private void Start()
    {
        isAttack = false;
        Invoke(nameof(Move), 4);
    }

    public void Move()
    {
        int ran = Random.Range(0, 2);

        if (ran == 0)
        {
            transform.DOMoveY(Random.Range(transform.position.y + 0.5f, GameController_SubmarineMinigame1.instance.startSizeCamera - 1), 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                isAttack = true;
                Attack(GameController_SubmarineMinigame1.instance.submarineObj.transform);
            });
        }
        else if (ran == 1)
        {
            transform.DOMoveY(Random.Range(-GameController_SubmarineMinigame1.instance.startSizeCamera + 1, transform.position.y - 0.5f), 1).SetEase(Ease.Linear).OnComplete(() =>
            {
                isAttack = true;
                Attack(GameController_SubmarineMinigame1.instance.submarineObj.transform);
            });
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<SubmarineObj_SubmarineMinigame1>().OnHit(gameObject);
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            if (GameController_SubmarineMinigame1.instance.listCurrentEnemy.Contains(gameObject))
            {
                GameController_SubmarineMinigame1.instance.listCurrentEnemy.Remove(gameObject);
                if (GameController_SubmarineMinigame1.instance.CheckNotEnemy())
                {
                    if (GameController_SubmarineMinigame1.instance.level == 2)
                    {
                        GameController_SubmarineMinigame1.instance.OnCollisionLevel2();
                    }
                    else if (GameController_SubmarineMinigame1.instance.level == 3)
                    {
                        GameController_SubmarineMinigame1.instance.OnCollisionLevel3();
                    }
                    
                }
            }
            var tmpVFX = Instantiate(GameController_SubmarineMinigame1.instance.VFXDestroy, gameObject.transform.position, Quaternion.identity);
            tmpVFX.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() =>
            {
                Destroy(tmpVFX);
            });
            Destroy(gameObject);
            Destroy(collision.gameObject);

        }
    }

}
