/*******************************************************************
* FileName:     GlobalDataManager.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-16
* Description:  
* other:    
********************************************************************/


using UFrame.Logger;
using System;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.Common;
using UnityEngine;

namespace Game.GlobalData
{
    public partial class GlobalDataManager : Singleton<GlobalDataManager>, ISingleton
    {
        bool isInit = false;

        public I18N i18n { get; protected set; }

        public PlayerData playerData = null;

        public LogicTableResource logicTableResource;

        private Vector3 sceneForward = Vector3.zero;

        /// <summary>
        /// 场景正方向
        /// </summary>
        public Vector3 SceneForward
        {
            get
            {
                float[] v = Config.globalConfig.getInstace().SceneForward;
                sceneForward.x = v[0];
                sceneForward.y = v[1];
                sceneForward.z = v[2];
                return sceneForward;
            }
        }

        public void Init()
        {
            if (isInit)
            {
                return;
            }
            isInit = true;

            logicTableResource = new LogicTableResource();
            playerData = PlayerData.Load();
        }


        /// <summary>
        /// 不是所有都Release
        /// </summary>
        public void Release()
        {

        }





    }

}
