using UFrame;
using UFrame.MessageCenter;

namespace Game.MessageCenter
{
    public class GuideMissionStateChanged : Message
    {
        public readonly static int ChangeDetail_NewTask = 1; // 新任务
        public readonly static int ChangeDetail_TaskProgress = 2; // 任务进度

        /// <summary>
        /// 任务Id
        /// </summary>
        public int taskId;

        /// <summary>
        /// 任务状态改变的细节
        /// </summary>
        public int detail;

        public static ObjectPool<GuideMissionStateChanged> pool = new ObjectPool<GuideMissionStateChanged>();

        public GuideMissionStateChanged()
        {
            this.messageID = (int)GameMessageDefine.GuideMissionStateChanged;
        }

        public void Init(int taskId, int detail)
        {
            this.taskId = taskId;
            this.detail = detail;
        }

        public override void Release()
        {
            taskId = 0;
            detail = 0;
            pool.Delete(this);
        }

        public static GuideMissionStateChanged Send(int taskId, int detail)
        {
            var msg = pool.New();
            msg.Init(taskId, detail);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("GuideMissionStateChanged taskId={0} detail={1}", taskId, detail);
        }
    }
}
