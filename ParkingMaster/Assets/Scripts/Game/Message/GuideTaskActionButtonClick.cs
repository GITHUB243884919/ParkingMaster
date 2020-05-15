using UFrame;
using UFrame.MessageCenter;

namespace Game.MessageCenter
{
    public class GuideTaskActionButtonClick : Message
    {
        public static ObjectPool<GuideTaskActionButtonClick> pool = new ObjectPool<GuideTaskActionButtonClick>();

        public GuideTaskActionButtonClick()
        {
            this.messageID = (int)GameMessageDefine.GuideTaskActionButtonClick;
        }

        public void Init()
        {
            
        }

        public override void Release()
        {
            pool.Delete(this);
        }

        public static GuideTaskActionButtonClick Send()
        {
            var msg = pool.New();
            msg.Init();
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("GuideTaskActionButtonClick");
        }
    }
}
