using Pluralize.Core;
using Pluralize.Core.Exceptions;

namespace Pluralize.UnitTests
{
    public class PluralizerTests
    {
        [Fact]
        public void Should_Pluralize_Data_From_Resources_File()
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
                Assert.Equal(singular, pluralizer.Singularize(plural));
                Assert.Equal(singular, pluralizer.Singularize(singular));
            }
        }

        [Fact]
        public void Should_Singularize_Data_From_Resources_File()
        {
            // Arrange
            var pluralizer = new Pluralizer();
            var resources = Resources.InputData.Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // Act Assert
            foreach (var line in resources)
            {
                var singular = line.Split(',')[0];
                var plural = line.Split(',')[1];
                Assert.Equal(singular, pluralizer.Singularize(plural));
                Assert.Equal(singular, pluralizer.Singularize(singular));
                Assert.Equal(plural, pluralizer.Pluralize(singular));
                Assert.Equal(plural, pluralizer.Pluralize(plural));
            }
        }

        [Fact]
        public void Should_Throw_NullOrEmptyWordException()
        {
            // Arrange
            var pluralizer = new Pluralizer();
            string wordEmpty = "";
            string wordNull = null;
            var expectedException = new NullOrEmptyWordException();

            // Act
            Assert.Throws<NullOrEmptyWordException>(() => pluralizer.Pluralize(wordEmpty));
            Assert.Throws<NullOrEmptyWordException>(() => pluralizer.Singularize(wordEmpty));
            Assert.Throws<NullOrEmptyWordException>(() => pluralizer.Pluralize(wordNull));
            Assert.Throws<NullOrEmptyWordException>(() => pluralizer.Singularize(wordNull));

            // Assert
            Assert.Equal(expectedException.Message, new NullOrEmptyWordException().Message);
        }
    }
}
