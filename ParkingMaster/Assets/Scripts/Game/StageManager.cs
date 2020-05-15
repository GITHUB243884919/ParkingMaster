using Game.GlobalData;
using Game.MessageCenter;
using Game.MiniGame;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.BehaviourFloat;
using UFrame.Common;
using UFrame.MessageCenter;
using UFrame.MiniGame;
using UFrame.OrthographicCamera;
using UnityEngine;

namespace Game
{
    public class StageManager : Singleton<StageManager>, ISingleton
    {
        #region Road
        /// <summary>
        /// 加载的路块数
        /// </summary>
        int numLoadedRoad = 0;

        /// <summary>
        /// 路资源的长度
        /// </summary>
        public const int Road_Len = 17;
        #endregion

        #region Segment
        int numNPCCarPosSegment = 0;
        const int segmentRow = 4;
        const int segmentCol = 2;
        const int cellHeight = 4;
        const int cellWidth = 6;
        Vector3[,] oneSegmentFullPos = new Vector3[segmentRow, segmentCol];
        #endregion

        EntityDrivedCar player = null;

        Vector3 traceCamPos {
            get { return TraceCamera.GetInstance().pos; }
            set { TraceCamera.GetInstance().pos = value; }
        }

        Vector3 orgtraceCamPos;

        GameObject roadInScreen = null;
        GameObject roadOutScreen = null;

        List<GameObject> NPCCarInScreen = new List<GameObject>();
        List<GameObject> NPCCarOutScreen = new List<GameObject>();

        public void Init()
        {
            GenSegmentFullPos();
            MessageManager.GetInstance().Regist((int)GameMessageDefine.TriggerLoadRoad, OnTriggerLoadRoad);
            MessageManager.GetInstance().Regist((int)GameMessageDefine.StageContinue, OnStageContinue);
            MessageManager.GetInstance().Regist((int)UFrameBuildinMessage.ArrivedRightAngle, OnParkingSucess);

        }

        public void Release()
        {
            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.TriggerLoadRoad, OnTriggerLoadRoad);
            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.StageContinue, OnStageContinue);
            MessageManager.GetInstance().UnRegist((int)UFrameBuildinMessage.ArrivedRightAngle, OnParkingSucess);
        }

        public void Load()
        {
            SceneMgr.Inst.LoadSceneAsync("Stage_01", Callback_LoadedOrgScene, null);
        }

        public void UnLoad()
        {

        }

        protected void Callback_LoadedOrgScene()
        {
            //生成两块路(屏幕内1+屏幕外1)
            //LoadRoad();
            //LoadRoad();
            roadInScreen = LoadRoadNew("Prefabs/Road/road_2");
            roadOutScreen = LoadRoadNew("Prefabs/Road/road_3");
            GenNPCCarNew(1f, NPCCarInScreen);
            GenNPCCarNew(1f, NPCCarOutScreen);

            //玩家
            GenPlayerCar(Vector3.zero);
            //加相机跟踪
            orgtraceCamPos = traceCamPos;
            TraceCamera.GetInstance().BeginTrace(player.GetTrans(), false);
            PageMgr.ShowPage<UIPlayerControll>();

            //GenNPCCar(1f);
            //GenNPCCar(1f);


            MessageManager.GetInstance().Send((int)GameMessageDefine.LoadZooSceneFinished);
        }

        protected void Continue()
        {
            var pos = new Vector3(0, player.position.y, player.position.z);
            //player.Deactive();
            EntityManager.GetInstance().RemoveFromEntityMovables(player);
            GenPlayerCar(pos);
            //跟踪相机的x轴复原后再跟踪
            traceCamPos = new Vector3(orgtraceCamPos.x, traceCamPos.y, traceCamPos.z);
            TraceCamera.GetInstance().BeginTrace(player.GetTrans(), false);
        }

        /// <summary>
        /// 生成玩家的car
        /// </summary>
        void GenPlayerCar(Vector3 orgPos)
        {
            //生成玩家控制的车
            //var entity = EntityManager.GetInstance().GenEntityGameObject(24, EntityFuncType.DrivedCar) as EntityDrivedCar;
            var entity = EntityManager.GetInstance().GenEntityGameObject(3, EntityFuncType.DrivedCar) as EntityDrivedCar;
            entity.position = orgPos;
            entity.LookAt(orgPos + GlobalDataManager.GetInstance().SceneForward);
            entity.moveSpeed = 10f;

            EntityManager.GetInstance().AddToEntityMovables(entity);

            //加碰撞
            if (null == entity.mainGameObject.GetComponent<CheckCollider>())
            {
                entity.mainGameObject.AddComponent<CheckCollider>();
            }

            //动画
            //if (entity.anim == null)
            //{
            //    entity.anim = new SimpleAnimation();
            //}
            //entity.anim.Init(entity.mainGameObject);

            //停车控制(规划直角路径)
            if (entity.ctrRA == null) {
                entity.ctrRA = new ControlledRightAngles();
            }
            entity.ctrRA.Init(entity, entity.moveSpeed, Vector3.forward, 1f);

            entity.Active();

            entity.mainGameObject.name = string.Format("PlayerCar[{0}][{1}]", entity.entityID, entity.mainGameObject.GetInstanceID());

            //Debug.LogErrorFormat("PlayerCar[{0}][{1}]", entity.entityID, entity.mainGameObject.GetInstanceID());

            player = entity;
        }

        //void GenNPCCar(float p)
        //{
        //    for (int i = 0; i < segmentRow; i++)
        //    {
        //        for (int j = 0; j < segmentCol; j++)
        //        {
        //            if (Random.Range(0.0F, 1.0F) < p)
        //            {
        //                //正常碰撞
        //                var NPCCar = ResourceManager.GetInstance().LoadGameObject("ResourcesDepend-box/Car_T_02/Car_T_02");
        //                //var NPCCar = ResourceManager.GetInstance().LoadGameObject("ResourcesDepend-box/Car_T_03/Car_T_03");
        //                //var NPCCar = LoadRandomGameObject(ResType.Car);
        //                NPCCar.transform.position = oneSegmentFullPos[i, j] + Vector3.forward * numNPCCarPosSegment * segmentRow * cellHeight;
        //                NPCCar.gameObject.name = string.Format("NPCCar {0}-[{1},{2}]", numNPCCarPosSegment, i, j);
        //            }
        //        }
        //    }
        //    ++numNPCCarPosSegment;
        //}

        void GenNPCCarNew(float p, List<GameObject> NPCCarList)
        {
            for (int i = 0; i < segmentRow; i++)
            {
                for (int j = 0; j < segmentCol; j++)
                {
                    if (Random.Range(0.0F, 1.0F) <= p)
                    {
                        //正常碰撞
                        //var NPCCar = ResourceManager.GetInstance().LoadGameObject("ResourcesDepend-box/Car_T_02/Car_T_02");
                        //var NPCCar = ResourceManager.GetInstance().LoadGameObject("ResourcesDepend-box/Car_T_03/Car_T_03");
                        var NPCCar = LoadRandomGameObject(ResType.Car);
                        NPCCar.transform.position = oneSegmentFullPos[i, j] + Vector3.forward * numNPCCarPosSegment * segmentRow * cellHeight;
                        NPCCarList.Add(NPCCar);
                        //NPCCar.gameObject.name = string.Format("NPCCar {0}-[{1},{2}]", numNPCCarPosSegment, i, j);
                    }
#if UNITY_EDITOR
                    else
                    {
                        if (p >= 1)
                        {
                            string e = "Not Gen NPC Car";
                            throw new System.Exception(e);
                        }
                        
                    }
#endif
                }
            }
            ++numNPCCarPosSegment;
        }

        void GenSegmentFullPos()
        {
            for (int i = 0; i < segmentRow; i++)
            {
                for (int j = 0; j < segmentCol; j++)
                {
                    oneSegmentFullPos[i, j] = Vector3.left * cellWidth/2 +
                        Vector3.forward * i * cellHeight +
                        Vector3.right * j * cellWidth;
#if UNITY_EDITOR
                    var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    go.transform.position = oneSegmentFullPos[i, j] + new Vector3(0, 8, 0);
                    go.name = string.Format("npccarpos[{0},{1}]", i, j);
#endif
                }
            }
        }

        protected void OnTriggerLoadRoad(Message msg)
        {
            SwitchRoad();
            //GenNPCCar(1f);
            SwitchNPCCar(1);
        }

        protected void OnStageContinue(Message msg)
        {
            Continue();
        }

        protected void OnParkingSucess(Message msg)
        {
            Continue();
        }

        protected GameObject LoadRoadNew(string strPath)
        {

            var road = ResourceManager.GetInstance().LoadGameObject(strPath);
            road.name = string.Format("road[{0}]", numLoadedRoad);

            road.transform.position += Vector3.forward * numLoadedRoad * Road_Len;
            ++numLoadedRoad;

            return road;
        }

        protected void SwitchRoad()
        {
            Debug.LogError("SwithRoad" + numLoadedRoad);

            ++numLoadedRoad;
            roadInScreen.transform.position += Vector3.forward * Road_Len * 2;
            var tmp = roadInScreen;
            roadInScreen = roadOutScreen;
            roadOutScreen = tmp;
        }

        protected void SwitchNPCCar(float p)
        {
            Debug.LogError("SwitchNPCCar" + numNPCCarPosSegment);

            for (int i = 0; i < NPCCarInScreen.Count; i++)
            {
                RecoveryToPool(NPCCarInScreen[i]);
            }
            NPCCarInScreen.Clear();
            //++numNPCCarPosSegment;
            GenNPCCarNew(p, NPCCarInScreen);

            var tmp = NPCCarInScreen;
            NPCCarInScreen = NPCCarOutScreen;
            NPCCarOutScreen = tmp;
        }

        GameObject LoadRandomGameObject(ResType resType)
        {
            List<ResourceKeyCell> cellList = GlobalDataManager.GetInstance().logicTableResource.GetResListByResType((int)resType);
            if (cellList == null || cellList.Count <= 0)
            {
                string e = string.Format("cellList 异常 {0}", resType);
                throw new System.Exception(e);
            }

            int idx = UnityEngine.Random.Range(0, cellList.Count);
            var cell = cellList[idx];
            var pool = PoolManager.GetInstance().GetGameObjectPool(cell.key);
            var go = pool.New();
            go.name = cell.key.ToString();
            return go;
        }

        void RecoveryToPool(GameObject go)
        {
            var pool = PoolManager.GetInstance().GetGameObjectPool(int.Parse(go.name));
#if UNITY_EDITOR
            if (pool == null)
            {
                string e = string.Format("没有找到pool type = {0}", go.name);
                throw new System.Exception(e);
            }
#endif
            if (pool != null)
            {
                go.transform.position = Const.Invisible_Postion;
                pool.Delete(go);
            }
        }

    }

}
