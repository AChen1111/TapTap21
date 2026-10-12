using UnityEngine;

namespace FXSystem
{
    /// <summary>
    /// 挂到物体上即可试播，方便验证 Catalog 是否接通。
    /// </summary>
    public class FXPlayOnStart : MonoBehaviour
    {
        public string FxId;
        public FXDefinition Definition;
        public bool Attached;
        public bool PlayAsUI;

        void Start()
        {
            if (PlayAsUI)
            {
                var rect = transform as RectTransform;
                if (Definition != null)
                {
                    if (Attached && rect != null)
                        FX.PlayUIAttached(Definition, rect);
                    else if (rect != null)
                        FX.PlayUI(Definition, rect);
                    else
                        FX.PlayUIAtWorld(Definition, transform.position);
                    return;
                }

                if (string.IsNullOrEmpty(FxId))
                    return;

                if (Attached && rect != null)
                    FX.PlayUIAttached(FxId, rect);
                else if (rect != null)
                    FX.PlayUI(FxId, rect);
                else
                    FX.PlayUIAtWorld(FxId, transform.position);
                return;
            }
            if (Definition != null)
            {
                if (Attached)
                    FX.PlayAttached(Definition, transform);
                else
                    FX.Play(Definition, transform.position);
                return;
            }

            if (string.IsNullOrEmpty(FxId))
                return;

            if (Attached)
                FX.PlayAttached(FxId, transform);
            else
                FX.Play(FxId, transform.position);
        }
    }
}
