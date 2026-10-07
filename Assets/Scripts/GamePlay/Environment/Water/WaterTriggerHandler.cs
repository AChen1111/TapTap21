using UnityEngine;

public class WaterTriggerHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _waterMask;

    private EdgeCollider2D _edgeCollider;
    private InteractiveWater _water;

    private void Awake()
    {
        _water = GetComponent<InteractiveWater>();
        _edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_waterMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 localPos = gameObject.transform.localPosition;
                Vector2 hitObjectPos = collision.transform.position;
                Bounds hitObjectBounds = collision.bounds;

                if (collision.transform.position.y >= _edgeCollider.points[1].y + _edgeCollider.offset.y + localPos.y)
                {

                }
                else
                {

                }

                float force = rb.linearVelocityY * _water.ForceMultiplier;
                force = Mathf.Clamp(force, -_water.ForceMax, _water.ForceMax);

                _water.Splash(collision, force);
            }
        }
    }
}
