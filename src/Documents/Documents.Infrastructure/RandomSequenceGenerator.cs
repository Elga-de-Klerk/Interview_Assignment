using Documents.Application.Abstractions;
using System.Security.Cryptography;

namespace Documents.Infrastructure
{
    public sealed class RandomSequenceGenerator : IRandomSequenceGenerator
    {
        public string Generate(int length = 8) => RandomNumberGenerator.GetHexString(length);
    }
}
