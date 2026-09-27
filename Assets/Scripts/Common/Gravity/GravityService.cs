using UnityEngine;
using AChen.Events;
using Sirenix.OdinInspector;
using UnityEngine.Rendering;
using AChen.Log;

namespace GamePlay.Gravity
{
    public class GravityService : MonoBehaviour
    {
        public static GravityService Instance { get; private set; }

        public EGravityDirection Direction { get; private set; } = EGravityDirection.Down;
        public float Strength { get; private set; } = 1f;
        [SerializeField]
        [Tooltip("启用后，仅在重力大小或方向发生改变时激活")]
        private bool IgnoreIfUnchanged = true;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                ALog.LogWarning($"重复单例，已销毁当前对象 {name}", name);
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        [Button]
        /// <summary>将重力翻转</summary>
        public void Flip()
        {
            SetGravity((EGravityDirection)(-(int)Direction), Strength);
        }

        [Button]
        /// <summary>重置重力</summary>
        public void ResetGravity()
        {
            SetGravity(EGravityDirection.Down, 1f);
        }

        /// <summary>
        /// 设置重力方向
        /// </summary>
        /// <param name="gDirection">重力方向</param>
        public void SetGravity(EGravityDirection gDirection)
        {
            SetGravity(gDirection, Strength);
        }

        /// <summary>
        /// 设置重力大小
        /// </summary>
        /// <param name="gStrength">重力大小</param>
        public void SetGravity(float gStrength)
        {
            SetGravity(Direction, gStrength);
        }

        [Button]
        /// <summary>
        /// 设置重力方向及大小
        /// </summary>
        /// <param name="gDirection">重力方向</param>
        /// <param name="gStrength">重力大小</param>
        public void SetGravity(EGravityDirection gDirection, float gStrength)
        {
            if (gDirection != EGravityDirection.Down && gDirection != EGravityDirection.Up)
            {
                gDirection = EGravityDirection.Down;
                ALog.LogWarning($"Gravity Direction 无效, 已重置为 {gDirection}", name);
            }

            if (gStrength < 0)
            {
                gStrength = 1f;
                ALog.LogWarning($"Gravity Strength 不可小于 0，已重置为 {gStrength}", name);
            }

            if (IgnoreIfUnchanged &&
                Direction == gDirection &&
                Strength == gStrength)
                return;


            if ((int)gDirection == -(int)Direction)
            {
                Direction = gDirection;
                EventCenter.Dispatch<EGravityDirection>(GameEvent.GravityFlipped, Direction);
            }

            Strength = gStrength;
            EventCenter.Dispatch<float, EGravityDirection>(GameEvent.GravityChanged, (int)gDirection * gStrength, Direction);
        }
    }
}   