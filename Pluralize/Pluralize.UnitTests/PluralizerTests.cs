using Pluralize.Core;
using Pluralize.Core.Exceptions;

namespace Pluralize.UnitTests
{
    public class PluralizerTests
    {
        private readonly Pluralizer _pluralizer;

        public PluralizerTests()
        {
            _pluralizer = new Pluralizer();
        }

        [Fact]
        public void Pluralize_NullOrEmptyWord_ThrowsException()
        {
            Assert.Throws<NullOrEmptyWordException>(() => _pluralizer.Pluralize(null!));
            Assert.Throws<NullOrEmptyWordException>(() => _pluralizer.Pluralize(string.Empty));
            Assert.Throws<NullOrEmptyWordException>(() => _pluralizer.Pluralize(" "));
        }

        [Theory]
        [InlineData("cat", "cats")]
        [InlineData("dog", "dogs")]
        [InlineData("child", "children")]
        public void Pluralize_ValidWord_ReturnsPlural(string singular, string expectedPlural)
        {
            var result = _pluralizer.Pluralize(singular);
            Assert.Equal(expectedPlural, result);
        }

        [Fact]
        public void Singular_NullOrEmptyWord_ThrowsException()
        {
            Assert.Throws<NullOrEmptyWordException>(() => _pluralizer.Singular(null!));
            Assert.Throws<NullOrEmptyWordException>(() => _pluralizer.Singular(string.Empty));
            Assert.Throws<NullOrEmptyWordException>(() => _pluralizer.Singular(" "));
        }

        [Theory]
        [InlineData("cats", "cat")]
        [InlineData("dogs", "dog")]
        [InlineData("children", "child")]
        public void Singular_ValidWord_ReturnsSingular(string plural, string expectedSingular)
        {
            var result = _pluralizer.Singular(plural);
            Assert.Equal(expectedSingular, result);
        }

        [Fact]
        public void Antonymize_NullOrEmptyWord_ThrowsException()
        {
            Assert.Throws<NullOrEmptyWordException>(() => _pluralizer.Antonym(null!));
            Assert.Throws<NullOrEmptyWordException>(() => _pluralizer.Antonym(string.Empty));
            Assert.Throws<NullOrEmptyWordException>(() => _pluralizer.Antonym(" "));
        }

        [Theory]
        [InlineData("active", "inactive")]
        [InlineData("approved", "disapproved")]
        [InlineData("opening", "closing")]
        public void Antonymize_ValidWord_ReturnsAntonym(string word, string expectedAntonym)
        {
            var result = _pluralizer.Antonym(word);
            Assert.Equal(expectedAntonym, result);
        }

        [Theory]
        [InlineData("unknown", "unknown")]
        public void Antonymize_UnknownWord_ReturnsEmptyString(string word, string expectedAntonym)
        {
            var result = _pluralizer.Antonym(word);
            Assert.Equal(expectedAntonym, result);
        }
    }
}
