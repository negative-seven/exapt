// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Exapt;

public class Simulation
{
    private readonly Solution solution;

    public bool Completed => inner.Completed;
    public int Cycles => inner.Cycles;
    public int CodeSize => solution.Exas.Sum(e => Wrappers.Simulation.CountCodeSize(e.Code));
    public int Activity => inner.Activity;

    private readonly Wrappers.Simulation inner;

    internal Simulation(Solution solution, int testIndex)
    {
        this.solution = solution;

        Dictionary<Wrappers.Team, IEnumerable<Wrappers.SolutionExa>> solutionExas = new()
        {
            { Wrappers.Team.Player, solution.Exas },
        };
        inner = Wrappers.Simulation.Create(
            Wrappers.Puzzles.FromId(new Wrappers.PuzzleId(solution.PuzzleId)),
            testIndex,
            Wrappers.Team.Player,
            solutionExas
        );
    }

    public void Step()
    {
        inner.Step();
    }

    public static void UseOptimizationsForPuzzle(string puzzleId)
    {
        Wrappers.Simulation.PatchPuzzleCompleteCheckPrefix(
            puzzleId switch
            {
                null => null,
                "PB004" => PreliminaryLeftArmCheck, // left arm
                "PB011B" => null, // heart
                "PB038" => null, // left hand
                "PB020" => PreliminarySawayamaWonderdiscCheck, // sawayama wonderdisc
                "PB030" => null, // visual cortex
                _ => PreliminaryLeaveNoTraceCheck,
            }
        );
    }

    private static bool PreliminaryLeaveNoTraceCheck(Wrappers.Simulation simulation)
    {
        return !simulation.Entities.Any(e => e.Team == Wrappers.Team.Player && e.IsSimExa());
    }

    private static bool PreliminaryLeftArmCheck(Wrappers.Simulation simulation)
    {
        Wrappers.LeftArmSpecificState leftArmSpecificState =
            simulation.PuzzleSpecificState.AsLeftArmSpecificState()
            ?? throw new InvalidPreliminaryCheckException("Simulation puzzle is not Left Arm");
        return leftArmSpecificState.OutputValues.Count() == 30;
    }

    private static bool PreliminarySawayamaWonderdiscCheck(Wrappers.Simulation simulation)
    {
        Wrappers.SawayamaWonderdiscSpecialState leftArmSpecificState =
            simulation.PuzzleSpecificState.AsSawayamaWonderdiscSpecificState()
            ?? throw new InvalidPreliminaryCheckException("Simulation puzzle is not Sawayama Wonderdisc");
        return leftArmSpecificState.OutputMatches.Count == 30;
    }
}
