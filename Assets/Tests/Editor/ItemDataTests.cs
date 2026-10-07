using System;
using GamePlay.Core;
using NUnit.Framework;

namespace GamePlay.Tests.Items
{
    public class ItemDataTests
    {
        [TestCase(typeof(Item_Shovel), "Item_Shovel", 9)]
        [TestCase(typeof(Item_WindSeed), "Item_WindSeed", 11)]
        [TestCase(typeof(Item_Driftwood), "Item_Driftwood", 12)]
        public void Constructor_InitializesIdentityAndDefaultState(
            Type itemType, string expectedName, int expectedId)
        {
            Item item = (Item)Activator.CreateInstance(itemType);

            Assert.AreEqual(expectedName, item.Name);
            Assert.AreEqual(expectedId, item.Id);
            Assert.AreEqual(0, item.State);
        }

        [TestCase(typeof(Item_Shovel))]
        [TestCase(typeof(Item_WindSeed))]
        [TestCase(typeof(Item_Driftwood))]
        public void CopyItem_PreservesDataAndReturnsIndependentInstance(Type itemType)
        {
            Item original = (Item)Activator.CreateInstance(itemType);
            original.State = 3;

            Item copy = original.CopyItem();

            Assert.AreNotSame(original, copy);
            Assert.AreEqual(itemType, copy.GetType());
            Assert.AreEqual(original.Name, copy.Name);
            Assert.AreEqual(original.Id, copy.Id);
            Assert.AreEqual(original.State, copy.State);

            copy.State = 5;

            Assert.AreEqual(3, original.State);
        }
    }
}
