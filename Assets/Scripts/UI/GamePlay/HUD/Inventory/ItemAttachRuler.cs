using UnityEngine;

[CreateAssetMenu(fileName = "ItemAttachRuler", menuName = "Scriptable Objects/ItemAttachRuler")]
public class ItemAttachRuler : ScriptableObject
{
    public bool enableAttach=true;
    public float OffsetX=>_offsetX;
    public float OffsetY=>_offsetY;
    public float Width=>_width;
    public float Height=>_height;

    [SerializeField]private float _offsetX;
    [SerializeField]private float _offsetY;
    [SerializeField]private float _width;
    [SerializeField]private float _height;
}
