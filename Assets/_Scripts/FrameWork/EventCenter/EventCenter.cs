using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.EventCenter
{
    /// <summary>
    /// �C�x���g�������S
    /// </summary>
    public class EventCenter
    {
        //�f�t�H���g�C�x���g
        private static Dictionary<EventKey, Action> eventNoParamDictionary = new Dictionary<EventKey, Action>();

        //���������C�x���g
        private static Dictionary<EventKey, Action<object>> eventWithParamDictionary =
            new Dictionary<EventKey, Action<object>>();

        // �߂�l�̂���C�x���g
        private static Dictionary<EventKey, Func<object, object>> eventWithReturnDictionary =
            new Dictionary<EventKey, Func<object, object>>();

        /// <summary>
        /// �C�x���g���T�u�X�N���C�u
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="listener">�C�x���g</param>
        public static void Subscribe(EventKey eventKey, Action listener)
        {
            if (eventNoParamDictionary.TryGetValue(eventKey, out var thisEvent))
            {
                // ���ɓ������X�i�[���o�^����Ă���ꍇ�͒ǉ����Ȃ�
                if (thisEvent != null && !thisEvent.GetInvocationList().Contains(listener))
                {
                    eventNoParamDictionary[eventKey] += listener;
                }
            }
            else
            {
                eventNoParamDictionary.Add(eventKey, listener);
            }
        }

        /// <summary>
        /// ���������C�x���g���T�u�X�N���C�u
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="listener">�C�x���g</param>
        public static void Subscribe(EventKey eventKey, Action<object> listener)
        {
            if (eventWithParamDictionary.TryGetValue(eventKey, out var thisEvent))
            {
                thisEvent += listener;
                eventWithParamDictionary[eventKey] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                eventWithParamDictionary.Add(eventKey, thisEvent);
            }
        }

        /// <summary>
        /// �߂�l�̂���C�x���g���T�u�X�N���C�u
        /// ��̃��X�i�[�����L��
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="listener">�C�x���g</param>
        public static void Subscribe(EventKey eventKey, Func<object, object> listener)
        {
            if (!eventWithReturnDictionary.ContainsKey(eventKey))
            {
                eventWithReturnDictionary[eventKey] = listener;
            }
            else
            {
                Debug.LogWarning(eventKey.ToString() + "�������̃C�x���g���T�u�X�N���C�u���Ă���");
                // �����̃��X�i�[�ƐV�������X�i�[��A�����鏈���i�K�v�ɉ����Ď����j
                // ��: �Ō�ɒǉ����ꂽ���X�i�[�݂̂��L���ɂȂ�
                eventWithReturnDictionary[eventKey] = listener;
            }
        }

        /// <summary>
        /// �T�u�X�N���C�u������
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="listener">�C�x���g</param>
        public static void Unsubscribe(EventKey eventKey, Action listener)
        {
            if (eventNoParamDictionary.TryGetValue(eventKey, out var thisEvent))
            {
                thisEvent -= listener;
                eventNoParamDictionary[eventKey] = thisEvent;
            }
        }

        /// <summary>
        /// ���������T�u�X�N���C�u������
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="listener">�C�x���g</param>
        public static void Unsubscribe(EventKey eventKey, Action<object> listener)
        {
            if (eventWithParamDictionary.TryGetValue(eventKey, out var thisEvent))
            {
                thisEvent -= listener;
                eventWithParamDictionary[eventKey] = thisEvent;
            }
        }

        /// <summary>
        /// �߂�l�̂���C�x���g������
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="listener">�C�x���g</param>
        public static void Unsubscribe(EventKey eventKey, Func<object, object> listener)
        {
            if (eventWithReturnDictionary.ContainsKey(eventKey))
            {
                eventWithReturnDictionary.Remove(eventKey);
            }
        }

        /// <summary>
        /// �C�x���g�����s����
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        public static void TriggerEvent(EventKey eventKey)
        {
            if (eventNoParamDictionary.TryGetValue(eventKey, out var thisEvent))
            {
                thisEvent?.Invoke();
            }
        }

        /// <summary>
        /// ���������C�x���g�����s
        /// </summary>
        /// <param name="eventKey">�C�x���g�L�[</param>
        /// <param name="eventParam">����</param>
        public static void TriggerEvent(EventKey eventKey, object eventParam)
        {
            if (eventWithParamDictionary.TryGetValue(eventKey, out var thisEvent))
            {
                thisEvent?.Invoke(eventParam);
            }
        }

        // �߂�l�̂���C�x���g���g���K�[
        public static object TriggerEventHasValue(EventKey eventKey, object eventParam)
        {
            if (eventWithReturnDictionary.TryGetValue(eventKey, out var thisEvent))
            {
                return thisEvent?.Invoke(eventParam);
            }
            return null;
        }
    }
}


