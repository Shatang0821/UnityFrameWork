using FrameWork.Manager;
using FrameWork.Utils;
using UnityEngine;

namespace FrameWork.Factories
{
    public class ManagerFactory : Singleton<ManagerFactory>
    {
        /// <summary>
        /// マネージャーの生成
        /// </summary>
        /// <param name="parent">親の指定</param>
        /// <typeparam name="T">マネージャークラス</typeparam>
        /// <returns></returns>
        public T CreateManager<T>(Transform parent) where T : Component, IManager
        {
            GameObject gameObject = new GameObject(typeof(T).Name);
            gameObject.transform.parent = parent;
            var t = gameObject.AddComponent<T>();
            t.Init();
            return t;
        }
    }
}