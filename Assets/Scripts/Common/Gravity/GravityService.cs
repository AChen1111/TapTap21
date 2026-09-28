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
        [Tooltip("���ú󣬽���������С�������ı�ʱ����")]
        private bool IgnoreIfUnchanged = true;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                ALog.LogWarning($"�ظ������������ٵ�ǰ���� {name}", name);
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        [Button]
        /// <summary>��������ת</summary>
        public void Flip()
        {
            SetGravity((EGravityDirection)(-(int)Direction), Strength);
        }

        [Button]
        /// <summary>��������</summary>
        public void ResetGravity()
        {
            SetGravity(EGravityDirection.Down, 1f);
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="gDirection">��������</param>
        public void SetGravity(EGravityDirection gDirection)
        {
            SetGravity(gDirection, Strength);
        }

        /// <summary>
        /// ����������С
        /// </summary>
        /// <param name="gStrength">������С</param>
        public void SetGravity(float gStrength)
        {
            SetGravity(Direction, gStrength);
        }

        [Button]
        /// <summary>
        /// �����������򼰴�С
        /// </summary>
        /// <param name="gDirection">��������</param>
        /// <param name="gStrength">������С</param>
        public void SetGravity(EGravityDirection gDirection, float gStrength)
        {
            if (gDirection != EGravityDirection.Down && gDirection != EGravityDirection.Up)
            {
                gDirection = EGravityDirection.Down;
                ALog.LogWarning($"Gravity Direction ��Ч, ������Ϊ {gDirection}", name);
            }

            if (gStrength < 0)
            {
                gStrength = 1f;
                ALog.LogWarning($"Gravity Strength ����С�� 0��������Ϊ {gStrength}", name);
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