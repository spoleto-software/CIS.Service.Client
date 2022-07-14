namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The changing states of the object: none, added, changed, removed.
    /// </summary>
    public enum ChangeState
    {
        /// <summary>
        /// The object is not changed.
        /// </summary>
        None = 0,
        /// <summary>
        /// The object is added.
        /// </summary>
        Added = 1,
        /// <summary>
        /// The object is changed.
        /// </summary>
        Changed = 2,
        /// <summary>
        /// The object is removed.
        /// </summary>
        Removed = 3,
        /// <summary>
        /// The object is restored from recycle bin.
        /// </summary>
        Restored = 4
    }
}
