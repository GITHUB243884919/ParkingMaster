/*******************************************************************
* FileName:     CcontroledRightAngles.cs
* Author:       Fan Zheng Yong
* Date:         2020-04-11
* Description:  
* other:    
********************************************************************/


using UnityEngine;
using UFrame.EntityFloat;
using UFrame.Logger;
using System.Collections.Generic;
using Game.Path.StraightLine;
using UFrame.MessageCenter;
using Game;
using Game.MessageCenter;

namespace UFrame.BehaviourFloat
{


    public class ControlledRightAngles : BehaviourBase
    {
        public bool isProcessingTurn = false;
        //public List<Vector3> pathPosList = new List<Vector3>();
        //public int nextPosIdx = 0;
        //public Vector3 nextPos = Vector3.zero;
        public bool isArrivedEnd = false;

        //protected Quaternion orgRotation;

        /// <summary>
        /// 执行转弯的点和目标点的偏移量
        /// 转弯实现是还没到目标点就要开始执行转向
        /// </summary>
        public float turnOffset = 1f;

        /// <summary>
        /// 转弯速度 角速度
        /// </summary>
        protected float turnSpeed;

        /// <summary>
        /// 控制点列表
        /// </summary>
        public List<RightAnglesControllNode> ctrList = new List<RightAnglesControllNode>();

        /// <summary>
        /// 路过的控制点索引
        /// </summary>
        public int idxCtr;

        /// <summary>
        /// 转弯CD
        /// </summary>
        protected IntCD turnCD = new IntCD(0);

        /// <summary>
        /// 转弯CD值
        /// </summary>
        protected int turnCDVal = 0;

        /// <summary>
        /// 弯道角度90
        /// </summary>
        protected float cornerAngle = Const.RightAngles;

        /// <summary>
        /// 是否是往前运动，如果是倒车这种就是false
        /// </summary>
        public bool isForward = true;

        List<Vector3> pathPosList = new List<Vector3>();

		Vector3 mileStone = Vector3.zero;

        void GenRightAnglePath(short turnSign, List<Vector3> pathList)
        {
            pathList.Clear();
            Vector3 p0 = ownerEntity.position;
            Vector3 p1 = ownerEntity.position + ownerEntity.forward * 2f;
            Vector3 p2 = p1 + Quaternion.Euler(0, UFrame.Const.RightAngles * turnSign, 0) * ownerEntity.forward * 3f;
            Vector3 p3 = p2 + ownerEntity.forward * 2f;
            pathList.Add(p0);
            pathList.Add(p1);
            pathList.Add(p2);
            pathList.Add(p3);
            GenDebugGo(p0, "p0");
            GenDebugGo(p1, "p1");
            GenDebugGo(p2, "p2");
            GenDebugGo(p3, "p3");

        }

        void GenControllNodeList(List<Vector3> pathPosList, List<RightAnglesControllNode> ctrList, float turnOrgOffset)
        {
            //List<RightAnglesControllNode> ctrList = new List<RightAnglesControllNode>();
            ctrList.Clear();
            int posLen = pathPosList.Count;
            if (posLen < 2)
            {
                return; //ctrList;
            }

            for (int i = 1; i < posLen - 1; i++)
            {
                //前点
                var forwardDir = (pathPosList[i + 1] - pathPosList[i]).normalized;
                var forwardPos = pathPosList[i] + forwardDir * turnOrgOffset;
#if UNITY_EDITOR
                var forwardGo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                forwardGo.transform.position = forwardPos + new Vector3(0, 8, 0);
                forwardGo.name = string.Format("ctr_forward_{0}", i);
#endif
                //后点
                var backDir = (pathPosList[i] - pathPosList[i - 1]).normalized;
                var backPos = pathPosList[i] - backDir * turnOrgOffset;
#if UNITY_EDITOR
                var backGo = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                backGo.transform.position = backPos + new Vector3(0, 8, 0);
                backGo.name = string.Format("ctr_back_{0}", i);
#endif
                //旋转原点
                var cross = Vector3.Cross(forwardDir, backDir);
                short turnSign = 1;
                if (cross.y > 0)
                {
                    turnSign = -1;
                }
                var turnOrgDir = Quaternion.Euler(0, UFrame.Const.RightAngles * turnSign, 0) * backDir;
                var turnOrgPos = backPos + turnOrgDir * turnOrgOffset;
#if UNITY_EDITOR
                var turnOrgGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                turnOrgGo.transform.position = turnOrgPos + new Vector3(0, 8, 0);
                turnOrgGo.name = string.Format("ctr_turnOrg{0}", i);
#endif
                var ctr = new RightAnglesControllNode();
                ctr.forwardPos = forwardPos;
                ctr.backPos = backPos;
                ctr.turnOrg = turnOrgPos;
                ctr.turnSign = turnSign;
                ctrList.Add(ctr);
                
            }

            return;//ctrList;
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        void GenDebugGo(Vector3 pos, string name)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            go.transform.position = pos + new Vector3(0, 8, 0);
            go.name = name;
        }

        public override void Tick(int deltaTimeMS)
        {
            if (!this.CouldRun())
            {
                return;
            }

            if (!this.isProcessingTurn)
            {
                float distanceDelta = this.speed * deltaTimeMS;
                ownerEntity.position += ownerEntity.forward * distanceDelta;
				if (IsArrivedTarget(mileStone)) 
				{
					mileStone += Vector3.forward * StageManager.Road_Len;
					MessageManager.GetInstance().Send((int)GameMessageDefine.TriggerLoadRoad);
				}
			}
            else
            {
                Follow(deltaTimeMS, this.pathPosList);
                TickTurn(deltaTimeMS);
            }

            //if (ownerEntity.position.z > 0 && ownerEntity.position.z >= 50)
            //{
            //    Stop();
            //}
        }

        public virtual void TickTurn(int deltaTimeMS)
        {
            if (!turnCD.IsRunning() || turnCD.IsFinish())
            {
                return;
            }

            turnCD.Tick(deltaTimeMS);
            var ctr = this.ctrList[idxCtr];

            int left = turnCD.org - Mathf.Max(0, turnCD.cd);

            int logicDelta = deltaTimeMS;
            if (turnCD.cd < 0)
            {
                logicDelta += turnCD.cd;
            }
            //旋转
            this.ownerEntity.Rotate(new Vector3(0, ctr.turnSign * this.turnSpeed * logicDelta, 0), Space.Self);
            //位移
            var ctrForward = ctr.backPos - ctr.turnOrg;
            var turnDir = Quaternion.Euler(0, ctr.turnSign * this.turnSpeed * left, 0) * ctrForward;
            this.ownerEntity.position = ctr.turnOrg + turnDir;

            if (turnCD.IsFinish())
            {
                turnCD.Stop();

                //去除tick旋转和位移的误差：位移到前点，看向下一个后点
                ownerEntity.position = ctr.forwardPos;
                idxCtr++;
                if (idxCtr < ctrList.Count)
                {
                    this.ownerEntity.LookAt(this.ctrList[idxCtr].backPos);
                    if (!isForward)
                    {
                        ownerEntity.Rotate(new Vector3(0, 180f, 0), Space.Self);
                    }
                }
            }
        }

        public virtual void Init(EntityMovable ownerEntity, float speed, Vector3 dir, float turnOffset)
        {
            this.ownerEntity = ownerEntity;
            this.speed = speed * 0.001f;
            this.turnOffset = turnOffset;
            ownerEntity.LookAt(ownerEntity.position + dir);
			idxCtr = 0;
			turnCD.Stop();
            this.turnSpeed = CalcTurnSpeed(this.speed);
            //因为知道角度是90度所以cd是旋转cd恒定的
            turnCDVal = Math_F.FloatToInt(cornerAngle / this.turnSpeed);
            turnCD.ResetOrg(turnCDVal);
            this.isForward = true;
			mileStone = Vector3.forward * StageManager.Road_Len;

		}

        /// <summary>
        /// 根据速度计算旋转角速度。
        /// 速度可以看成是以控制点为中心，半径为specialOffset的圆上的上两个点的距离。
        /// 这样可以根据余弦定理可以求角
        /// speed最好带入毫秒单位，否则Acos会出现NaN值
        /// 准确的说，应该确保2 * this.specialOffset * this.specialOffset > speed * speed
        /// 以毫秒单位的speed不能超过圆的直径，否则构不成三角形
        /// 也就是cos为[0, 1]
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        float CalcTurnSpeed(float speed)
        {
            //cos(A) = (b^b + c^c - a^a) / 2bc
            float cos = (2 * this.turnOffset * this.turnOffset - speed * speed) / (2 * this.turnOffset * this.turnOffset);
            float rad = Mathf.Acos(cos);
            return turnSpeed = Math_F.RadianToAngle(rad);
        }

        public override void Release()
        {
            //pathPosList = null;
            ctrList.Clear();
            ctrList = null;

            pathPosList.Clear();
            pathPosList = null;
            base.Release();
        }

        protected override bool IsPassed(Vector3 pos)
        {
            var localPos = this.ownerEntity.InverseTransformPoint(pos);
            if (isForward)
            {
                return localPos.z <= 0;
            }

            return localPos.z > 0;
        }

        protected virtual void Follow(int deltaTimeMS, List<Vector3> pathPosList)
        {
            //是否到后点
            //到达后点, 开始启动旋转，位移转按角速度求出的位置
            //未到达后点往后点继续前进
            if (turnCD.IsRunning() || !turnCD.IsFinish())
            {
                return;
            }

            //还没到最后一个转弯点
#if UNITY_EDITOR
            if (ctrList == null)
            {
                string e = string.Format("{0} 路径异常", this.ownerEntity.entityID);
                throw new System.Exception(e);
            }
#endif
            if (idxCtr < ctrList.Count)
            {
                var ctr = ctrList[idxCtr];
                if (!IsArrivedTarget(ctr.backPos))
                {
                    UnArrived(deltaTimeMS, ctr.backPos);
                    return;
                }

                //重新设置旋转CD
                turnCD.ResetOrg(turnCDVal);
                turnCD.Run();
                return;
            }

            //到这里已经转过最后一个弯了
            var lastPos = pathPosList[pathPosList.Count - 1];
            if (!IsArrivedTarget(lastPos))
            {
                UnArrived(deltaTimeMS, lastPos);
                return;
            }

            //走到这里已经到达路径最后一个点，走完全部path
            WhenArrivedEndPos();

            this.isArrivedEnd = true;

        }

        public virtual void WhenArrivedEndPos()
        {
            MessageArrivedEx.Send((int)UFrameBuildinMessage.ArrivedRightAngle, this);
            isProcessingTurn = false;

            this.Stop();

            //MessageManager.GetInstance().Send((int)GameMessageDefine.ParkingSucess);
        }

        public bool IsArrivedTarget(Vector3 target)
        {
            if (Math_F.Approximate3D(this.ownerEntity.position, target) || this.IsPassed(target))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 直接转，相当于LookAt基本不用。
        /// </summary>
        /// <param name="target"></param>
        protected void TurnTo(Vector3 target)
        {
            var dirWorld = target - this.ownerEntity.position;
            float angle = Math_F.TwoDirYAngle(this.ownerEntity.forward, dirWorld.normalized);
            if (Vector3.Cross(dirWorld, this.ownerEntity.forward).y >= 0)
            {
                this.ownerEntity.Rotate(new Vector3(0, -angle, 0), Space.Self);
                return;
            }

            this.ownerEntity.Rotate(new Vector3(0, angle, 0), Space.Self);
        }

        protected virtual void UnArrived(int deltaTimeMS, Vector3 target)
        {
            //向目的地走
            this.tickDir = Math_F.TwoPositionDir(this.ownerEntity.position, target);
            //活动移动量
            this.tickSpeed = this.speed * deltaTimeMS;
            this.tickMove = this.tickDir * this.tickSpeed;
            //新位置
            this.ownerEntity.position += this.tickMove;
        }

        public void TurnLeft()
        {
            isProcessingTurn = true;
            GenRightAnglePath(-1, pathPosList);
            GenControllNodeList(pathPosList, ctrList, this.turnOffset);
            turnCD.Run();
        }

        public void TurnRight()
        {
            isProcessingTurn = true;
            GenRightAnglePath(1, pathPosList);
            GenControllNodeList(pathPosList, ctrList, this.turnOffset);
            turnCD.Run();
        }
    }
}

