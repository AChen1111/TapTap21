using UnityEngine;

namespace FXSystem
{
    /// <summary>
    /// 单次播放的可选参数。未赋值的字段使用安全默认值。
    /// </summary>
    public struct FXPlayParams
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        public Transform Parent;
        public bool Follow;
        public bool WorldPositionStays;
        public bool IsUI;
        public bool UseScreenPosition;
        public Vector2 ScreenPosition;
        public Camera WorldCamera;
        public float UIScaleMultiplier;
        public int SortingOrderOffset;
        public Color Color;
        public bool UseColor;
        public float Speed;
        public bool FlipX;
        public FXCompleteCallback OnComplete;

        public static FXPlayParams Default => new FXPlayParams
        {
            Rotation = Quaternion.identity,
            Scale = Vector3.one,
            Speed = 1f,
            Color = Color.white
        };

        public static FXPlayParams At(Vector3 position)
        {
            var p = Default;
            p.Position = position;
            return p;
        }

        internal void Sanitize()
        {
            if (Rotation == default)
                Rotation = Quaternion.identity;
            if (Scale == default)
                Scale = Vector3.one;
            if (Speed <= 0f)
                Speed = 1f;
            if (UIScaleMultiplier <= 0f)
                UIScaleMultiplier = 1f;
        }
    }
}
