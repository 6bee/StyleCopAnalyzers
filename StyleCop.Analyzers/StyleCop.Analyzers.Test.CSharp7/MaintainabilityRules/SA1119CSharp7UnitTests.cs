﻿// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp7.MaintainabilityRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using StyleCop.Analyzers.Test.MaintainabilityRules;
    using TestHelper;
    using Xunit;

    public class SA1119CSharp7UnitTests : SA1119UnitTests
    {
        /// <summary>
        /// Verifies that extra parentheses in pattern matching are reported.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        /// <seealso cref="SA1408CSharp7UnitTests.TestPatternMatchingAsync"/>
        [Fact(Skip = "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/2398")]
        public async Task TestPatternMatchingAsync()
        {
            var testCode = @"public class Foo
{
    public void Bar()
    {
        if ((new object() is bool b) && b)
        {
            return;
        }
    }
}";
            var fixedCode = @"public class Foo
{
    public void Bar()
    {
        if (new object() is bool b && b)
        {
            return;
        }
    }
}";

            DiagnosticResult[] expected =
            {
                this.CSharpDiagnostic(DiagnosticId).WithLocation(5, 13),
                this.CSharpDiagnostic(ParenthesesDiagnosticId).WithLocation(5, 13),
                this.CSharpDiagnostic(ParenthesesDiagnosticId).WithLocation(5, 36),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(2372, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/2372")]
        public async Task TestNegatedPatternMatchingAsync()
        {
            var testCode = @"public class Foo
{
    public void Bar()
    {
        object obj = null;
        if (!(obj is string anythng))
        {
            // ...
        }
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestTupleDeconstructionAsync()
        {
            var testCode = @"public class Foo
{
    public void Bar()
    {
        var (a, (b, c), d) = (1, (2, (3)), 4);
    }
}";
            var fixedCode = @"public class Foo
{
    public void Bar()
    {
        var (a, (b, c), d) = (1, (2, 3), 4);
    }
}";

            DiagnosticResult[] expected =
            {
                this.CSharpDiagnostic(DiagnosticId).WithLocation(5, 38),
                this.CSharpDiagnostic(ParenthesesDiagnosticId).WithLocation(5, 38),
                this.CSharpDiagnostic(ParenthesesDiagnosticId).WithLocation(5, 40),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode).ConfigureAwait(false);
        }
    }
}
