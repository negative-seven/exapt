// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Exapt;

public class Solution
{
    public string PuzzleId { get; private set; }
    public IEnumerable<Wrappers.SolutionExa> Exas => inner.SolutionExas;

    private readonly Wrappers.Solution inner;

    internal Solution(string solutionFilepath)
    {
        inner =
            Wrappers.Solution.FromFile(Path.GetFullPath(solutionFilepath))
            ?? throw new SolutionFileLoadException("Failed to load solution file");

        PuzzleId = inner.PuzzleId.Id;
    }

    public Simulation CreateSimulation(int testIndex)
    {
        return new Simulation(this, testIndex);
    }
}
