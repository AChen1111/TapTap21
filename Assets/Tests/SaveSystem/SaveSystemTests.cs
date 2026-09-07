using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using LitJson;
using NUnit.Framework;
using TapTap21.SaveSystem.dyh;
using UnityEngine;

namespace TapTap21SaveSystemTests
{
    [Serializable]
    public class SaveSystemTestData
    {
        public int number;
        public float ratio;
        public bool enabled;
        public string text;
        public TestKind kind;
        public List<int> numbers = new List<int>();
        public Dictionary<string, int> lookup = new Dictionary<string, int>();
        public TestNested nested = new TestNested();
    }

    public enum TestKind { First, Second }

    [Serializable]
    public class TestNested { public string id; public int amount; }

    [Serializable]
    public class MigrationTestData { public int value; public string added; }

    public class SaveSystemTests
    {
        private readonly List<string> slots = new List<string>();

        [SetUp]
        public void SetUp()
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Saves"));
        }

        [TearDown]
        public void TearDown()
        {
            foreach (string slot in slots) SaveSystem.DeleteSlot(slot);
        }

        private string NewSlot() { string id = "test_" + Guid.NewGuid().ToString("N"); slots.Add(id); return id; }
        private static SaveDefinition<SaveSystemTestData> NewDefinition(string suffix, int version = 1)
        {
            return SaveSystem.Register<SaveSystemTestData>("test_data_" + suffix, "test_data_" + suffix + ".json", version);
        }

        [Test]
        public void SavesAndLoadsAllCommonDataTypes()
        {
            SaveDefinition<SaveSystemTestData> definition = NewDefinition(Guid.NewGuid().ToString("N"));
            string slot = NewSlot();
            var expected = new SaveSystemTestData { number = 42, ratio = 0.5f, enabled = true, text = "hello", kind = TestKind.Second, nested = new TestNested { id = "nested", amount = 7 } };
            expected.numbers.AddRange(new[] { 1, 2, 3 });
            expected.lookup["coins"] = 500;
            Assert.IsTrue(SaveSystem.Save(definition, slot, expected).Success);
            LoadResult<SaveSystemTestData> result = SaveSystem.Load(definition, slot);
            Assert.IsTrue(result.Success, result.Message);
            Assert.AreEqual(42, result.Data.number);
            Assert.AreEqual(TestKind.Second, result.Data.kind);
            Assert.AreEqual(3, result.Data.numbers.Count);
            Assert.AreEqual(500, result.Data.lookup["coins"]);
            Assert.AreEqual("nested", result.Data.nested.id);
        }

        [Test]
        public void PrimitiveAndCollectionDefinitionsCanBeUsedDirectly()
        {
            string suffix = Guid.NewGuid().ToString("N");
            string slot = NewSlot();
            SaveDefinition<int> integer = SaveSystem.Register<int>("int_" + suffix, "int_" + suffix + ".json");
            SaveDefinition<float> single = SaveSystem.Register<float>("float_" + suffix, "float_" + suffix + ".json");
            SaveDefinition<bool> boolean = SaveSystem.Register<bool>("bool_" + suffix, "bool_" + suffix + ".json");
            SaveDefinition<string> text = SaveSystem.Register<string>("string_" + suffix, "string_" + suffix + ".json");
            SaveDefinition<TestKind> enumeration = SaveSystem.Register<TestKind>("enum_" + suffix, "enum_" + suffix + ".json");
            SaveDefinition<int[]> array = SaveSystem.Register<int[]>("array_" + suffix, "array_" + suffix + ".json");
            SaveDefinition<List<int>> list = SaveSystem.Register<List<int>>("list_" + suffix, "list_" + suffix + ".json");
            SaveDefinition<Dictionary<string, int>> dictionary = SaveSystem.Register<Dictionary<string, int>>("dict_" + suffix, "dict_" + suffix + ".json");
            Assert.IsTrue(SaveSystem.Save(integer, slot, 7).Success);
            Assert.IsTrue(SaveSystem.Save(single, slot, 1.25f).Success);
            Assert.IsTrue(SaveSystem.Save(boolean, slot, true).Success);
            Assert.IsTrue(SaveSystem.Save(text, slot, "text").Success);
            Assert.IsTrue(SaveSystem.Save(enumeration, slot, TestKind.Second).Success);
            Assert.IsTrue(SaveSystem.Save(array, slot, new[] { 1, 2 }).Success);
            Assert.IsTrue(SaveSystem.Save(list, slot, new List<int> { 3, 4 }).Success);
            Assert.IsTrue(SaveSystem.Save(dictionary, slot, new Dictionary<string, int> { { "a", 5 } }).Success);
            Assert.AreEqual(7, SaveSystem.Load(integer, slot).Data);
            Assert.AreEqual(1.25f, SaveSystem.Load(single, slot).Data);
            Assert.IsTrue(SaveSystem.Load(boolean, slot).Data);
            Assert.AreEqual("text", SaveSystem.Load(text, slot).Data);
            Assert.AreEqual(TestKind.Second, SaveSystem.Load(enumeration, slot).Data);
            CollectionAssert.AreEqual(new[] { 1, 2 }, SaveSystem.Load(array, slot).Data);
            CollectionAssert.AreEqual(new[] { 3, 4 }, SaveSystem.Load(list, slot).Data);
            Assert.AreEqual(5, SaveSystem.Load(dictionary, slot).Data["a"]);
        }

        [Test]
        public void SlotsDoNotOverwriteEachOther()
        {
            SaveDefinition<SaveData> definition = SaveDefinitions.Main;
            string first = NewSlot(); string second = NewSlot();
            SaveSystem.Save(definition, first, new SaveData { version = 11 });
            SaveSystem.Save(definition, second, new SaveData { version = 22 });
            Assert.AreEqual(11, SaveSystem.Load(definition, first).Data.version);
            Assert.AreEqual(22, SaveSystem.Load(definition, second).Data.version);
        }

        [Test]
        public void MissingAndInvalidSlotsReturnResults()
        {
            Assert.AreEqual(SaveStatus.NotFound, SaveSystem.Load(SaveDefinitions.Main, NewSlot()).Status);
            Assert.AreEqual(SaveStatus.InvalidSlot, SaveSystem.Load(SaveDefinitions.Main, "../escape").Status);
        }

        [Test]
        public void CorruptFormalFileRecoversBackup()
        {
            SaveDefinition<SaveData> definition = SaveDefinitions.Main;
            string slot = NewSlot();
            SaveSystem.Save(definition, slot, new SaveData { version = 1 });
            SaveSystem.Save(definition, slot, new SaveData { version = 2 });
            string path = Path.Combine(Application.persistentDataPath, "Saves", slot, definition.FileName);
            File.WriteAllText(path, "{ this is not json }");
            LoadResult<SaveData> result = SaveSystem.Load(definition, slot);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.UsedBackup);
            Assert.AreEqual(1, result.Data.version);
        }

        [Test]
        public void DuplicateDefinitionsAreRejected()
        {
            string suffix = Guid.NewGuid().ToString("N");
            SaveSystem.Register<SaveData>("duplicate_key_" + suffix, "duplicate_file_" + suffix + ".json");
            Assert.Throws<SaveRegistrationException>(() => SaveSystem.Register<SaveData>("duplicate_key_" + suffix, "other_" + suffix + ".json"));
            Assert.Throws<SaveRegistrationException>(() => SaveSystem.Register<SaveData>("other_key_" + suffix, "duplicate_file_" + suffix + ".json"));
        }

        [Test]
        public void BatchSaveAndSlotSummaryWork()
        {
            string slot = NewSlot();
            SaveSnapshot snapshot = SaveSystem.CreateSnapshot(slot).SetMetadata("第一槽", "SampleScene", 123);
            snapshot.Set(SaveDefinitions.Main, new SaveData { version = 1 });
            snapshot.Set(SaveDefinitions.Player, new PlayerSaveData { level = 3, coins = 9 });
            SaveResult save = SaveSystem.SaveSlot(snapshot);
            Assert.IsTrue(save.Success, save.Message);
            LoadSlotResult loaded = SaveSystem.LoadSlot(slot);
            Assert.IsTrue(loaded.Success, loaded.Message);
            Assert.AreEqual("第一槽", loaded.Info.DisplayName);
            Assert.AreEqual(2, loaded.Info.ItemCount);
            Assert.AreEqual(3, loaded.Get(SaveDefinitions.Player).Data.level);
            Assert.IsNotNull(SaveSystem.GetSlotInfo(slot));
        }

        [Test]
        public void FailedBatchValidationLeavesExistingSlotUntouched()
        {
            string slot = NewSlot();
            SaveSnapshot first = SaveSystem.CreateSnapshot(slot);
            first.Set(SaveDefinitions.Main, new SaveData { version = 1 });
            Assert.IsTrue(SaveSystem.SaveSlot(first).Success);
            SaveDefinition<SaveSystemTestData> invalidDefinition = NewDefinition(Guid.NewGuid().ToString("N"));
            SaveSnapshot second = SaveSystem.CreateSnapshot(slot);
            second.Set(SaveDefinitions.Main, new SaveData { version = 2 });
            second.Set(invalidDefinition, new SaveSystemTestData { ratio = float.NaN });
            SaveResult result = SaveSystem.SaveSlot(second);
            Assert.AreEqual(SaveStatus.ValidationFailed, result.Status);
            Assert.AreEqual(1, SaveSystem.Load(SaveDefinitions.Main, slot).Data.version);
        }

        [Test]
        public void DeleteItemAndSlotWork()
        {
            string slot = NewSlot();
            SaveSystem.Save(SaveDefinitions.Main, slot, new SaveData());
            Assert.IsTrue(SaveSystem.Delete(SaveDefinitions.Main, slot).Success);
            Assert.AreEqual(SaveStatus.NotFound, SaveSystem.Load(SaveDefinitions.Main, slot).Status);
            SaveSystem.Save(SaveDefinitions.Main, slot, new SaveData());
            Assert.IsTrue(SaveSystem.DeleteSlot(slot).Success);
            Assert.IsFalse(SaveSystem.ExistsSlot(slot));
        }

        [Test]
        public void StrongSlotIdsSupportFixedAndDynamicSlots()
        {
            SaveData expected = new SaveData { version = 7 };

            SaveResult fixedSave = SaveSystem.Save(SaveDefinitions.Main, SaveSlots.Slot0, expected);
            Assert.IsTrue(fixedSave.Success, fixedSave.Message);
            Assert.AreEqual(7, SaveSystem.Load(SaveDefinitions.Main, SaveSlots.Slot0).Data.version);

            SaveSlotId dynamicId = SaveSystem.CreateSlotId();
            slots.Add(dynamicId.Value);
            SaveSnapshot snapshot = SaveSystem.CreateSnapshot(dynamicId)
                .SetMetadata("玩家自定义名称", "SampleScene", 10)
                .Set(SaveDefinitions.Main, expected);

            Assert.IsTrue(SaveSystem.SaveSlot(snapshot).Success);
            SaveSlotInfo info = SaveSystem.GetSlotInfo(dynamicId);
            Assert.AreEqual(dynamicId, info.Id);
            Assert.AreEqual("玩家自定义名称", info.DisplayName);

            SaveSystem.DeleteSlot(SaveSlots.Slot0);
        }

        [Test]
        public void InvalidNumericDataIsRejectedBeforeWriting()
        {
            SaveDefinition<SaveSystemTestData> definition = NewDefinition(Guid.NewGuid().ToString("N"));
            string slot = NewSlot();
            SaveResult result = SaveSystem.Save(definition, slot, new SaveSystemTestData { ratio = float.NaN });
            Assert.AreEqual(SaveStatus.ValidationFailed, result.Status);
            Assert.IsFalse(SaveSystem.ExistsSlot(slot));
        }

        [Test]
        public void HighVersionAndTypeMismatchAreReported()
        {
            SaveDefinition<SaveData> definition = SaveDefinitions.Main;
            string slot = NewSlot();
            SaveSystem.Save(definition, slot, new SaveData());
            string path = Path.Combine(Application.persistentDataPath, "Saves", slot, definition.FileName);
            string json = File.ReadAllText(path).Replace("\"version\":1", "\"version\":99");
            File.WriteAllText(path, json);
            Assert.AreEqual(SaveStatus.VersionUnsupported, SaveSystem.Load(definition, slot).Status);
            SaveSystem.Save(definition, slot, new SaveData());
            // 清除旧备份，确保下面的类型损坏场景不会被备份恢复掩盖。
            string backupPath = path + ".bak";
            if (File.Exists(backupPath)) File.Delete(backupPath);
            json = File.ReadAllText(path).Replace(definition.Key, "not_the_registered_key");
            File.WriteAllText(path, json);
            Assert.AreEqual(SaveStatus.TypeMismatch, SaveSystem.Load(definition, slot).Status);
        }

        [Test]
        public void MigrationStepsAreApplied()
        {
            string suffix = Guid.NewGuid().ToString("N");
            SaveDefinition<MigrationTestData> definition = SaveSystem.Register<MigrationTestData>("migration_" + suffix, "migration_" + suffix + ".json", 2);
            SaveMigration.Register(definition, 1, 2, old => { old.added = "migrated"; old.value += 1; return old; });
            string slot = NewSlot();
            string dataJson = JsonMapper.ToJson(new MigrationTestData { value = 4 });
            string escapedDataJson = "\"" + dataJson.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
            string envelope = "{\"key\":\"" + definition.Key + "\",\"typeName\":\"" + definition.DataType.FullName + "\",\"version\":1,\"savedAtUtc\":\"" + DateTime.UtcNow.ToString("O") + "\",\"checksum\":\"" + Checksum(dataJson) + "\",\"dataJson\":" + escapedDataJson + "}";
            string directory = Path.Combine(Application.persistentDataPath, "Saves", slot);
            Directory.CreateDirectory(directory);
            File.WriteAllText(Path.Combine(directory, definition.FileName), envelope);
            LoadResult<MigrationTestData> result = SaveSystem.Load(definition, slot);
            Assert.IsTrue(result.Success, result.Message);
            Assert.AreEqual(5, result.Data.value);
            Assert.AreEqual("migrated", result.Data.added);
        }

        private static string Checksum(string value)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
                var builder = new StringBuilder();
                foreach (byte b in bytes) builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
