using Documents.Application.Abstractions;
using Documents.Application.DocumentMutation;
using NSubstitute;

namespace Documents.Tests.Application.Fixtures
{
    internal class DocumentMutationServiceFixture
    {
        private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        private readonly IRandomSequenceGenerator _randomSequenceGenerator = Substitute.For<IRandomSequenceGenerator>();

        public DocumentMutationServiceFixture SetupDateTimeProviderToReturn(DateTimeOffset dateTimeOffset)
        {
            _dateTimeProvider.CurrentDateTime.Returns(dateTimeOffset);
            return this;
        }

        public DocumentMutationServiceFixture SetupRandomSequenceGeneratorToReturn(string randomSequence)
        {
            _randomSequenceGenerator.Generate().Returns(randomSequence);
            return this;
        }

        public DocumentMutationService Build() => new(_dateTimeProvider, _randomSequenceGenerator);
    }
}
