/*******************************************************************
* FileName:     GameManager.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-8
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.Common;
using UFrame;
using Game.GlobalData;
using Game.MessageCenter;
using UFrame.MessageCenter;
using UFrame.Logger;
using System;

namespace Game
{
    public class GameManagerTick : TickBase
    {
        public Dictionary<string, UIPage> tickedPages = new Dictionary<string, UIPage>();
        public override void Tick(int deltaTimeMS)
        {
            //MessageManager不受暂停和停止限制
            MessageManager.GetInstance().Tick();

            if (!this.CouldRun())
            {
                return;
            }

            GameModuleManager.GetInstance().Tick(deltaTimeMS);

            foreach(var val in tickedPages.Values)
            {
                if (val != null)
                {
                    val.Tick(deltaTimeMS);
                }
            }
        }
    }

    public class GameManager : SingletonMono<GameManager>
    {
        GameManagerTick tickObj;

        static int moduleOrderID = 0;

        public int tickCount;

        bool isLoadedModule = false;

        public override void Awake()
        {
            base.Awake();
            Init();
            this.Run();
        }

        public void Start()
        {
            var pd = GlobalDataManager.GetInstance().playerData;
            if (pd.isFirstInstall)
            {
                //ThirdPartTA.Identify();
                //ThirdPartTA.StartTrack();
                //ThirdPartTA.Track(TAEventsMonitorEnum.register);
            }
            else
            {
                //ThirdPartTA.StartTrack();
            }

            //ThirdPartTA.Track(TAEventsMonitorEnum.gamestart);

            //ThirdPartTA.TrackAppInstall();
            //每次登录写last_login_time
            //LogWarp.Log("ThirdPartTA.UserSet.last_login_time");
            //var taParam = new Dictionary<string, object>();
            //taParam.Add("last_login_time", DateTime.Now);
            //ThirdPartTA.UserSet(taParam);

            LogWarp.Log("LoadingMgr.Inst.isRunning = true");
            LoadingMgr.Inst.isRunning = true;
        }

        public void Update()
        {
            this.Tick(Math_F.FloatToInt1000(Time.deltaTime));
        }

        public void Init()
        {
            MessageManager.GetInstance().SetCallbackNotFoundMessage(this.OnNotFoundMessage);

            MessageManager.GetInstance().Regist((int)GameMessageDefine.LoadZooSceneFinished, OnLoadZooSceneFinished);

            InitGlobaData();
            PageMgr.SetButtonSound(Config.globalConfig.getInstace().UiButtonSoynd);
            this.tickObj = new GameManagerTick();
#if UNITY_EDITOR
            tickCount = 0;
#endif
        }

        public void Release()
        {
#if UNITY_EDITOR
            tickCount = 0;
#endif
            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.LoadZooSceneFinished, OnLoadZooSceneFinished);
            UnLoadModule();
            //tickObj.tickedPages.Clear();
            RemoveAllTickPage();
            this.Stop();
        }

        public void Run()
        {
            tickObj.Run();
        }

        public void Stop()
        {
            tickObj.Stop();
        }
        public void Pause()
        {
            tickObj.Pause();
        }
        public void Pause(bool isPause)
        {
            tickObj.isPause = isPause;
        }

        public void Tick(int deltaTimeMS)
        {
#if UNITY_EDITOR
            tickCount++;
#endif
            tickObj.Tick(deltaTimeMS);
        }

        protected void AddPageToTick(UIPage page)
        {
            tickObj.tickedPages.Add(page.name, page);
        }

        protected void RemovePageFromTick(string pageName)
        {
            tickObj.tickedPages.Remove(pageName);
        }

        public void LoadSceneModule()
        {
            if (!this.isLoadedModule)
            {
                //移动
                GameModuleManager.GetInstance().AddMoudle(new MoveMovableEntityMoudle(moduleOrderID++));

            }

            GameModuleManager.GetInstance().Stop();
        }

        public void UnLoadModule()
        {
            GameModuleManager.GetInstance().Release();
        }

        public void RemoveAllTickPage()
        {
            tickObj.tickedPages.Clear();
        }

        protected void InitGlobaData()
        {
            GlobalDataManager.GetInstance().Init();
        }

        protected void OnLoadZooSceneFinished(Message msg)
        {
            LoadSceneModule();
            GameModuleManager.GetInstance().Run();
            
        }

        protected void OnUIMessage_AddToTick(Message msg)
        {
            var _msg = msg as UIMessage_AddToTick;

            tickObj.tickedPages.Add(_msg.page.name, _msg.page);
        }


        protected void OnNotFoundMessage(int messageID)
        {
            if (messageID < 10001)
            {
                LogWarp.LogErrorFormat("消息未注册  {0}", (UFrameBuildinMessage)messageID);
                return;
            }

            LogWarp.LogErrorFormat("消息未注册  {0}  {1}", (GameMessageDefine)messageID, messageID);
        }
    }

}
