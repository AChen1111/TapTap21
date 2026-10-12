using UnityEngine;
using FXSystem;


namespace GamePlay.Environment.Water
{
    public class WaterTriggerHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask _waterMask;

        //private EdgeCollider2D _edgeCollider;
        private InteractiveWater _water;

        private void Awake()
        {
            _water = GetComponent<InteractiveWater>();
            //_edgeCollider = GetComponent<EdgeCollider2D>();
        }

        private Vector2 GetCollisionPoint(Collider2D other)
        {
            if (!TryGetComponent<Collider2D>(out Collider2D waterCol))
                return Vector2.zero;

            ColliderDistance2D dist = waterCol.Distance(other);
            return dist.pointA;
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
                    Collider2D waterCol = GetComponent<Collider2D>();
                    ColliderDistance2D dist = waterCol.Distance(collision);
                    Vector2 splashPos = dist.pointA;

                    if (collision.transform.position.y >= _water.Vertices[1].y + localPos.y)
                    {
                        FX.Play("water_splash", splashPos);//GetCollisionPoint(collision));
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

}

