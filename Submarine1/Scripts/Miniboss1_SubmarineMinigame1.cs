using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniboss1_SubmarineMinigame1 : MonoBehaviour
{
    public bool isAttack = false;
    public GameObject bulletEnemy;
    public int HP;
    public Color startColor;

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

    public virtual IEnumerator Attack()
    {
        while (gameObject != null && isAttack)
        {
            Shoot();

            yield return new WaitForSeconds(3);

        }
    }

    public virtual void Shoot()
    {
        var tmpBullet = Instantiate(bulletEnemy, transform.position, Quaternion.identity);
        tmpBullet.transform.DOMoveX(-30, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(tmpBullet);
        });
    }

    public virtual void Move()
    {
        transform.DOMoveY(2.65f, 2).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOMoveY(-3.65f, 2).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (gameObject != null)
                {
                    Move();
                }
            });
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            if(HP > 0)
            {
                HP--;
                GetComponent<SpriteRenderer>().DOColor(Color.red, 0.5f).OnComplete(() =>
                {
                    GetComponent<SpriteRenderer>().DOColor(startColor, 0.5f);
                });
                if(HP == 0)
                {
                    var tmpVFX = Instantiate(GameController_SubmarineMinigame1.instance.VFXDestroy, gameObject.transform.position, Quaternion.identity);
                    tmpVFX.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() =>
                    {
                        Destroy(tmpVFX);
                    });
                    Destroy(gameObject);
                    GameController_SubmarineMinigame1.instance.level = 2;
                    GameController_SubmarineMinigame1.instance.SetLevel();
                    GameController_SubmarineMinigame1.instance.Level2_Spawn();
                    StopAllCoroutines();
                }
            }
            
           
            Destroy(collision.gameObject);

        }
    }
}
