﻿// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp7.SpacingRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using StyleCop.Analyzers.Test.SpacingRules;
    using TestHelper;
    using Xunit;

    public class SA1015CSharp7UnitTests : SA1015UnitTests
    {
        /// <summary>
        /// Verifies spacing around a <c>&gt;</c> character in tuple types.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        /// <seealso cref="SA1001CSharp7UnitTests.TestClosingGenericBracketsInTupleTypesNotFollowedBySpaceAsync"/>
        /// <seealso cref="SA1009CSharp7UnitTests.TestClosingGenericBracketsInTupleTypesNotFollowedBySpaceAsync"/>
        [Fact]
        public async Task TestClosingGenericBracketsInTupleTypesNotPrecededBySpaceAsync()
        {
            const string testCode = @"using System;

public class Foo
{
    public void TestMethod()
    {
        (Func<int > , Func<int > ) value = (null, null);
    }
}";
            const string fixedCode = @"using System;

public class Foo
{
    public void TestMethod()
    {
        (Func<int>, Func<int> ) value = (null, null);
    }
}";

            DiagnosticResult[] expected =
            {
                this.CSharpDiagnostic().WithLocation(7, 19).WithArguments(" not", "preceded"),
                this.CSharpDiagnostic().WithLocation(7, 19).WithArguments(" not", "followed"),
                this.CSharpDiagnostic().WithLocation(7, 32).WithArguments(" not", "preceded"),
            };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
            await this.VerifyCSharpFixAsync(testCode, fixedCode, cancellationToken: CancellationToken.None).ConfigureAwait(false);
        }
    }
}
