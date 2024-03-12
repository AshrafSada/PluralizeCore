using Pluralize.Core;
using Pluralize.Core.Exceptions;

namespace Pluralize.UnitTests
{
    public class PluralizerTests
    {
        [Fact]
        public void ShouldPluralizeDataFromResourcesFile()
        {
            // Arrange
            var pluralizer = new Pluralizer();
            var resources = Resources.InputData.Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // Act Assert
            foreach (var line in resources)
            {
                var singular = line.Split(',')[0];
                var plural = line.Split(',')[1];
                Assert.Equal(plural, pluralizer.Pluralize(singular));
                Assert.Equal(plural, pluralizer.Pluralize(plural));
                Assert.Equal(singular, pluralizer.Singular(plural));
                Assert.Equal(singular, pluralizer.Singular(singular));
            }
        }

        [Fact]
        public void ShouldSingularDataFromResourcesFile()
        {
            // Arrange
            var pluralizer = new Pluralizer();
            var resources = Resources.InputData.Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // Act Assert
            foreach (var line in resources)
            {
                var singular = line.Split(',')[0];
                var plural = line.Split(',')[1];
                Assert.Equal(singular, pluralizer.Singular(plural));
                Assert.Equal(singular, pluralizer.Singular(singular));
                Assert.Equal(plural, pluralizer.Pluralize(singular));
                Assert.Equal(plural, pluralizer.Pluralize(plural));
            }
        }

        [Fact]
        public void ShouldThrowNullOrEmptyWordException()
        {
            // Arrange
            var pluralizer = new Pluralizer();
            string wordEmpty = "";
            string wordNull = null;
            var expectedException = new NullOrEmptyWordException();

            // Act
            Assert.Throws<NullOrEmptyWordException>(() => pluralizer.Pluralize(wordEmpty));
            Assert.Throws<NullOrEmptyWordException>(() => pluralizer.Singular(wordEmpty));
            Assert.Throws<NullOrEmptyWordException>(() => pluralizer.Pluralize(wordNull!));
            Assert.Throws<NullOrEmptyWordException>(() => pluralizer.Singular(wordNull!));

            // Assert
            Assert.Equal(expectedException.Message, new NullOrEmptyWordException().Message);
        }
    }
}
