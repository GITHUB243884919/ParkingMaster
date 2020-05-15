using HedgehogTeam.EasyTouch;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UnityEngine;

namespace Game
{

    public class PlayerCarFinger : MonoBehaviour
    {
        // Subscribe to events
        void OnEnable()
        {
            EasyTouch.On_TouchStart += On_TouchStart;
        }
        // Unsubscribe
        void OnDisable()
        {
            EasyTouch.On_TouchStart -= On_TouchStart;
        }
        // Unsubscribe
        void OnDestroy()
        {
            EasyTouch.On_TouchStart -= On_TouchStart;
        }
        // Touch start event
        public void On_TouchStart(Gesture gesture)
        {
            MessageManager.GetInstance().Send((int)UFrameBuildinMessage.TurnRightAngle);
        }
    }

}
