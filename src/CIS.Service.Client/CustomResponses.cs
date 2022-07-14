namespace CIS.Service.Client
{
    /// <summary>
    /// Custom responses.
    /// </summary>
    public static class CustomResponses
    {
        /// <summary>
        /// The response for case when The token is expired (response contains System.IdentityModel.Tokens.SecurityTokenExpiredException).
        /// </summary>
        public const string LifetimeValidationFailedResponse = "LifetimeValidationFailed";
    }
}
