// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Exapt.Wrappers.Meta;
using HarmonyLib;

namespace Exapt.Wrappers;

[ClassWrapper("Sim")]
public class Simulation : NonStaticWrapper<Simulation>
{
    public bool Completed => InnerGetCompleted(Inner);
    public int Cycles => (int)Call("#=q6Z2p0iddfrFXKSNmGS9UBQ==")!;
    public int Activity => (int)Call("#=q24Y2a5fbidtcHWquAdDEIBgp$0kfXMpkH3qbYxIvP8g=")!;

    public IEnumerable<SimEntity> Entities =>
        Enumerable
            .Cast<object>((System.Collections.IEnumerable)Get("#=q5aeg_A8y67EWjoqZoAbaRw==")!)
            .Select(simEntityInner => new SimEntity(simEntityInner));

    public PuzzleSpecificState PuzzleSpecificState => new(Get("#=qFG0xJPPsuDX3$SwU$exm7U8J7bBymPTjhWxdMJU5CVA=")!);

    private static readonly Harmony harmony = new(nameof(Simulation));
    private static Func<Simulation, bool>? currentPuzzleCompleteCheckPrefix;

    private Simulation(object inner)
        : base(inner) { }

    public static Simulation Create(
        [NotNull] Puzzle puzzle,
        int testRunNumber,
        Team team,
        [NotNull] Dictionary<Team, IEnumerable<SolutionExa>> solutionExas
    )
    {
        Type dictionaryKeyType = Type.GetType("Team, Burbank")!;
        Type dictionaryValueType = typeof(IEnumerable<>).MakeGenericType(Type.GetType("SolutionExa, Burbank")!);
        Type dictionaryType = typeof(Dictionary<,>).MakeGenericType(dictionaryKeyType, dictionaryValueType);

        MethodInfo castToDictionaryValueMethod = typeof(Enumerable)
            .GetMethod("Cast")!
            .MakeGenericMethod(Type.GetType("SolutionExa, Burbank")!);
        object convertedSolutionExas = Utils.CallConstructor(dictionaryType)!;
        foreach (KeyValuePair<Team, IEnumerable<SolutionExa>> pair in solutionExas)
        {
            object key = pair.Key;
            object value = castToDictionaryValueMethod.Invoke(null, [pair.Value.Select(e => e.Inner)])!;
            _ = Utils.CallNonStatic(convertedSolutionExas, "Add", key, value);
        }

        object inner = CallStatic(
            "#=qCZfLbsGT8TxGPNVcp52ThA==",
            puzzle.Inner,
            testRunNumber,
            team,
            convertedSolutionExas
        )!;
        return new Simulation(inner);
    }

    public void Step()
    {
        _ = InnerStep(Inner);
    }

    public static int CountCodeSize([NotNull] Code code)
    {
        return (int)CallStatic("#=q$bzjuqpJ4$1ZnJopcCnGHikwx30NyLQHmmR$fALl5MA=", code.Inner)!;
    }

    [MethodWrapper("#=q9jlSbij7xzD7a5JTreHwgSlOVHw2c6NutHpXBargYEs=")]
    private static bool InnerGetCompleted(object _)
    {
        return (bool)MethodWrapperAttribute.Stub();
    }

    [MethodWrapper("#=qajowlg6dle2da5dexjWRrQ==")]
    private static object InnerStep(object _)
    {
        return MethodWrapperAttribute.Stub();
    }

    public static void PatchPuzzleCompleteCheckPrefix(Func<Simulation, bool>? prefix)
    {
        static bool prefixWrapper(object __instance)
        {
            return currentPuzzleCompleteCheckPrefix!.Invoke(new(__instance));
        }

        // undo any previous patches made by this Harmony instance
        harmony.UnpatchAll(harmony.Id);

        if (prefix is not null)
        {
            currentPuzzleCompleteCheckPrefix = prefix;

            _ = harmony.Patch(
                WrappedType.GetMethod(
                    "#=q9xYcWbKgLb2qX8RtRXp5R7g6WnxOMCTSrr67Ge4e1ug=",
                    BindingFlags.NonPublic | BindingFlags.Instance
                ),
                new HarmonyMethod(prefixWrapper)
            );
        }
    }
}
