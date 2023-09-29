namespace CIS.Service.Client.MetaSystem
{
    /// <summary>
    /// Permission types: read, create, update, remove
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// Read.
        /// </summary>
        Read,
        /// <summary>
        /// Create.
        /// </summary>
        Create,
        /// <summary>
        /// Update.
        /// </summary>
        Update,
        /// <summary>
        /// Delete.
        /// </summary>
        Delete,
        /// <summary>
        /// Execute (common commands).
        /// </summary>
        Execute
    }
}
