﻿namespace Microsoft.Extensions.Logging
{
    using System;
    using Divergic.Logging.Xunit;
    using EnsureThat;
    using global::Xunit.Abstractions;

    /// <summary>
    ///     The <see cref="LoggingBuilderExtensions" />
    ///     class provides extension methods for the <see cref="ILoggingBuilder" /> interface.
    /// </summary>
    public static class LoggingBuilderExtensions
    {
        /// <summary>
        ///     Adds a logger to writes to the xUnit test output to the specified logging builder.
        /// </summary>
        /// <param name="builder">The logging builder.</param>
        /// <param name="output">The xUnit test output helper.</param>
        /// <param name="customFormatter">Optional custom message formatter.</param>
        public static void AddXunit(
            this ILoggingBuilder builder,
            ITestOutputHelper output,
            Func<int, string, LogLevel, EventId, string, Exception, string> customFormatter = null)
        {
            Ensure.That(builder, nameof(builder)).IsNotNull();
            Ensure.That(output, nameof(output)).IsNotNull();

            // Object is added as a provider to the builder and cannot be disposed of here
#pragma warning disable CA2000 // Dispose objects before losing scope
            var provider = new TestOutputLoggerProvider(output, customFormatter);
#pragma warning restore CA2000 // Dispose objects before losing scope

            builder.AddProvider(provider);
        }
    }
}