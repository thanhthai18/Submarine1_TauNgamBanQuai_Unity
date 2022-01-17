using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel3_SubmarineMinigame1 : MonoBehaviour
{
    public bool isAttack;
    public GameObject bulletPrefabLevel3;


    private void Start()
    {
        isAttack = false;
        Invoke(nameof(SetAttack), 4);
    }

    public void SetAttack()
    {
        isAttack = true;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (gameObject != null && isAttack)
        {
            Shoot();

            yield return new WaitForSeconds(Random.Range(2,5));

        }
    }

    void Shoot()
    {
        var tmpBullet = Instantiate(bulletPrefabLevel3, transform.position, Quaternion.identity);
        tmpBullet.transform.DOMoveX(-30, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(tmpBullet);
        });
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
                    GameController_SubmarineMinigame1.instance.OnCollisionLevel3();
                }
            }
            var tmpVFX = Instantiate(GameController_SubmarineMinigame1.instance.VFXDestroy, gameObject.transform.position, Quaternion.identity);
            tmpVFX.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() =>
            {
                Destroy(tmpVFX);
            });
            StopAllCoroutines();
            Destroy(gameObject);
            Destroy(collision.gameObject);

        }
    }

}
