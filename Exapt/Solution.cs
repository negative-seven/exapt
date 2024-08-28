// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Exapt;

public class Solution
{
    public string PuzzleId { get; private set; }
    public IEnumerable<Wrappers.SolutionExa> Exas => inner.SolutionExas;
    public int Size { get; private set; }

    private readonly Wrappers.Solution inner;

    internal Solution(string solutionFilepath)
    {
        inner =
            Wrappers.Solution.FromFile(solutionFilepath)
            ?? throw new SolutionFileLoadException("Failed to load solution file");

        // Create a dummy Simulation instance to assemble code
        Dictionary<Wrappers.Team, IEnumerable<Wrappers.SolutionExa>> solutionExas =
            new() { { Wrappers.Team.Player, inner.SolutionExas } };
        _ = Wrappers.Simulation.Create(Wrappers.Puzzles.FromId(inner.PuzzleId), 0, Wrappers.Team.Player, solutionExas);

        Size = inner.SolutionExas.Select(e => Wrappers.Simulation.CountCodeSize(e.Code)).Sum();

        PuzzleId = inner.PuzzleId.Id;
    }

    public Simulation CreateSimulation(int testIndex)
    {
        return new Simulation(this, testIndex);
    }
}
