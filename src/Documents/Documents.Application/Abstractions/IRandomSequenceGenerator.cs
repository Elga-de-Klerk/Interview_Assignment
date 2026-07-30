namespace Documents.Application.Abstractions
{
    public interface IRandomSequenceGenerator
    {
        string Generate(int length = 8);
    }
}
