namespace CIS.Service.Client.Interfaces
{
    /// <summary>
    /// Interface for classes with <see cref="Identity"/> property.
    /// </summary>
    public interface IIdentity : IConvertible
    {
        /// <summary>
        /// Gets or sets the primary identifier.
        /// </summary>
        System.Guid? Identity { get; set; }
    }
}
