namespace Documents.Application.Abstractions
{
    /// <summary>
    /// Service that holds random sequence generator behavior.
    /// </summary>
    public interface IRandomSequenceGenerator
    {
        /// <summary>
        /// Generates a random sequence.
        /// </summary>
        /// <param name="length">Optional argument to set 
        /// the length of the sequence.</param>
        /// <returns cref="string"></returns>
        string Generate(int length = 8);
    }
}
