using UFrame;
using UFrame.MessageCenter;

namespace Game.MessageCenter
{
    public class BuildingLevelup : Message
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
        /// 建筑属性
        /// </summary>
        public int buildingProperty;

        /// <summary>
        /// 当前等级
        /// </summary>
        public int currLevel;

        /// <summary>
        /// 其它可选参数
        /// </summary>
        public object others;

        public static ObjectPool<BuildingLevelup> pool = new ObjectPool<BuildingLevelup>();

        public BuildingLevelup()
        {
            this.messageID = (int)GameMessageDefine.BuildingLevelup;
        }

        public void Init(int buildingType, int buildingId, int buildingProperty, int currLevel, object others = null)
        {
            this.buildingType = buildingType;
            this.buildingId = buildingId;
            this.buildingProperty = buildingProperty;
            this.currLevel = currLevel;
            this.others = others;
        }

        public override void Release()
        {
            others = null;
            pool.Delete(this);
        }

        public static BuildingLevelup Send(int buildingType, int buildingId, int buildingProperty, int currLevel, object others = null)
        {
            var msg = pool.New();
            msg.Init(buildingType, buildingId, buildingProperty, currLevel, others);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("BuildingLevelup buildingType={0} buildingId={1} buildingProperty={2} currLevel={3} others={4}",
                buildingType, buildingId, buildingProperty, currLevel, others);
        }
    }
}
