---
name: "dotnet-authentication"
description: "Load before wiring credentials into the Maxio Advanced Billing .NET SDK client in C#, or when a call comes back 401/403. Covers HTTP Basic: the credential each scheme takes, the MaxioAdvancedBillingClientOptions property it goes on, and a startup check that fails fast on a missing secret."
---

# Authenticating the Maxio Advanced Billing .NET SDK client

This API authenticates with HTTP Basic. Each scheme is a **nullable credentials property** on `MaxioAdvancedBillingClientOptions`; set the ones below, then construct the client (see `dotnet-client-initialization`).

| Scheme | Property on `MaxioAdvancedBillingClientOptions` | Credential |
| --- | --- | --- |
| HTTP Basic | `BasicAuth` | `BasicAuthCredentials` |

**That table is the entire authentication surface of this SDK.** `MaxioAdvancedBilling.Core.Authentication` also holds scheme classes this API does not use, so read what is configurable off the properties above rather than off that folder.

## Basic auth

**API Provider Notes:**

> The `username` is a Maxio Chargify API key. The `password` is `x`.

```csharp
using MaxioAdvancedBilling.Core.Authentication.Basic;

options.BasicAuth = new BasicAuthCredentials
{
    Username = "...",
    Password = "..."
};
```

Sends `Authorization: Basic base64(username:password)`. Both halves are required — a username with an empty password is misconfigured, not partially configured.

## ⚠ Missing credentials must stop the app from starting

⚠ **Configure nothing and the request goes out unauthenticated — no exception.** An unset credentials property on `MaxioAdvancedBillingClientOptions` sends the call with no credential, and it comes back `401`; the SDK never reports the missing credential. A required credential that is not configured is a deployment fault, not a request fault.

**Validate at startup and refuse to boot.** Bind the credentials into your own options type and make the host check it before the app serves anything:

```csharp
builder.Services
    .AddOptions<MaxioAdvancedBillingClientSettings>()
    .Bind(builder.Configuration.GetSection("MaxioAdvancedBillingClient"))
    .ValidateDataAnnotations()      // [Required] on each credential property
    .ValidateOnStart();             // throws during startup, not on first request
```

`ValidateOnStart()` is the load-bearing call — without it, `IOptions<T>` validation is lazy and fires on first resolution, which is a request, which is exactly the late failure you are trying to avoid. For a console app, an explicit guard is equally acceptable as long as it runs **before** the app is ready:

```csharp
if (string.IsNullOrWhiteSpace(settings.Username))
    throw new InvalidOperationException(
        "MaxioAdvancedBillingClient:Username is not configured. Set it via environment variable, " +
        "user-secrets, or your secret store before starting the app.");
```

Three rules for the message it fails with:

- **Name the missing config key**, so the operator knows what to set — `"MaxioAdvancedBillingClient:Username is not configured"`, not `"authentication failed"`.
- **Never echo the value**, present or absent — no length, no prefix, no masked form. A "configured: AC1234…" line is a secret in a log.
- **Do not fall back to a default, a placeholder, or an unauthenticated client.** Booting degraded hides the fault and pushes it to the first caller.

Check every value the schemes actually need. For this SDK that is:

| Value to validate | Feeds `MaxioAdvancedBillingClientOptions` property |
| --- | --- |
| `Username` | `BasicAuth` |
| `Password` | `BasicAuth` |

## Notes

- Set credentials **before** constructing the client, or inside the `AddMaxioAdvancedBillingClient(options => ...)` callback when registering via DI. `MaxioAdvancedBillingClientOptions` is read at construction; mutating it afterwards does not re-wire an existing client.
- Keep secrets out of source — load them from configuration (environment variables, a secret store, or any other `IConfiguration` source) instead of hardcoding, either inside the `AddMaxioAdvancedBillingClient(options => ...)` callback for a host or via a `ConfigurationBuilder()...Build()` chain for a console app.

