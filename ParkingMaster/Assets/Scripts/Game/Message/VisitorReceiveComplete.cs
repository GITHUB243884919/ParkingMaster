using UFrame;
using UFrame.MessageCenter;

namespace Game.MessageCenter
{
    public class VisitorReceiveComplete : Message
    {
        /// <summary>
        /// 动物栏Id
        /// </summary>
        public int littleZooId;

        public static ObjectPool<VisitorReceiveComplete> pool = new ObjectPool<VisitorReceiveComplete>();

        public VisitorReceiveComplete()
        {
            this.messageID = (int)GameMessageDefine.VisitorReceiveComplete;
        }

        public void Init(int littleZooId)
        {
            this.littleZooId = littleZooId;
        }

        public override void Release()
        {
            littleZooId = 0;
            pool.Delete(this);
        }

        public static VisitorReceiveComplete Send(int littleZooId)
        {
            var msg = pool.New();
            msg.Init(littleZooId);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("VisitorReceiveComplete littleZooId={0}", littleZooId);
        }
    }
}
