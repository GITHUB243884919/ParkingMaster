using Game.MessageCenter;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.BehaviourFloat;
using UFrame.EntityFloat;
using UFrame.MessageCenter;
using UnityEngine;

namespace Game
{
    public class EntityDrivedCar : EntityMovable
    {
        public SimpleAnimation anim;

        public ControlledRightAngles ctrRA;

        public static ObjectPool<EntityDrivedCar> pool = new ObjectPool<EntityDrivedCar>();

        public float moveDistance = 0;

        public bool isCarCollider = false;

        public override void Active()
        {
            base.Active();
            isCarCollider = false;
			ctrRA.isProcessingTurn = false;
			ctrRA.Run();
            MessageManager.GetInstance().Regist((int)GameMessageDefine.CarCollider, OnCarCollider);
            MessageManager.GetInstance().Regist((int)UFrameBuildinMessage.TurnLeftRightAngle, OnTurnLeft);
            MessageManager.GetInstance().Regist((int)UFrameBuildinMessage.TurnRightRightAngle, OnTurnRight);
        }

        protected void OnCarCollider(Message msg)
        {
            //Debug.LogError("OnCarCollider");
            isCarCollider = true;
            ctrRA.Stop();
        }

        protected void OnTurnLeft(Message msg)
        {
            this.ctrRA.TurnLeft();
        }

        protected void OnTurnRight(Message msg)
        {
            this.ctrRA.TurnRight();
        }

        public override void Deactive()
        {
            DebugFile.GetInstance().WriteKeyFile(entityID, "{0} Deactive {1} , pos {2}", entityID, mainGameObject.GetInstanceID(), Const.Invisible_Postion);
            DebugFile.GetInstance().WriteKeyFile(mainGameObject.GetInstanceID(), "{0} Deactive {1} pos {2}", mainGameObject.GetInstanceID(), entityID, Const.Invisible_Postion);

            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.CarCollider, OnCarCollider);
            MessageManager.GetInstance().UnRegist((int)UFrameBuildinMessage.TurnLeftRightAngle, OnTurnLeft);
            MessageManager.GetInstance().UnRegist((int)UFrameBuildinMessage.TurnRightRightAngle, OnTurnRight);

            ctrRA.Stop();
            //移动到看不见的地方
            this.position = Const.Invisible_Postion;
            
            base.Deactive();
        }

        public override void Tick(int deltaTimeMS)
        {
            if (!this.CouldActive())
            {
                return;
            }
            ctrRA.Tick(deltaTimeMS);

            TickAnim();
        }

        public override void OnDeathToPool()
        {
            this.Deactive();
            base.OnDeathToPool();
        }

        public override void OnRecovery()
        {
            this.Deactive();
            base.OnRecovery();
            //anim.Release();
            //anim = null;
        }

        protected void TickAnim()
        {
            //if (followPath.IsRunning() != anim.GetAutoPlay())
            //{
            //    anim.SetAutoPlay(followPath.IsRunning());
            //}
        }
    }
}

