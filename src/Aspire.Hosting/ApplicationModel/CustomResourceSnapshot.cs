// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Immutable;

namespace Aspire.Hosting.ApplicationModel;

/// <summary>
/// An immutable snapshot of the state of a resource.
/// </summary>
public sealed record CustomResourceSnapshot
{
    /// <summary>
    /// The type of the resource.
    /// </summary>
    public required string ResourceType { get; init; }

    /// <summary>
    /// The properties that should show up in the dashboard for this resource.
    /// </summary>
    public required ImmutableArray<ResourcePropertySnapshot> Properties { get; init; }

    /// <summary>
    /// The creation timestamp of the resource.
    /// </summary>
    public DateTime? CreationTimeStamp { get; init; }

    /// <summary>
    /// Represents the state of the resource.
    /// </summary>
    public ResourceStateSnapshot? State { get; init; }

    /// <summary>
    /// The exit code of the resource.
    /// </summary>
    public int? ExitCode { get; init; }

    /// <summary>
    /// The environment variables that should show up in the dashboard for this resource.
    /// </summary>
    public ImmutableArray<EnvironmentVariableSnapshot> EnvironmentVariables { get; init; } = [];

    /// <summary>
    /// The URLs that should show up in the dashboard for this resource.
    /// </summary>
    public ImmutableArray<UrlSnapshot> Urls { get; init; } = [];
}

/// <summary>
/// A snapshot of the resource state
/// </summary>
/// <param name="Text">The text for the state update.</param>
/// <param name="Style">The style for the state update. Use <seealso cref="KnownResourceStateStyles"/> for the supported styles.</param>
public sealed record ResourceStateSnapshot(string Text, string? Style)
{
    /// <summary>
    /// Convert text to state snapshot. The style will be null by default
    /// </summary>
    /// <param name="s"></param>
    public static implicit operator ResourceStateSnapshot?(string? s) =>
        s is null ? null : new(Text: s, Style: null);
}

/// <summary>
/// A snapshot of an environment variable.
/// </summary>
/// <param name="Name">The name of the environment variable.</param>
/// <param name="Value">The value of the environment variable.</param>
/// <param name="IsFromSpec">Determines if this environment variable was defined in the resource explicitly or computed (for e.g. inherited from the process hierarchy).</param>
public sealed record EnvironmentVariableSnapshot(string Name, string? Value, bool IsFromSpec);

/// <summary>
/// A snapshot of the url.
/// </summary>
/// <param name="Name">Name of the url.</param>
/// <param name="Url">The full uri.</param>
/// <param name="IsInternal">Determines if this url is internal.</param>
public sealed record UrlSnapshot(string Name, string Url, bool IsInternal);

/// <summary>
/// A snapshot of the resource property.
/// </summary>
/// <param name="Name">The name of the property.</param>
/// <param name="Value">The value of the property.</param>
public sealed record ResourcePropertySnapshot(string Name, object? Value);

/// <summary>
/// The set of well known resource states.
/// </summary>
public static class KnownResourceStateStyles
{
    /// <summary>
    /// The success state
    /// </summary>
    public static readonly string Success = "success";

    /// <summary>
    /// The error state. Useful for error messages.
    /// </summary>
    public static readonly string Error = "error";

    /// <summary>
    /// The info state. Useful for informational messages.
    /// </summary>
    public static readonly string Info = "info";

    /// <summary>
    /// The warn state. Useful for showing warnings.
    /// </summary>
    public static readonly string Warn = "warn";
}

/// <summary>
/// The set of well known resource states.
/// </summary>
public static class KnownResourceStates
{
    /// <summary>
    /// The hidden state. Useful for hiding the resource.
    /// </summary>
    public static readonly string Hidden = nameof(Hidden);

    /// <summary>
    /// The starting state. Useful for showing the resource is starting.
    /// </summary>
    public static readonly string Starting = nameof(Starting);

    /// <summary>
    /// The running state. Useful for showing the resource is running.
    /// </summary>
    public static readonly string Running = nameof(Running);

    /// <summary>
    /// The finished state. Useful for showing the resource has failed to start successfully.
    /// </summary>
    public static readonly string FailedToStart = nameof(FailedToStart);

    /// <summary>
    /// The stopping state. Useful for showing the resource is stopping.
    /// </summary>
    public static readonly string Stopping = nameof(Stopping);

    /// <summary>
    /// The exited state. Useful for showing the resource has exited.
    /// </summary>
    public static readonly string Exited = nameof(Exited);

    /// <summary>
    /// The finished state. Useful for showing the resource has finished.
    /// </summary>
    public static readonly string Finished = nameof(Finished);

    /// <summary>
    /// The waiting state. Useful for showing the resource is waiting for a dependency.
    /// </summary>
    public static readonly string Waiting = nameof(Waiting);

    /// <summary>
    /// List of terminal states.
    /// </summary>
    public static readonly string[] TerminalStates = [Finished, FailedToStart, Exited];
}
