using UFrame;
using UFrame.MessageCenter;

namespace Game.MessageCenter
{
    public class BuildingUnlock : Message
    {
        /// <summary>
        /// 建筑类型
        /// </summary>
        public int buildingType;

        /// <summary>
        /// 建筑Id
        /// </summary>
        public int buildingId;

        /// <summary>
        /// 其它可选参数
        /// </summary>
        public object others;

        public static ObjectPool<BuildingUnlock> pool = new ObjectPool<BuildingUnlock>();

        public BuildingUnlock()
        {
            this.messageID = (int)GameMessageDefine.BuildingUnlock;
        }

        public void Init(int buildingType, int buildingId, object others = null)
        {
            this.buildingType = buildingType;
            this.buildingId = buildingId;
            this.others = others;
        }

        public override void Release()
        {
            others = null;
            pool.Delete(this);
        }

        public static BuildingUnlock Send(int buildingType, int buildingId, object others = null)
        {
            var msg = pool.New();
            msg.Init(buildingType, buildingId, others);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("BuildingLevelup buildingType={0} buildingId={1} others={2}",
                buildingType, buildingId, others);
        }
    }
}
