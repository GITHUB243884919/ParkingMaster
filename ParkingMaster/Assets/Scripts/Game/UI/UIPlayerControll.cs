using Game;
using Game.GlobalData;
using Game.MessageCenter;
using UFrame.Logger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UFrame;
using UFrame.MessageCenter;
using UnityEngine;
using UnityEngine.UI;
using UFrame.MiniGame;

public class UIPlayerControll : UIPage
{
    
    public UIPlayerControll() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "uiprefab/UIPlayerControll";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //初始化控件
        RegistBtnAndClick("Panel/ctr/Button_Left", OnClickedLeft, false);
        RegistBtnAndClick("Panel/ctr/Button_Right", OnClickedRight, false);
        RegistBtnAndClick("Panel/ctr/Button_Continue", OnClickedContinue);
        RegistBtnAndClick("Panel/ctr/Button_Restart", OnClickedRestart);

    }

    protected void OnClickedLeft(Button btn)
    {
        MessageManager.GetInstance().Send((int)UFrameBuildinMessage.TurnLeftRightAngle);
    }

    protected void OnClickedRight(Button btn)
    {
        MessageManager.GetInstance().Send((int)UFrameBuildinMessage.TurnRightRightAngle);
    }

    protected void OnClickedContinue(Button btn)
    {
        //MessageManager.GetInstance().Send((int)UFrameBuildinMessage.TurnLeftRightAngle);
        MessageManager.GetInstance().Send((int)GameMessageDefine.StageContinue);
    }

    protected void OnClickedRestart(Button btn)
    {
        //MessageManager.GetInstance().Send((int)UFrameBuildinMessage.TurnLeftRightAngle);
        //MessageManager.GetInstance().Send((int)GameMessageDefine.StageContinue);
    }





}

