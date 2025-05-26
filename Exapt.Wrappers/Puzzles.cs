// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;
using Exapt.Wrappers.Meta;

namespace Exapt.Wrappers;

[TypeWrapper("Puzzles")]
public class Puzzles : StaticWrapper<Puzzles>
{
    private static bool initialized = false;

    public static void Initialize()
    {
        if (!initialized)
        {
            _ = CallStatic("#=qmvy28H$Ax5rPhkJqrGFbUg==");
            initialized = true;
        }
    }

    public static Puzzle FromId([NotNull] PuzzleId puzzleId)
    {
        return new Puzzle(CallStatic("#=q4YjDHfJYS2sN7OymGOsg4Q==", puzzleId.Inner)!);
    }
}
