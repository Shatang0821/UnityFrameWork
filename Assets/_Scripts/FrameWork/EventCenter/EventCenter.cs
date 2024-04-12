using System;
using System.Collections.Generic;
using System.Linq;
using FrameWork.Utils;
using UnityEngine;

namespace FrameWork.EventCenter
{
    /// <summary>
    /// �C�x���g�������S
    /// </summary>
    public class EventCenter
    {
        //�f�t�H���g�C�x���g
        private static Dictionary<EventKey, Delegate> m_EventDictionary = new Dictionary<EventKey, Delegate>();
#region Add

        /// <summary>
        /// �C�x���g�o�^�`�F�b�N
        /// </summary>
        /// <param name="eventKey"></param>
        /// <param name="callBack"></param>
        /// <exception cref="Exception"></exception>
        private static void OnListenerAdding(EventKey eventKey, Delegate callBack)
        {
            //�C�x���g�o�^����
            if (!m_EventDictionary.ContainsKey(eventKey))
            {
                m_EventDictionary.Add(eventKey, null);
            }

            Delegate d = m_EventDictionary[eventKey];
            if (d != null && d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("�C�x���g{0}�ɈقȂ�f���P�[�g��ǉ����悤�Ƃ���,���݂̃C�x���g�ɑΉ�����f���P�[�g��{1},�o�^�������f���P�[�g��{2}",
                    eventKey, d.GetType(), callBack.GetType()));
            }
        }

        /// <summary>
        /// �����Ȃ��f���P�[�g��o�^
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="callBack">�f���P�[�g</param>
        public static void AddListener(EventKey eventKey, CallBack callBack)
        {
            OnListenerAdding(eventKey, callBack);

            m_EventDictionary[eventKey] = Delegate.Combine(m_EventDictionary[eventKey], callBack);
        }

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="callBack">�f���P�[�g</param>
        /// <typeparam name="T">����</typeparam>
        public static void AddListener<T>(EventKey eventKey, CallBack<T> callBack)
        {
            OnListenerAdding(eventKey, callBack);

            m_EventDictionary[eventKey] = Delegate.Combine(m_EventDictionary[eventKey], callBack);
        }
        
        
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="callBack">�f���P�[�g</param>
        /// <typeparam name="T1">����</typeparam>
        /// <typeparam name="T2">����</typeparam>
        public static void AddListener<T1,T2>(EventKey eventKey, CallBack<T1,T2> callBack)
        {
            OnListenerAdding(eventKey, callBack);

            m_EventDictionary[eventKey] = Delegate.Combine(m_EventDictionary[eventKey], callBack);
        }

#endregion

#region Remove
        /// <summary>
        /// �C�x���g�̉����`�F�b�N
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="callBack">�C�x���g</param>
        /// <exception cref="Exception"></exception>
        private static void OnListenerRemoving(EventKey eventKey, Delegate callBack)
        {
            //�C�x���g�o�^����
            if (m_EventDictionary.ContainsKey(eventKey))
            {
                Delegate d = m_EventDictionary[eventKey];
                if (d == null)
                {
                    throw new Exception(string.Format("�f���P�[�g���������s,�C�x���g{0}�ɂ͑Ή�����f���P�[�g{1}�͑��݂��Ă��Ȃ�", eventKey,
                        callBack.GetType()));
                }
                else if (d.GetType() != callBack.GetType())
                {
                    throw new Exception(string.Format(
                        "�f���P�[�g���������s,�C�x���g{0}�ɈႤ�^�̃f���P�[�g���������悤�Ƃ���,���݂̃C�x���g�ɑΉ�����f���P�[�g��{1},�����������f���P�[�g��{2}", eventKey,
                        d.GetType(), callBack.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("�f���P�[�g���������s,�C�x���g�L�[{0}�͑��݂��Ă��Ȃ�", eventKey));
            }
        }

        /// <summary>
        /// �C�x���g�̉�������
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        private static void OnListenerRemoved(EventKey eventKey)
        {
            if (m_EventDictionary[eventKey] == null)
            {
                m_EventDictionary.Remove(eventKey);
            }
        }

        /// <summary>
        /// �f���P�[�g������
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="callBack">�f���P�[�g</param>
        public static void RemoveListener(EventKey eventKey, CallBack callBack)
        {
            OnListenerRemoving(eventKey, callBack);

            m_EventDictionary[eventKey] = (CallBack)m_EventDictionary[eventKey] - callBack;

            OnListenerRemoved(eventKey);
        }

        /// <summary>
        /// �����P����
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="callBack">�f���P�[�g</param>
        /// <typeparam name="T">����</typeparam>
        public static void RemoveListener<T>(EventKey eventKey, CallBack<T> callBack)
        {
            OnListenerRemoving(eventKey, callBack);

            m_EventDictionary[eventKey] = (CallBack<T>)m_EventDictionary[eventKey] - callBack;

            OnListenerRemoved(eventKey);
        }
        
        /// <summary>
        /// �����Q����
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="callBack">�f���P�[�g</param>
        /// <typeparam name="T1">����</typeparam>
        /// <typeparam name="T2">����</typeparam>
        public static void RemoveListener<T1,T2>(EventKey eventKey, CallBack<T1,T2> callBack)
        {
            OnListenerRemoving(eventKey, callBack);

            m_EventDictionary[eventKey] = (CallBack<T1,T2>)m_EventDictionary[eventKey] - callBack;

            OnListenerRemoved(eventKey);
        }
#endregion

#region Trigger

        /// <summary>
        /// �C�x���g�����s����
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        public static void TriggerEvent(EventKey eventKey)
        {
            if (m_EventDictionary.TryGetValue(eventKey, out Delegate d))
            {
                if (d is CallBack callBack)
                {
                    callBack();
                }
                else
                {
                    throw new Exception(string.Format("�C�x���g�����s���s:�C�x���g{0}�͈Ⴄ�^�̃f���P�[�g�������Ă���", eventKey));
                }
            }
        }

        /// <summary>
        /// ������̃C�x���g�����s
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="arg">����</param>
        /// <typeparam name="T">�^</typeparam>
        /// <exception cref="Exception"></exception>
        public static void TriggerEvent<T>(EventKey eventKey, T arg)
        {
            if (m_EventDictionary.TryGetValue(eventKey, out Delegate d))
            {
                if (d is CallBack<T> callBack)
                {
                    callBack(arg);
                }
                else
                {
                    throw new Exception(string.Format("�C�x���g�����s���s:�C�x���g{0}�͈Ⴄ�^�̃f���P�[�g�������Ă���", eventKey));
                }
            }
        }
        
        /// <summary>
        /// ������̃g���K�[
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <typeparam name="T1">�����^1</typeparam>
        /// <typeparam name="T2">�����^2</typeparam>
        public static void TriggerEvent<T1,T2>(EventKey eventKey, T1 arg1,T2 arg2)
        {
            if (m_EventDictionary.TryGetValue(eventKey, out Delegate d))
            {
                if (d is CallBack<T1,T2> callBack)
                {
                    callBack(arg1,arg2);
                }
                else
                {
                    throw new Exception(string.Format("�C�x���g�����s���s:�C�x���g{0}�͈Ⴄ�^�̃f���P�[�g�������Ă���", eventKey));
                }
            }
        }
#endregion
    }
}