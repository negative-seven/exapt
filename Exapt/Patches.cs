// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace Exapt;

// Steam achievement calls are used inside level code
[HarmonyPatch]
internal static class PatchSteamUserStats
{
    [HarmonyPatch("Steamworks.SteamUserStats, Steamworks.NET", "SetAchievement")]
    [HarmonyPrefix]
    public static bool SetAchievementPrefix()
    {
        return false;
    }

    [HarmonyPatch("Steamworks.SteamUserStats, Steamworks.NET", "StoreStats")]
    [HarmonyPrefix]
    public static bool StoreStatsPrefix()
    {
        return false;
    }
}

// Disable the other Steam calls by overwriting a build-time constant
[HarmonyPatch]
internal static class PatchReadonlyGlobals
{
    public static IEnumerable<MethodBase> TargetMethods()
    {
        MethodBase? method = Type.GetType("#=qD_usaxHu0XL9_OQn7XltrQ==, Burbank")
            ?.GetConstructor(BindingFlags.NonPublic | BindingFlags.Static, []);
        if (method is not null)
        {
            yield return method;
        }
    }

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> codes = new(instructions);
        FieldInfo? targetField = Type.GetType("#=qD_usaxHu0XL9_OQn7XltrQ==, Burbank")!
            .GetField("#=q2OFH6JrUOETgdwoCqXZcwfL0QOKQKOpwpwsnPngDHwo=");
        ArgumentNullException.ThrowIfNull(targetField);
        for (int i = 0; i + 1 < codes.Count; i++)
        {
            if (codes[i].opcode == OpCodes.Ldc_I4_1 && codes[i + 1].StoresField(targetField))
            {
                codes[i].opcode = OpCodes.Ldc_I4_0;
            }
        }

        return codes.AsEnumerable();
    }
}

// Skip user input checks in sandbox
[HarmonyPatch("SpecialPuzzleLogics+#=qTeI8TrGQjU99Ov7BJNVX1w==", "#=qMcivoG7Mpioeq5qRq72aCA==")]
internal static class PatchSandboxKeyPressed
{
    public static bool Prefix(ref bool __result)
    {
        __result = false;
        return false;
    }
}
