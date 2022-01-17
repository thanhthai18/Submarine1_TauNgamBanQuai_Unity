using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1_SubmarineMinigame1 : MonoBehaviour
{
    public bool isAttack;

    private void Start()
    {
        isAttack = false;
        Invoke(nameof(SetAttack), 4);
    }

    public virtual void SetAttack()
    {
        isAttack = true;
    }


    public virtual void Attack(Transform targetObj)
    {
        transform.DOMove(targetObj.position, Vector2.Distance(transform.position, targetObj.position) / 3);
    }

    private void FixedUpdate()
    {
        if (isAttack)
        {
            if (GameController_SubmarineMinigame1.instance.submarineObj != null)
            {
                Attack(GameController_SubmarineMinigame1.instance.submarineObj.transform);
            }
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
                    if(GameController_SubmarineMinigame1.instance.level == 1)
                    {
                        GameController_SubmarineMinigame1.instance.OnCollisionLevel1();
                    }                   
                    else if(GameController_SubmarineMinigame1.instance.level == 2)
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
