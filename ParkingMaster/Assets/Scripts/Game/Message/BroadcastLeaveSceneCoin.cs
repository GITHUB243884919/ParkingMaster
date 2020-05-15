using Game.MessageCenter;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.MessageCenter;
using UnityEngine;
using System.Numerics;

namespace Game.MessageCenter
{
    public class BroadcastLeaveSceneCoin : Message
    {
        /// <summary>
        /// key = sceneid， val = 金币数
        /// </summary>
        public Dictionary<int, BigInteger> sceneCoinDict = new Dictionary<int, BigInteger>();
        public static ObjectPool<BroadcastLeaveSceneCoin> pool = new ObjectPool<BroadcastLeaveSceneCoin>();
        public static BroadcastLeaveSceneCoin msg = null;

        public BroadcastLeaveSceneCoin()
        {
            this.messageID = (int)GameMessageDefine.BroadcastLeaveSceneCoin;
        }

        public void AddSceneCoin(int sceneID, BigInteger coin)
        {
            sceneCoinDict.Add(sceneID, coin);
        }

        public override void Release()
        {
            pool.Delete(this);
        }

        public static BroadcastLeaveSceneCoin PreSend()
        {
            if (msg == null)
            {
                msg = pool.New();
            }

            msg.sceneCoinDict.Clear();

            return msg;
        }

        public static BroadcastLeaveSceneCoin Send()
        {
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("BroadcastLeaveSceneCoin messageID={0}", messageID);
        }

    }
}

