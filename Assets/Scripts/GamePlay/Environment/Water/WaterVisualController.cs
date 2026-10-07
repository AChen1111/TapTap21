using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class WaterVisualController : MonoBehaviour
{
    static readonly int WaveSpeedId = Shader.PropertyToID("_WaveSpeed");
    static readonly int WaveDensityId = Shader.PropertyToID("_WaveDensity");
    static readonly int WaveSizeId = Shader.PropertyToID("_WaveSize");
    static readonly int WaveTruncationId = Shader.PropertyToID("_WaveTruncation");
    static readonly int BodyColorId = Shader.PropertyToID("_BodyColor");
    static readonly int BodyTransparencyId = Shader.PropertyToID("_BodyTransparency");
    static readonly int LineColorId = Shader.PropertyToID("_LineColor");
    static readonly int LineLengthId = Shader.PropertyToID("_LineLength");
    static readonly int TextureId = Shader.PropertyToID("_Texture2D");
    static readonly int TextureTexelSizeId = Shader.PropertyToID("_Texture2D_TexelSize");
    static readonly int DistortionDirId = Shader.PropertyToID("_DistortionDir");
    static readonly int DistortionScaleId = Shader.PropertyToID("_DistortionScale");
    static readonly int DistortionStrengthId = Shader.PropertyToID("_DistortionStrength");
    static readonly int DistortionOffsetSpeedId = Shader.PropertyToID("_DistortionOffsetSpeed");

    [Header("Wave")]
    [SerializeField, Range(-50f, 50f)] float _waveSpeed = 1f;
    [SerializeField] float _waveDensity = 2f;
    [SerializeField, Range(0f, 2f)] float _waveSize = 0.06f;
    [SerializeField, Range(0f, 100f)] float _waveTruncation = 100f;

    [Header("Body")]
    [SerializeField] Color _bodyColor = new Color(0f, 0.5271822f, 1f, 1f);
    [SerializeField, Range(0f, 1f)] float _bodyTransparency = 0.5f;
    [SerializeField] Color _lineColor = Color.white;
    [SerializeField] float _lineLength = 0.005f;

    [Header("Distortion")]
    [SerializeField] Texture2D _texture2D;
    [SerializeField] Vector2 _distortionDir = new Vector2(0.05f, 0f);
    [SerializeField, Range(0f, 50f)] float _distortionScale = 23.3f;
    [SerializeField, Range(-0.05f, 0.05f)] float _distortionStrength = 0.0056f;
    [SerializeField, Range(0f, 10f)] float _distortionOffsetSpeed = 2.57f;

    MeshRenderer _renderer;
    MaterialPropertyBlock _block;
    bool _dirty = true;

    public float WaveSpeed { get => _waveSpeed; set => Set(ref _waveSpeed, value); }
    public float WaveDensity { get => _waveDensity; set => Set(ref _waveDensity, value); }
    public float WaveSize { get => _waveSize; set => Set(ref _waveSize, value); }
    public float WaveTruncation { get => _waveTruncation; set => Set(ref _waveTruncation, value); }
    public Color BodyColor { get => _bodyColor; set => Set(ref _bodyColor, value); }
    public float BodyTransparency { get => _bodyTransparency; set => Set(ref _bodyTransparency, value); }
    public Color LineColor { get => _lineColor; set => Set(ref _lineColor, value); }
    public float LineLength { get => _lineLength; set => Set(ref _lineLength, value); }
    public Texture2D Texture2D
    {
        get => _texture2D;
        set
        {
            if (_texture2D == value) return;
            _texture2D = value;
            _dirty = true;
        }
    }
    public Vector2 DistortionDir { get => _distortionDir; set => Set(ref _distortionDir, value); }
    public float DistortionScale { get => _distortionScale; set => Set(ref _distortionScale, value); }
    public float DistortionStrength { get => _distortionStrength; set => Set(ref _distortionStrength, value); }
    public float DistortionOffsetSpeed { get => _distortionOffsetSpeed; set => Set(ref _distortionOffsetSpeed, value); }

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _block = new MaterialPropertyBlock();
    }

    void OnEnable()
    {
        _dirty = true;
    }

    void OnValidate()
    {
        _dirty = true;
        Apply();
    }

    void LateUpdate()
    {
        if (!_dirty) return;
        Apply();
    }

    public void Apply()
    {
        if (_renderer == null)
            _renderer = GetComponent<MeshRenderer>();
        if (_renderer == null)
            return;
        if (_block == null)
            _block = new MaterialPropertyBlock();

        _block.Clear();
        _block.SetFloat(WaveSpeedId, _waveSpeed);
        _block.SetFloat(WaveDensityId, _waveDensity);
        _block.SetFloat(WaveSizeId, _waveSize);
        _block.SetFloat(WaveTruncationId, _waveTruncation);
        _block.SetColor(BodyColorId, _bodyColor);
        _block.SetFloat(BodyTransparencyId, _bodyTransparency);
        _block.SetColor(LineColorId, _lineColor);
        _block.SetFloat(LineLengthId, _lineLength);
        _block.SetVector(DistortionDirId, _distortionDir);
        _block.SetFloat(DistortionScaleId, _distortionScale);
        _block.SetFloat(DistortionStrengthId, _distortionStrength);
        _block.SetFloat(DistortionOffsetSpeedId, _distortionOffsetSpeed);
        if (_texture2D != null)
        {
            _block.SetTexture(TextureId, _texture2D);
            float width = _texture2D.width;
            float height = _texture2D.height;
            if (width > 0f && height > 0f)
            {
                _block.SetVector(TextureTexelSizeId, new Vector4(1f / width, 1f / height, width, height));
            }
        }

        _renderer.SetPropertyBlock(_block);
        _dirty = false;
    }

    void Set<T>(ref T field, T value)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        _dirty = true;
    }
}
