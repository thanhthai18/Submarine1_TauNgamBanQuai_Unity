using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_SubmarineMinigame1 : AbstractMyLoopBG_Minigame
{
    public float posReset;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Start()
    {
        //speedBG = 5;
    }

    private void Update()
    {
        //-20.31
        LoopBG(MyDirection.left, posReset);
    }
}
