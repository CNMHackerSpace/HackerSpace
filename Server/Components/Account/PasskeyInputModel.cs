// Copyright (c) 2025. All rights reserved.

namespace Server.Components.Account;

/// <summary>
/// Model for passkey input data.
/// </summary>
public class PasskeyInputModel
{
    /// <summary>
    /// Gets or sets the credential JSON.
    /// </summary>
    public string? CredentialJson { get; set; }

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string? Error { get; set; }
}
