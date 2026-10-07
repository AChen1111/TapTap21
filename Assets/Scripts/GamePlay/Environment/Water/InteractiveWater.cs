using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Rendering;
using System;




#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using TMPro.EditorUtilities;
using TMPro;
#endif

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
[RequireComponent(typeof(WaterTriggerHandler), typeof(BoxCollider2D), typeof(BuoyancyEffector2D))]
[RequireComponent(typeof(SortingGroup))]
public class InteractiveWater : MonoBehaviour
{
    [Header("Water Body")]
    public bool enableVertexPerUnit = false;
    public float vertexPerUnit = 2f;
    [Range(2, 500)] public int numsOfXVertices = 50;
    public float width = 10f;
    public float height = 10f;
    public Material material;
    private const int numsOfYVertices = 2;

    [Header("Simulaion")]
    [Header("Spring")]
    [SerializeField] private float _stiffness = 15.0f;
    [SerializeField] private float _resistance = 3.0f;
    //[SerializeField] private float _propagation = 20.0f;
    //[SerializeField, Range (1, 10)] private int _wavePropogationIterations = 8;
    private int _pointCount = 0;

    [Header("Wave")]
    [SerializeField] private float _waveSpeed = 5f;
    [SerializeField] private int _simulationSubsteps = 4;
    private float _vertexSpacing = 1f;

    [Header("Force")]
    public float ForceMultiplier = 20f;
    public float ForceMax = 300f;


    [Header("Gizmos Color")]
    public Color SelectBoxColor = Color.white;
    public Color WaterPointColor = Color.green;

    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Vector3[] _vertices;
    private int[] _topVerticesIndex;

    private EdgeCollider2D _edgeCollider;
    private BoxCollider2D _boxCollider;


    float[] _velocities;
    float[] _accelerations;
    float[] _heights;
    float[] _targetHeights;
    float[] _externalForces;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.enabled = true;
    }

    private void Start()
    {
        InitializeWater();
        UpdateCollider();
    }

    private void Reset()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _edgeCollider = GetComponent<EdgeCollider2D>();
        _edgeCollider.isTrigger = true;
    }

    private void OnDestroy()
    {
        if (_mesh != null)
        {
            Destroy(_mesh);
            _mesh = null;
        }
    }

    private void InitializeWater()
    {
        if (enableVertexPerUnit)
        {
            numsOfXVertices = Mathf.Max(2,
                Mathf.RoundToInt((width * vertexPerUnit)));
        }
        _pointCount = numsOfXVertices;
        _vertexSpacing = width / (_pointCount - 1);

        GenerateMesh();
        ResetCollider();

        _velocities = new float[_pointCount];
        _accelerations = new float[_pointCount];
        _heights = new float[_pointCount];
        _targetHeights = new float[_pointCount];
        _externalForces = new float[_pointCount];

        CreateWaterPoints();
    }

    private void SimulateStep(float dt)
    {
        float waveCoefficient =
            (_waveSpeed * _waveSpeed) / (_vertexSpacing * _vertexSpacing);
        for (int i = 1; i < _pointCount - 1; i++)
        {
            float spring = -_stiffness * (_heights[i] - _targetHeights[i]);
            float damping = -_resistance * _velocities[i];

            float wave = waveCoefficient *
                (
                    _heights[i - 1]
                    - 2f * _heights[i]
                    + _heights[i + 1]
                );

            _accelerations[i] =
                spring +
                damping +
                wave +
                _externalForces[i];
        }

        for (int i = 1; i < _pointCount - 1; ++i)
        {
            _velocities[i] += _accelerations[i] * dt;
            _heights[i] += _velocities[i] * dt;
        }
    }
    private void SimulateWater()
    {
        float subDt = Time.fixedDeltaTime / _simulationSubsteps;

        for (int step = 0; step < _simulationSubsteps; ++step)
        {
            SimulateStep(subDt);
        }

        Array.Clear(_externalForces, 0, _externalForces.Length);
    }

    private void UpdateMesh()
    {
        for (int i = 0; i < _pointCount; ++i)
        {
            _vertices[_topVerticesIndex[i]].y =
                _heights[i];
        }

        _mesh.SetVertices(
            _vertices,
            0,
            _vertices.Length,
            MeshUpdateFlags.DontRecalculateBounds |
            MeshUpdateFlags.DontValidateIndices |
            MeshUpdateFlags.DontResetBoneBounds
        );
    }

    private void UpdateCollider()
    {
        _boxCollider.size = new Vector2(width, height);
        _boxCollider.offset = Vector2.zero;
    }


    private void FixedUpdate()
    {
        SimulateWater();
        UpdateMesh();
    }

    public void Splash(Collider2D collider, float force)
    {
        for (int i = 0; i < _pointCount; ++i)
        {
            Vector2 vertexWorldPos = transform.TransformPoint(_vertices[_topVerticesIndex[i]]);

            if (collider.OverlapPoint(vertexWorldPos))
            {
                //Debug.Log("Hit!");
                _externalForces[i] = force;
            }
        }
    }

    private void CreateWaterPoints()
    {
        for (int i = 0; i < _topVerticesIndex.Length; ++i)
        {

            _heights[i] = _vertices[_topVerticesIndex[i]].y;
            _targetHeights[i] = _vertices[_topVerticesIndex[i]].y;
            _velocities[i] = 0f;
            _accelerations[i] = 0f;
            _externalForces[i] = 0f;
        }
    }

    private void ResetCollider()
    {
        _edgeCollider = GetComponent<EdgeCollider2D>();

        Vector2[] newPoints = new Vector2[2];

        Vector2 left = new Vector2(_vertices[_topVerticesIndex[0]].x, _vertices[_topVerticesIndex[0]].y);
        newPoints[0] = left;

        Vector2 right = new Vector2(_vertices[_topVerticesIndex[_topVerticesIndex.Length - 1]].x, _vertices[_topVerticesIndex[_topVerticesIndex.Length - 1]].y);
        newPoints[1] = right;

        _edgeCollider.offset = Vector2.zero;
        _edgeCollider.points = newPoints;

        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.size = new Vector2(width, height);
        _boxCollider.offset = Vector2.zero;
    }

    private void GenerateMesh()
    {
        if (_mesh != null)
        {
            DestroyImmediate(_mesh);
            _mesh = null;
        }

        _mesh = new Mesh();
        _mesh.name = "Water Mesh";

        _vertices = new Vector3[numsOfXVertices * numsOfYVertices];
        _topVerticesIndex = new int[numsOfXVertices];
        int index = 0;
        for (int y = 0; y < numsOfYVertices; ++y)
        {
            float yPos = (y / (float)(numsOfYVertices - 1)) * height - height * 0.5f;
            for (int x = 0; x < numsOfXVertices; ++x, ++index)
            {
                float xPos = (x / (float)(numsOfXVertices - 1)) * width - width * 0.5f;
                _vertices[index] = new Vector3(xPos, yPos, 0f);

                if (y == numsOfYVertices - 1)
                {
                    _topVerticesIndex[x] = index;
                }
            }
        }

        index = 0;
        int[] triangles = new int[(numsOfYVertices - 1) * (numsOfXVertices - 1) * 6];
        for (int i = 0; i < numsOfXVertices - 1; ++i)
        {
            // One quad
            int bottomLeft = i;
            int bottomRight = (i + 1);
            int topLeft = i + numsOfXVertices;
            int topRight = topLeft + 1;

            triangles[index++] = bottomLeft;
            triangles[index++] = topLeft;
            triangles[index++] = bottomRight;

            triangles[index++] = bottomRight;
            triangles[index++] = topLeft;
            triangles[index++] = topRight;
        }

        Vector2[] uvs = new Vector2[_vertices.Length];
        for (int j = 0; j < uvs.Length; ++j)
        {
            uvs[j] = new Vector2((_vertices[j].x + width * 0.5f) / width, (_vertices[j].y + height * 0.5f) / height);
        }

        _mesh.vertices = _vertices;
        _mesh.triangles = triangles;
        _mesh.uv = uvs;

        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();

        if (_meshFilter == null)
        {
            _meshFilter = GetComponent<MeshFilter>();
        }
        _meshFilter.mesh = _mesh;
        if (_meshRenderer == null)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        _meshRenderer.material = material;
    }



#if UNITY_EDITOR
    [CustomEditor(typeof(InteractiveWater))]
    public class InteractiveWaterEditor : Editor
    {
        private InteractiveWater _water;

        private void OnEnable()
        {
            _water = (InteractiveWater)target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            InspectorElement.FillDefaultInspector(root, serializedObject, this);
            root.Add(new VisualElement { style = { height = 10 } });

            Button generateMeshButton = new Button(() => _water.GenerateMesh())
            {
                text = "Generate Mesh"
            };
            root.Add(generateMeshButton);

            Button placeColliderButton = new Button(() => _water.ResetCollider())
            {
                text = "Place Collider"
            };
            root.Add(placeColliderButton);

            //Button generateWaterPointsButton = new Button(() => _water.CreateWaterPoints())
            //{
            //    text = "Generate Water Points"
            //};
            //root.Add(generateWaterPointsButton);

            return root;
        }

        private void ChangeDimensions(ref float width, ref float height, float calculatedWidthMax, float calculatedHeightMax)
        {
            width = Mathf.Max(.1f, calculatedWidthMax);
            height = Mathf.Max(.1f, calculatedHeightMax);
        }


        private void OnSceneGUI()
        {
            // Draw the wireframe box
            Handles.color = _water.SelectBoxColor;
            Vector3 center = _water.transform.position;
            Vector3 size = new Vector3(_water.width, _water.height, 0.1f);
            Handles.DrawWireCube(center, size);

            // Handles for width and height
            float handleSize = HandleUtility.GetHandleSize(center) * 0.1f;
            Vector3 snap = Vector3.one * 0.1f;

            // Corner handles
            Vector3[] corners = new Vector3[4];
            corners[0] = center + new Vector3(-_water.width * 0.5f, -_water.height * 0.5f, 0);  // Bottom-left
            corners[1] = center + new Vector3(_water.width * 0.5f, -_water.height * 0.5f, 0);  // Bottom-right
            corners[2] = center + new Vector3(-_water.width * 0.5f, _water.height * 0.5f, 0);  // Top-left
            corners[3] = center + new Vector3(_water.width * 0.5f, _water.height * 0.5f, 0);  // Top-right

            // Handle for each corner
            EditorGUI.BeginChangeCheck();
            Vector3 newBottomLeft = Handles.FreeMoveHandle(corners[0], handleSize, snap, Handles.CubeHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                ChangeDimensions(ref _water.width, ref _water.height, corners[1].x - newBottomLeft.x, corners[3].y - newBottomLeft.y);
                _water.transform.position += new Vector3((newBottomLeft.x - corners[0].x) * 0.5f, (newBottomLeft.y - corners[0].y) * 0.5f, 0);
            }

            EditorGUI.BeginChangeCheck();
            Vector3 newBottomRight = Handles.FreeMoveHandle(corners[1], handleSize, snap, Handles.CubeHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                ChangeDimensions(ref _water.width, ref _water.height, newBottomRight.x - corners[0].x, corners[3].y - newBottomRight.y);
                _water.transform.position += new Vector3((newBottomRight.x - corners[1].x) * 0.5f, (newBottomRight.y - corners[1].y) * 0.5f, 0);
            }

            EditorGUI.BeginChangeCheck();
            Vector3 newTopLeft = Handles.FreeMoveHandle(corners[2], handleSize, snap, Handles.CubeHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                ChangeDimensions(ref _water.width, ref _water.height, corners[3].x - newTopLeft.x, newTopLeft.y - corners[0].y);
                _water.transform.position += new Vector3((newTopLeft.x - corners[2].x) * 0.5f, (newTopLeft.y - corners[2].y) * 0.5f, 0);
            }

            EditorGUI.BeginChangeCheck();
            Vector3 newTopRight = Handles.FreeMoveHandle(corners[3], handleSize, snap, Handles.CubeHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                ChangeDimensions(ref _water.width, ref _water.height, newTopRight.x - corners[2].x, newTopRight.y - corners[1].y);
                _water.transform.position += new Vector3((newTopRight.x - corners[3].x) * 0.5f, (newTopRight.y - corners[3].y) * 0.5f, 0);
            }

            // Update the mesh if changed
            if (GUI.changed)
            {
                _water.InitializeWater();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color oldColor = Gizmos.color;
        Gizmos.color = WaterPointColor;
        for (int i = 0; i < _pointCount; ++i)
        {
            Gizmos.DrawWireSphere(gameObject.transform.TransformPoint(_vertices[_topVerticesIndex[i]]), 0.15f);
        }
        Gizmos.color = oldColor;
    }

#endif
}

