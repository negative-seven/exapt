// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;

namespace Exapt.Wrappers;

public class Puzzles : StaticWrapper<Puzzles>
{
    private static bool initialized = false;

    static Puzzles()
    {
        SetWrappedType("Puzzles");
    }

    public static void Initialize(string exapunksDirectory)
    {
        if (!initialized)
        {
            Utils.WithWorkingDirectory(exapunksDirectory, () => CallStatic("#=qmvy28H$Ax5rPhkJqrGFbUg=="));
            initialized = true;
        }
    }

    public static Puzzle FromId([NotNull] PuzzleId puzzleId)
    {
        return new Puzzle(CallStatic("#=q4YjDHfJYS2sN7OymGOsg4Q==", puzzleId.Inner)!);
    }
}
