//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CarRadar : MonoBehaviour
//{
//    [Header("设定减速到的值")]
//    public float vSpeed = 0;


//    Rigidbody m_RigidBody = null;
//    Collider m_Collider = null;
//    CarControl m_Car = null;

//    Transform m_TransCollider = null;
//    float vSpeedHold = 0f;

//    float m_fCurTime = 0.2f;

//    float m_fTotalCheckStopTime = 0.2f;

//    float m_fCDKnockWall = 2f;
//    float m_fCDKnockCar = 2f;
//    float m_fKnockWallSpeed = 5f;
//    float m_fKnockAISpeed = 20f;

//    int m_nTryChangeFwdTimes = 0;
//    void ResetKnockWallCD()
//    {
//        m_fCDKnockWall = 2f;
//    }
//    void ResetKnockCarCD()
//    {
//        m_fCDKnockCar = 2f;
//    }

//    void SimulateCD(float fDeltaTime)
//    {
//        if(m_fCDKnockCar > 0)
//        {
//            m_fCDKnockCar -= fDeltaTime;
//        }
//        if (m_fCDKnockWall > 0)
//        {
//            m_fCDKnockWall -= fDeltaTime;
//        }
//    }

//    void SimulateCheckStop(float fDeltaTime)
//    {
//        if(m_fCurTime >= m_fTotalCheckStopTime)
//        {
//            return;
//        }
//        m_fCurTime += fDeltaTime;
//        if(m_fCurTime >= m_fTotalCheckStopTime)
//        {
//            m_fCurTime = 0f;
//            m_Car.TryChangeFwd(++m_nTryChangeFwdTimes);
//        }
//    }

//    bool CheckCanDoShake(Collider other)
//    {
//        if(other.gameObject.layer == GameDefine.cfg_DefaultLayer)
//        {
//            if(m_Car.CurMoveSpeed >= m_fKnockWallSpeed && m_fCDKnockWall <= 0)
//            {
//                ResetKnockWallCD();
//                return true;
//            }
//        }
//        else if(other.gameObject.layer == GameDefine.cfg_CarLayer)
//        {
//            if (m_Car.CurMoveSpeed >= m_fKnockAISpeed && m_fCDKnockCar <= 0)
//            {
//                ResetKnockCarCD();
//                return true;
//            }
//        }
//        return false;
//    }

//    private void Start()
//    {
//        InitParams();
//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if(m_Car == null)
//        {
//            return;
//        }
//        if (m_Car != null && !m_Car.isAI)
//        {
//            if (CheckCanDoShake(other))
//            {
//                CameraMgr.Inst.DoShake();
//            }
//        }
//        if (isCanDoSubSpeed(other))
//        {
//            m_TransCollider = other.transform;
//            vSpeedHold = m_Car.CurDesc.speed;
//            m_Car.SetMaxMoveSpeed(vSpeed, 0, true,true);
//            EnableCheckStop(true);     
//        }
//    }
//    private void OnTriggerExit(Collider other)
//    {
//        if (m_Car == null)
//        {
//            return;
//        }
//        if (m_TransCollider == other.transform)
//        {
//            m_Car.SetMaxMoveSpeed(vSpeedHold, m_Car.CurDesc.acc_speed, false);
//            m_TransCollider = null;
//            EnableCheckStop(false);
//        }
//    }
//    bool isCanDoSubSpeed(Collider other)
//    {
//        if(other.gameObject.layer != GameDefine.cfg_CarLayer
//            && other.gameObject.layer != GameDefine.cfg_BuffLayer
//            && other.gameObject.layer != GameDefine.cfg_UICarLayer)
//        {

//            return !m_Car.isFinish;
//        }
//        return false;
//    }
//    void InitParams()
//    {
//        m_RigidBody = GetComponent<Rigidbody>();
//        m_RigidBody.isKinematic = true;
//        m_RigidBody.useGravity = false;
//        m_RigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
//        m_Collider = GetComponent<Collider>();
//        m_Collider.isTrigger = true;
//        m_Car = transform.parent.GetComponentInChildren<CarControl>();
//        m_fCDKnockWall = Excel_Conf.GetConfigFloatValue(Excel_Conf.CDKnockWall);
//        m_fCDKnockCar = Excel_Conf.GetConfigFloatValue(Excel_Conf.CDKnockCar);
//        m_fKnockWallSpeed = Excel_Conf.GetConfigFloatValue(Excel_Conf.KnockWallSpeed);
//        m_fKnockAISpeed = Excel_Conf.GetConfigFloatValue(Excel_Conf.KnockAISpeed);
//    }

//    private void Update()
//    {
//        if(m_Car != null)
//        {
//            float fDeltaTime = Time.deltaTime;
//            if (m_Car.isAI)
//            {
//                SimulateCheckStop(fDeltaTime);
//            }
//            else
//            {
//                SimulateCD(fDeltaTime);
//            }
//        }
//    }
//    void EnableCheckStop(bool enable)
//    {
//        m_fCurTime = enable ? 0 : m_fTotalCheckStopTime;
//        if(enable)
//        {
//            m_nTryChangeFwdTimes = 0;
//        }
//    }


//}
