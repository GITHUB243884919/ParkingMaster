using UFrame;
using UFrame.MessageCenter;

namespace Game.MessageCenter
{
    public class AdWatchComplete : Message
    {
        public readonly static int AdType_Interstitial = 1; // 插屏
        public readonly static int AdType_RewardedVideo = 2; // 激励视频

        /// <summary>
        /// 广告类型
        /// </summary>
        public int adType;

        /// <summary>
        /// 广告位
        /// </summary>
        public string adTag;

        public static ObjectPool<AdWatchComplete> pool = new ObjectPool<AdWatchComplete>();

        public AdWatchComplete()
        {
            this.messageID = (int)GameMessageDefine.AdWatchComplete;
        }

        public void Init(int adType, string adTag)
        {
            this.adType = adType;
            this.adTag = adTag;
        }

        public override void Release()
        {
            pool.Delete(this);
        }

        public static AdWatchComplete Send(int adType, string adTag)
        {
            var msg = pool.New();
            msg.Init(adType, adTag);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("AdWatchComplete adType={0} adTag={1}", adType, adTag);
        }
    }
}
