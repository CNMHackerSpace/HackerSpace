// Copyright (c) 2025. All rights reserved.

namespace Server.Components.Account;

/// <summary>
/// Represents the type of passkey operation.
/// </summary>
public enum PasskeyOperation
{
    /// <summary>
    /// Create a new passkey.
    /// </summary>
    Create = 0,

    /// <summary>
    /// Request an existing passkey.
    /// </summary>
    Request = 1,
}
