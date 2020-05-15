using Game.MessageCenter;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UnityEngine;

namespace Game
{
    public class CheckCollider : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            //Debug.LogError("OnCollisionEnter ");
            if (collision.collider.tag.Equals("GameCollider"))
            {
                if (collision.gameObject.transform.position != Const.Invisible_Postion)
                {
					//Debug.LogErrorFormat(string.Format("OnCollisionEnter {0}, {1}",
					//	collision.gameObject.name, collision.gameObject.transform.position));

					MessageManager.GetInstance().Send((int)GameMessageDefine.CarCollider);
                }
            }
        }
    }
}

