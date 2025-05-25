// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Exapt.Tests;

public class Test
{
    [Test]
    public void Simulate()
    {
        Program.Initialize(
            Environment.GetEnvironmentVariable("EXAPUNKS_DIR")
                ?? throw new MissingEnvironmentVariableException(@"Environment variable ""EXAPUNKS_DIR"" not set")
        );

        JObject expectedResults = JObject.Parse(File.ReadAllText("resources/expected_results.json"))!;
        string[] solutionFiles = Directory.GetFiles("resources/solutions", "*", SearchOption.AllDirectories);
        foreach (string solutionFile in solutionFiles)
        {
            TestContext.Progress.WriteLine($@"Running solution ""{solutionFile}""");

            string expectedResultKey = Path.GetRelativePath("resources/solutions", solutionFile)
                .Replace(".solution", "", StringComparison.Ordinal)
                .Replace("\\", "/", StringComparison.Ordinal);
            SolutionData? expectedResult = expectedResults[expectedResultKey]?.ToObject<SolutionData>();
            Assert.That(
                expectedResult,
                Is.Not.Null,
                $@"Failed to find solution key ""{expectedResultKey}"" in ""resources/expected_results.json"""
            );

            Stopwatch stopwatch = Stopwatch.StartNew();
            SolutionData result = Program.Simulate(solutionFile);
            stopwatch.Stop();
            TestContext.Progress.WriteLine($@"Solution ""{solutionFile}"" finished in {stopwatch.Elapsed}");

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
