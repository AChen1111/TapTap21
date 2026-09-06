using AChen.Prefabs.Editor;
using NUnit.Framework;

namespace AChen.Tests.Prefabs
{
    public sealed class PrefabNameSpaceGeneratorTests
    {
        [TestCase("Bullet", "Bullet")]
        [TestCase("UI-Frame", "UI_Frame")]
        [TestCase("123 Bullet", "_123_Bullet")]
        [TestCase("class", "_class")]
        public void ToIdentifier_ProducesValidReadableCSharpIdentifier(string prefabName, string expected)
        {
            Assert.That(PrefabNameSpaceGenerator.ToIdentifier(prefabName), Is.EqualTo(expected));
        }

        [Test]
        public void BuildSource_RemovesDuplicateNamesAndResolvesIdentifierCollisions()
        {
            string source = PrefabNameSpaceGenerator.BuildSource(new[]
            {
                "Bullet",
                "Bullet",
                "UI Frame",
                "UI-Frame"
            });

            Assert.That(CountOccurrences(source, "public const string Bullet"), Is.EqualTo(1));
            Assert.That(source, Does.Contain("public const string UI_Frame = \"UI Frame\";"));
            Assert.That(source, Does.Contain("public const string UI_Frame_2 = \"UI-Frame\";"));
        }

        static int CountOccurrences(string source, string value)
        {
            int count = 0;
            int index = 0;
            while ((index = source.IndexOf(value, index, System.StringComparison.Ordinal)) >= 0)
            {
                count++;
                index += value.Length;
            }

            return count;
        }
    }
}
