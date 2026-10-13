using MaxioAdvancedBilling.Core.Authentication;
using MaxioAdvancedBilling.Core.Authentication.Basic;

namespace MaxioAdvancedBilling;

internal sealed class AuthSchemes
{
    public IAuthScheme BasicAuth { get; }

    public AuthSchemes(MaxioAdvancedBillingClientOptions options)
    {
        BasicAuth = BasicAuthScheme.Create(options.BasicAuth);
    }
}
