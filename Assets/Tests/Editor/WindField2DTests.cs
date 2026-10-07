using System.Collections.Generic;
using GamePlay.Wind;
using NUnit.Framework;
using UnityEngine;

namespace GamePlay.Tests.Wind
{
    public class WindField2DTests
    {
        private readonly List<GameObject> _objects = new List<GameObject>();
        private readonly Vector2 _origin = new Vector2(12345f, 23456f);

        [TearDown]
        public void TearDown()
        {
            foreach (GameObject instance in _objects)
            {
                Object.DestroyImmediate(instance);
            }

            _objects.Clear();
            Physics2D.SyncTransforms();
        }

        private WindField2D CreateField(Vector2 direction, float strength)
        {
            GameObject instance = new GameObject("WindFieldTest");
            _objects.Add(instance);
            instance.transform.position = _origin;
            WindField2D field = instance.AddComponent<WindField2D>();
            field.HoverAtEnd = false;
            field.SetSize(6f, 1f);
            field.SetWind(direction, strength);
            Physics2D.SyncTransforms();
            return field;
        }

        [Test]
        public void GetForce_NormalizesDirectionAndChecksRange()
        {
            WindField2D field = CreateField(new Vector2(0f, 10f), 5f);

            Assert.AreEqual(new Vector2(0f, 5f), field.GetForce(_origin));
            Assert.AreEqual(Vector2.zero, field.GetForce(_origin + Vector2.right * 10f));
            Assert.IsTrue(field.GetComponent<BoxCollider2D>().isTrigger);
        }

        [Test]
        public void GetForce_ZeroDirectionOrNegativeStrengthReturnsZero()
        {
            WindField2D field = CreateField(Vector2.zero, 5f);
            Assert.AreEqual(Vector2.zero, field.GetForce(_origin));

            field.SetWind(Vector2.up, -5f);
            Assert.AreEqual(0f, field.Strength);
            Assert.AreEqual(Vector2.zero, field.GetForce(_origin));
        }

        [Test]
        public void SampleTotalForce_AddsOverlappingFieldsAndRemovesDisabledField()
        {
            WindField2D first = CreateField(Vector2.up, 5f);
            WindField2D second = CreateField(Vector2.right, 3f);
            Vector2 baseline = WindField2D.SampleTotalForce(_origin) -
                first.GetForce(_origin) - second.GetForce(_origin);

            Assert.AreEqual(baseline + new Vector2(3f, 5f),
                WindField2D.SampleTotalForce(_origin));
            first.enabled = false;
            Assert.AreEqual(baseline + new Vector2(3f, 0f),
                WindField2D.SampleTotalForce(_origin));
            first.enabled = true;
            Assert.AreEqual(baseline + new Vector2(3f, 5f),
                WindField2D.SampleTotalForce(_origin));
        }

        [Test]
        public void GetForce_DisabledColliderOrInactiveObjectReturnsZero()
        {
            WindField2D field = CreateField(Vector2.up, 5f);
            field.GetComponent<BoxCollider2D>().enabled = false;
            Assert.AreEqual(Vector2.zero, field.GetForce(_origin));

            field.GetComponent<BoxCollider2D>().enabled = true;
            field.gameObject.SetActive(false);
            Assert.AreEqual(Vector2.zero, field.GetForce(_origin));
        }

        [Test]
        public void Place_StartsAtSelectedPointAndExtendsOnlyForward()
        {
            WindField2D field = CreateField(Vector2.up, 5f);
            field.Place(_origin, Vector2.right);
            field.SetSize(6f, 1f);
            Assert.AreEqual(Vector2.right * 5f, field.GetForce(_origin + Vector2.right * 3f));
            Assert.AreEqual(Vector2.zero, field.GetForce(_origin - Vector2.right));
            Assert.AreEqual(Vector2.zero, field.GetForce(_origin + new Vector2(3f, 1f)));
            field.SetWind(Vector2.up, 5f);
            Assert.AreEqual(Vector2.up * 5f, field.GetForce(_origin + Vector2.up * 3f));
            Assert.AreEqual(Vector2.zero, field.GetForce(_origin + Vector2.right * 3f));
        }

        [Test]
        public void WindBody_GetForceAppliesResponseAndSupportsManualMode()
        {
            CreateField(Vector2.up, 5f);
            GameObject instance = new GameObject("WindBodyTest");
            _objects.Add(instance);
            instance.transform.position = _origin;
            WindBody2D body = instance.AddComponent<WindBody2D>();
            body.AutoApply = false;
            body.Response = 2f;
            Physics2D.SyncTransforms();
            Vector2 expected = WindField2D.SampleTotalForce(
                instance.GetComponent<Rigidbody2D>().worldCenterOfMass) * 2f;

            Assert.IsFalse(body.AutoApply);
            Assert.AreEqual(expected, body.GetForce());
            body.Response = -1f;
            Assert.AreEqual(0f, body.Response);
            Assert.AreEqual(Vector2.zero, body.GetForce());
            body.Response = 1f;
            body.enabled = false;
            Assert.AreEqual(Vector2.zero, body.GetForce());
        }

        [Test]
        public void HoverForce_RaisesQuicklyAndBrakesNearEnd()
        {
            WindField2D field = CreateField(Vector2.up, 5f);
            field.HoverAtEnd = true;
            Vector2 gravity = new Vector2(0f, -9.81f);
            Vector2 rising = field.CalculateHoverForce(_origin, Vector2.zero, 1f, gravity, 0f);
            Assert.Greater(rising.y + gravity.y, 20f);
            Vector2 braking = field.CalculateHoverForce(_origin + Vector2.up * 5.6f,
                Vector2.up * 6f, 1f, gravity, 0f);
            Assert.Less(braking.y + gravity.y, 0f);
        }

        [Test]
        public void HoverForce_TracksMovingTargetAndScalesForceWithMass()
        {
            WindField2D field = CreateField(Vector2.up, 5f);
            field.HoverAtEnd = true;
            Vector2 gravity = new Vector2(0f, -9.81f);
            Vector2 position = _origin + Vector2.up * field.HoverDistance;
            float targetSpeed = 0.1f * 0.6f * Mathf.PI * 2f;
            Vector2 oneMass = field.CalculateHoverForce(position,
                Vector2.up * targetSpeed, 1f, gravity, 0f);
            Vector2 twoMass = field.CalculateHoverForce(position,
                Vector2.up * targetSpeed, 2f, gravity, 0f);
            Assert.That(oneMass.y, Is.EqualTo(9.81f).Within(0.05f));
            Assert.That(twoMass.y, Is.EqualTo(oneMass.y * 2f).Within(0.001f));
        }

        [Test]
        public void SelectHoverField_UsesEndBufferOnlyForPreviouslySelectedField()
        {
            WindField2D field = CreateField(Vector2.up, 5f);
            field.HoverAtEnd = true;
            Vector2 overshoot = _origin + Vector2.up * 6.2f;
            Assert.IsNull(WindField2D.SelectHoverField(overshoot, null));
            Assert.AreSame(field, WindField2D.SelectHoverField(overshoot, field));
            Assert.IsNull(WindField2D.SelectHoverField(_origin + Vector2.up * 7f, field));
            Assert.IsNull(WindField2D.SelectHoverField(_origin + new Vector2(1f, 5f), field));
            field.enabled = false;
            Assert.IsNull(WindField2D.SelectHoverField(overshoot, field));
        }

        [Test]
        public void HoverForce_ZeroResponseOrDisabledFieldReturnsZero()
        {
            WindField2D field = CreateField(Vector2.up, 5f);
            field.HoverAtEnd = true;
            Assert.AreEqual(Vector2.zero, field.CalculateHoverForce(
                _origin, Vector2.zero, 1f, Vector2.down * 9.81f, 0f, 0f));
            field.enabled = false;
            Assert.AreEqual(Vector2.zero, field.CalculateHoverForce(
                _origin, Vector2.zero, 1f, Vector2.down * 9.81f, 0f));
        }
    }
}
