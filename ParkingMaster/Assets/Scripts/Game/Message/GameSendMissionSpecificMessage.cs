using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.MessageCenter;
using UnityEngine;
namespace Game.MessageCenter
{
    public class GameSendMissionSpecificMessage : Message
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public int taskType;

        /// <summary>
        /// 任务参数1
        /// </summary>
        public int taskparam1;

        /// <summary>
        /// 任务参数2
        /// </summary>
        public int taskparam2;

        /// <summary>
        /// 进度
        /// </summary>
        public int nowVal;

        public static ObjectPool<GameSendMissionSpecificMessage> pool = new ObjectPool<GameSendMissionSpecificMessage>();

        public void Init(int messageID, int taskType, int taskparam1, int taskparam2, int nowVal)
        {
            this.messageID = messageID;
            this.taskType = taskType;
            this.taskparam1 = taskparam1;
            this.taskparam2 = taskparam2;
            this.nowVal = nowVal;
        }

        public override void Release()
        {
            pool.Delete(this);
        }
        /// <summary>
        /// 发送任务明细相关的消息
        /// </summary>
        /// <param name="taskType">任务类型</param>
        /// <param name="taskparam1">任务参数1</param>
        /// <param name="taskparam2">任务参数2</param>
        /// <param name="nowVal">任务参数相关的当前进度</param>
        /// <returns></returns>
        public static GameSendMissionSpecificMessage Send(int taskType, int taskparam1, int taskparam2, int nowVal)
        {
            var msg = pool.New();
            msg.Init((int)GameMessageDefine.SendGameMissionSpecificMessage, taskType, taskparam1, taskparam2, nowVal);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("SetDetailValueOfPlayerData messageID = {0}, taskType = {1}, taskparam1={2}, taskparam2={3} , nowVal={4}",
                messageID, taskType, taskparam1, taskparam2, nowVal);
        }
    }
}
