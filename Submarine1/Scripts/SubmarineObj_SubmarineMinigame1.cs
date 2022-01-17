using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineObj_SubmarineMinigame1 : MonoBehaviour
{
    public Camera mainCamera;
    public Vector2 mouseCurrentPos;
    public bool isHoldMouse;
    public Transform gunPoint;
    public Bullet_SubmarineMinigame1 bulletPrefab;
    public bool isShoot;
    public float maxYCamera;
    public int HP;

    private void Awake()
    {
        isShoot = false;
    }

    private void Start()
    {
        HP = 3;
    }

    void Move()
    {
        transform.DOMoveY(mouseCurrentPos.y, 0.5f);
    }
  
    IEnumerator Shoot()
    {
        while (isShoot)
        {
            yield return new WaitForSeconds(0.2f);
            var tmpBullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Destroy(tmpBullet.gameObject, 3);
        }
    }

    public void CallShoot()
    {
        isShoot = true;
        StartCoroutine(Shoot());
    }

    void Update()
    {
        if (!GameController_SubmarineMinigame1.instance.isWin && !GameController_SubmarineMinigame1.instance.isLose && GameController_SubmarineMinigame1.instance.isBegin)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isHoldMouse = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isHoldMouse = false;
            }

            if (isHoldMouse)
            {
                mouseCurrentPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mouseCurrentPos = new Vector2(mouseCurrentPos.x, Mathf.Clamp(mouseCurrentPos.y, -maxYCamera + 1, maxYCamera - 1));
                Move();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            OnHit(collision.gameObject);
        }
    }

    public void OnHit(GameObject col)
    {
        if (HP > 0)
        {
            HP--;
            var tmpt = GameController_SubmarineMinigame1.instance.listHeal[HP];
            tmpt.DOFade(0, 1).OnComplete(() =>
            {
                Destroy(tmpt.gameObject);
            });
            if (HP == 0)
            {
                Destroy(gameObject);
                GameController_SubmarineMinigame1.instance.Lose();
            }
        }

        var tmpVFX = Instantiate(GameController_SubmarineMinigame1.instance.VFXDestroy, gameObject.transform.position, Quaternion.identity);
        tmpVFX.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() =>
        {
            Destroy(tmpVFX);
        });
        Destroy(col);
    }
}
