using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 高优先级 Window（默认弹窗）所在层，由 WindowUILayer 控制。
/// </summary>
public class WindowParaLayer : MonoBehaviour {
    [SerializeField] 
    private GameObject darkenBgObject = null;

    private List<GameObject> containedScreens = new List<GameObject>();
    
    public void AddScreen(Transform screenRectTransform) {
        screenRectTransform.SetParent(transform, false);
        containedScreens.Add(screenRectTransform.gameObject);
    }

    public void RefreshDarken() {
        for (int i = 0; i < containedScreens.Count; i++) {
            if (containedScreens[i] != null) {
                if (containedScreens[i].activeSelf) {
                    darkenBgObject.SetActive(true);
                    return;
                }
            }
        }

        darkenBgObject.SetActive(false);
    }

    public void DarkenBG() {
        if (darkenBgObject == null) {
            return;
        }

        darkenBgObject.SetActive(true);
        darkenBgObject.transform.SetAsLastSibling();
    }

    public void HideDarken() {
        if (darkenBgObject == null) {
            return;
        }

        darkenBgObject.SetActive(false);
    }
}
