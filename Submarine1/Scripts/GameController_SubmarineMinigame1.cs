using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_SubmarineMinigame1 : MonoBehaviour
{
    public static GameController_SubmarineMinigame1 instance;

    public bool isWin, isLose, isBegin;
    public Camera mainCamera;
    public float startSizeCamera;
    public SubmarineObj_SubmarineMinigame1 submarineObj;
    public int level;
    public int stageOfLevel;
    public int fullStageOfLevel;
    public List<GameObject> listCurrentEnemy = new List<GameObject>();
    public EnemyLevel1_SubmarineMinigame1 enemyLv1Prefab;
    public EnemyLevel2_SubmarineMinigame1 enemyLv2Prefab;
    public EnemyLevel3_SubmarineMinigame1 enemyLv3Prefab;
    public float posXEnemy, posYEnemy, posXEnemy_3, posYEnemy_3, startPosXEnemy;
    public GameObject currentPosSpawnEnemyWave;
    public GameObject VFXDestroy;
    public Boom_SubmarineMinigame1 boomPrefab;
    public float posXSpawnBoom;
    public Miniboss1_SubmarineMinigame1 miniboss1Prefab;
    public Miniboss2_SubmarineMinigame1 miniboss2Prefab;
    public Boss_SubmarineMinigame1 boss3Prefab;
    public GameObject panelHeal;
    public List<Image> listHeal = new List<Image>();


    //public List<Transform> listWavePosLv2 = new List<Transform>();
    //public List<Transform> listWavePosLv3 = new List<Transform>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);

        isWin = false;
        isLose = false;
        isBegin = false;
    }

    private void Start()
    {
        panelHeal.SetActive(false);
        SetSizeCamera();
        startSizeCamera = mainCamera.orthographicSize;
        submarineObj.maxYCamera = startSizeCamera;
        mainCamera.orthographicSize *= 2.0f / 5;
        posXSpawnBoom = startSizeCamera * Screen.width * 1.0f / Screen.height + 3;
        Intro();
    }

    void SetSizeCamera()
    {
        float f1, f2;
        f1 = 16.0f / 9;
        f2 = Screen.width * 1.0f / Screen.height;
        mainCamera.orthographicSize *= f1 / f2;
    }

    void Intro()
    {
        mainCamera.DOOrthoSize(startSizeCamera, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            isBegin = true;
            submarineObj.transform.DOMoveX(-(startSizeCamera * Screen.width * 1.0f / Screen.height) + 2.5f, 2).OnComplete(() =>
            {
                submarineObj.CallShoot();
                level = 1;
                SetLevel();
                panelHeal.SetActive(true);
                Level1_Spawn();
                StartCoroutine(SpawnBoom());
            });
        });
    }

    public void SetLevel()
    {
        stageOfLevel = 0;
        if (level == 1)
        {
            startPosXEnemy = 3f;
            fullStageOfLevel = 10;
        }
        else if (level == 2)
        {
            startPosXEnemy = 3f;
            fullStageOfLevel = 14;
        }
        else if (level == 3)
        {
            posXEnemy_3 = 6.67f;
            startPosXEnemy = 3f;
            fullStageOfLevel = 20;
        }
    }

    public bool CheckNotEnemy()
    {
        if (listCurrentEnemy.Count != 0)
        {
            return false;
        }
        else return true;
    }

    void SetPosXEnemy()
    {
        posXEnemy = currentPosSpawnEnemyWave.transform.position.x;
        
    }
    void SetPosYEnemyForLevel12()
    {
        posYEnemy = Random.Range(-startSizeCamera + 2, startSizeCamera - 1);
    }
    void SetPosYEnemyForLevel3()
    {
        posYEnemy_3 = currentPosSpawnEnemyWave.transform.position.y;
    }


    public void Level1_Spawn()
    {
        posXEnemy = startPosXEnemy;
        List<GameObject> tmpListObjVaoViTri = new List<GameObject>();
        SetPosYEnemyForLevel12();
        //1
        SpawnLevel(1);
        currentPosSpawnEnemyWave.transform.position = new Vector2(currentPosSpawnEnemyWave.transform.position.x + 20, currentPosSpawnEnemyWave.transform.position.y);
        tmpListObjVaoViTri.Add(currentPosSpawnEnemyWave);
        SetPosXEnemy();
        //2
        SpawnLevel(1);
        tmpListObjVaoViTri.Add(currentPosSpawnEnemyWave);
        SetPosXEnemy();
        //3
        SpawnLevel(1);
        tmpListObjVaoViTri.Add(currentPosSpawnEnemyWave);

        tmpListObjVaoViTri.ForEach(s => s.transform.DOMoveX(s.transform.position.x - 20, 1).OnComplete(() =>
        {
            s.GetComponent<Collider2D>().enabled = true;
        }));
    }

    public void Level2_Spawn()
    {
        List<GameObject> tmpListObjVaoViTri = new List<GameObject>();
        posXEnemy = startPosXEnemy;
        SetPosYEnemyForLevel12();
        //1
        SpawnLevel(2);
        currentPosSpawnEnemyWave.transform.position = new Vector2(currentPosSpawnEnemyWave.transform.position.x + 20, currentPosSpawnEnemyWave.transform.position.y);
        tmpListObjVaoViTri.Add(currentPosSpawnEnemyWave);
        SetPosXEnemy();
        //2
        SpawnLevel(2);
        tmpListObjVaoViTri.Add(currentPosSpawnEnemyWave);
        SetPosXEnemy();
        //3
        SpawnLevel(2);
        tmpListObjVaoViTri.Add(currentPosSpawnEnemyWave);

        tmpListObjVaoViTri.ForEach(s => s.transform.DOMoveX(s.transform.position.x - 20, 1).OnComplete(() =>
        {
            s.GetComponent<Collider2D>().enabled = true;
        }));
    }

    public void Level3_Spawn()
    {
        posYEnemy_3 = Random.Range(-startSizeCamera + 2, startSizeCamera - 1) + 0.8f;
        List<GameObject> tmpListObjVaoViTri = new List<GameObject>();
        //1
        SpawnLevel(3);
        currentPosSpawnEnemyWave.transform.position = new Vector2(currentPosSpawnEnemyWave.transform.position.x, currentPosSpawnEnemyWave.transform.position.y - 10);
        tmpListObjVaoViTri.Add(currentPosSpawnEnemyWave);
        SetPosYEnemyForLevel3();
        //2
        SpawnLevel(3);
        tmpListObjVaoViTri.Add(currentPosSpawnEnemyWave);
        SetPosYEnemyForLevel3();
        //3
        SpawnLevel(3);
        tmpListObjVaoViTri.Add(currentPosSpawnEnemyWave);

        tmpListObjVaoViTri.ForEach(s => s.transform.DOMoveY(s.transform.position.y + 10, 1).OnComplete(() =>
        {
            s.GetComponent<Collider2D>().enabled = true;
        }));
    }

    void SpawnLevel(int enemyType)
    {
        if (enemyType == 1)
        {
            var tmpEnemy = Instantiate(enemyLv1Prefab, new Vector2(posXEnemy + 1.7f, posYEnemy), Quaternion.identity);
            currentPosSpawnEnemyWave = tmpEnemy.gameObject;
            tmpEnemy.transform.eulerAngles = new Vector3(0, 180, 0);
            listCurrentEnemy.Add(tmpEnemy.gameObject);
        }
        else if (enemyType == 2)
        {
            var tmpEnemy = Instantiate(enemyLv2Prefab, new Vector2(posXEnemy + 1.7f, posYEnemy), Quaternion.identity);
            currentPosSpawnEnemyWave = tmpEnemy.gameObject;
            tmpEnemy.transform.eulerAngles = new Vector3(0, 180, 0);
            listCurrentEnemy.Add(tmpEnemy.gameObject);
        }
        else if (enemyType == 3)
        {
            var tmpEnemy = Instantiate(enemyLv3Prefab, new Vector2(posXEnemy_3, posYEnemy_3 - 0.8f), Quaternion.identity);
            currentPosSpawnEnemyWave = tmpEnemy.gameObject;
            tmpEnemy.transform.eulerAngles = new Vector3(0, 180, 0);
            listCurrentEnemy.Add(tmpEnemy.gameObject);
        }
    }

    IEnumerator SpawnBoom()
    {
        while(!isLose && !isWin)
        {
            var tmpBoom = Instantiate(boomPrefab, new Vector2(posXSpawnBoom, Random.Range(-startSizeCamera + 2, startSizeCamera - 1)), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }

    public void OnCollisionLevel1()
    {
        if (stageOfLevel < 4)
        {
            Level1_Spawn();
            stageOfLevel++;
        }
        else if (stageOfLevel < fullStageOfLevel)
        {
            Level1_Spawn();
            Level1_Spawn();
            stageOfLevel++;
        }
        else if (stageOfLevel == fullStageOfLevel)
        {
            Debug.Log("Miniboss1");
            level = -1;
            Instantiate(miniboss1Prefab, new Vector2(15, 0), Quaternion.identity);
        }
    }
    public void OnCollisionLevel2()
    {
        if (stageOfLevel < 4)
        {
            int ran = Random.Range(0, 2);
            if (ran == 0)
            {
                Level1_Spawn();
            }
            else
            {
                Level2_Spawn();
            }
            stageOfLevel++;
        }
        else if (stageOfLevel >= 4 && stageOfLevel < 10)
        {
            Level1_Spawn();
            Level2_Spawn();
            stageOfLevel++;

        }
        else if (instance.stageOfLevel < fullStageOfLevel)
        {
            Level1_Spawn();
            Level1_Spawn();
            Level2_Spawn();
            stageOfLevel++;
        }

        else if (stageOfLevel == fullStageOfLevel)
        {
            Debug.Log("Miniboss2");
            level = -2;
            Instantiate(miniboss2Prefab, new Vector2(15, 0), Quaternion.identity);
        }
    }

    public void OnCollisionLevel3()
    {
        if (stageOfLevel < 4)
        {
            int ran = Random.Range(0, 3);
            if (ran == 0)
            {
                Level1_Spawn();
            }
            else if (ran == 1)
            {
                Level2_Spawn();
            }
            else
            {
                Level3_Spawn();
            }
            stageOfLevel++;
        }
        else if (stageOfLevel >= 4 && stageOfLevel < 8)
        {
            int ran = Random.Range(0, 3);
            if (ran == 0)
            {
                Level1_Spawn();
                Level2_Spawn();
            }
            else if (ran == 1)
            {
                Level2_Spawn();
                Level3_Spawn();
            }
            else if (ran == 2)
            {
                Level1_Spawn();
                Level3_Spawn();
            }

            stageOfLevel++;
        }
        else if (stageOfLevel >= 8 && stageOfLevel < 14)
        {
            int ran = Random.Range(0, 3);
            int ran2 = Random.Range(0, 3);
            if (ran == 0)
            {
                if (ran2 == 0)
                {
                    int ran3 = Random.Range(0, 2);
                    if (ran3 == 0)
                    {
                        Level1_Spawn();
                    }
                    else
                    {
                        Level2_Spawn();
                    }
                }
                Level1_Spawn();
                Level2_Spawn();
            }
            else if (ran == 1)
            {
                if (ran2 == 0)
                {
                    int ran3 = Random.Range(0, 2);
                    if (ran3 == 0)
                    {
                        Level2_Spawn();
                    }
                    else
                    {
                        Level3_Spawn();
                    }
                }
                Level2_Spawn();
                Level3_Spawn();
            }
            else if (ran == 2)
            {
                if (ran2 == 0)
                {
                    int ran3 = Random.Range(0, 2);
                    if (ran3 == 0)
                    {
                        Level1_Spawn();
                    }
                    else
                    {
                        Level3_Spawn();
                    }
                }
                Level1_Spawn();
                Level3_Spawn();
            }
            stageOfLevel++;

        }
        else if (stageOfLevel >= 14 && stageOfLevel < 18)
        {
            Level1_Spawn();
            Level2_Spawn();
            Level3_Spawn();
            stageOfLevel++;

        }
        else if (stageOfLevel >= 18 && stageOfLevel < fullStageOfLevel)
        {
            int ran2 = Random.Range(0, 3);
            if (ran2 == 0)
            {
                int ran3 = Random.Range(0, 3);
                if (ran3 == 0)
                {
                    Level1_Spawn();
                }
                else if(ran3 == 1)
                {
                    Level2_Spawn();
                }
                else if(ran3 == 2)
                {
                    Level3_Spawn();
                }
            }
            Level1_Spawn();
            Level2_Spawn();
            Level3_Spawn();
            stageOfLevel++;

        }
        else if (stageOfLevel == fullStageOfLevel)
        {
            Debug.Log("Final Boss");
            level = -3;
            Instantiate(boss3Prefab, new Vector2(15, 0), Quaternion.identity);
        }
    }

    public void Win()
    {
        isWin = true;
        StopAllCoroutines();
        Debug.Log("Win");
    }

    public void Lose()
    {
        isLose = true;
        StopAllCoroutines();
        Debug.Log("Lose");
    }
}
