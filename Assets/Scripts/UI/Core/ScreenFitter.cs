using UnityEngine;
using UnityEngine.UI;

namespace AChen.UI
{
/// <summary>
/// 屏幕适配器
/// </summary>
public class ScreenFitter : MonoBehaviour
{
   private int height = 960;
   public CanvasScaler[] m_CanvasScaler;
   public Camera m_Camera;

   private void Awake()
   {
     float ratio = (float)Screen.width/Screen.height;
     DoFit(ratio);
   }

   private void DoFit(float ratio) {
      foreach (var cs in m_CanvasScaler)
      {
        cs.referenceResolution = new Vector2(height*ratio, height);
      }
      if(m_Camera != null)
      {
        m_Camera.orthographicSize = height/(2f * 100);
      }
   }
}
}
