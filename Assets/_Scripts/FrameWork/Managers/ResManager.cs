using FrameWork.Utils;

namespace FrameWork.Manager
{
    public class ResManager : UnitySingleton<ResManager>
    {
        public override void Awake()
        {
            base.Awake();
        }

        public T GetAssetCache<T>(string name) where T : UnityEngine.Object
        {
//#if UNITY_EDITOR
            string path = "Assets/AssetsPackage/" + name;
            UnityEngine.Object target = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            return target as T;
//#endif
        }
    }
}
