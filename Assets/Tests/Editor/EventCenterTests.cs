using System;
using System.Collections.Generic;
using System.Reflection;
using AChen.Events;
using NUnit.Framework;

namespace AChen.Tests.Events
{
    public sealed class EventCenterTests
    {
        static readonly MethodInfo s_resetState = typeof(EventCenter).GetMethod(
            "ResetState",
            BindingFlags.Static | BindingFlags.NonPublic);

        [SetUp]
        public void SetUp()
        {
            ResetEventCenter();
        }

        [TearDown]
        public void TearDown()
        {
            ResetEventCenter();
        }

        [Test]
        public void Dispatch_InvokesZeroOneAndTwoArgumentListeners()
        {
            EventId noArgs = new EventId("Test.NoArgs");
            EventId<int> oneArg = new EventId<int>("Test.OneArg");
            EventId<int, string> twoArgs = new EventId<int, string>("Test.TwoArgs");
            bool noArgsCalled = false;
            int receivedNumber = 0;
            string receivedText = null;

            EventCenter.AddListener(noArgs, () => noArgsCalled = true);
            EventCenter.AddListener(oneArg, value => receivedNumber = value);
            EventCenter.AddListener(twoArgs, (number, text) =>
            {
                receivedNumber = number;
                receivedText = text;
            });

            EventCenter.Dispatch(noArgs);
            EventCenter.Dispatch(oneArg, 21);
            EventCenter.Dispatch(twoArgs, 42, "typed");

            Assert.That(noArgsCalled, Is.True);
            Assert.That(receivedNumber, Is.EqualTo(42));
            Assert.That(receivedText, Is.EqualTo("typed"));
        }

        [Test]
        public void Dispatch_InvokesListenersInRegistrationOrder()
        {
            EventId evt = new EventId("Test.Order");
            List<int> order = new List<int>();

            EventCenter.AddListener(evt, () => order.Add(1));
            EventCenter.AddListener(evt, () => order.Add(2));

            EventCenter.Dispatch(evt);

            Assert.That(order, Is.EqualTo(new[] { 1, 2 }));
        }

        [Test]
        public void RemoveListener_PreventsFurtherInvocation()
        {
            EventId<int> evt = new EventId<int>("Test.Remove");
            int callCount = 0;
            Action<int> listener = _ => callCount++;
            EventCenter.AddListener(evt, listener);

            EventCenter.Dispatch(evt, 1);
            EventCenter.RemoveListener(evt, listener);
            EventCenter.Dispatch(evt, 2);

            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void Dispatch_WithNoListeners_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => EventCenter.Dispatch(new EventId("Test.Empty")));
        }

        [Test]
        public void SameNameWithDifferentSignature_Throws()
        {
            EventId<int> intEvent = new EventId<int>("Test.SharedName");
            EventId<string> stringEvent = new EventId<string>("Test.SharedName");
            EventCenter.Dispatch(intEvent, 1);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
                () => EventCenter.Dispatch(stringEvent, "wrong signature"));

            Assert.That(exception.Message, Does.Contain("Test.SharedName"));
        }

        [Test]
        public void AddListener_WithNullListener_Throws()
        {
            EventId<int> evt = new EventId<int>("Test.NullListener");
            Action<int> listener = null;

            Assert.Throws<ArgumentNullException>(() => EventCenter.AddListener(evt, listener));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void EventId_WithBlankName_Throws(string name)
        {
            Assert.Throws<ArgumentException>(() => new EventId(name));
        }

        [Test]
        public void ResetState_ClearsListenersAndSignatures()
        {
            EventId<int> intEvent = new EventId<int>("Test.Reset");
            int callCount = 0;
            EventCenter.AddListener(intEvent, _ => callCount++);

            ResetEventCenter();

            Assert.DoesNotThrow(() => EventCenter.Dispatch(new EventId<string>("Test.Reset"), "new signature"));
            Assert.That(callCount, Is.Zero);
        }

        static void ResetEventCenter()
        {
            Assert.That(s_resetState, Is.Not.Null, "EventCenter.ResetState must remain available for isolation.");
            s_resetState.Invoke(null, null);
        }
    }
}
