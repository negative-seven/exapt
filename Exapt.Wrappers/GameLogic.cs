// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;

namespace Exapt.Wrappers;

public class GameLogic : Meta.NonStaticWrapper<GameLogic>
{
    [DisallowNull]
    public static GameLogic Instance
    {
        get => new(GetStatic("#=qEACwyfrjJQwoldN_MUIRWw==")!);
        set =>
            SetStatic("#=qEACwyfrjJQwoldN_MUIRWw==", (value ?? throw new ArgumentNullException(nameof(value))).Inner);
    }

    static GameLogic()
    {
        SetWrappedType("GameLogic");
    }

    public GameLogic(object inner)
        : base(inner) { }

    public GameLogic()
        : base(CallConstructor()!) { }

    public void InitializeFontsA(Action fontLoadedCallback)
    {
        _ = Call("#=qwTm7q5Z8yZyRE$EA_wW4DQ==", fontLoadedCallback);
    }

    public void InitializeFontsB()
    {
        _ = Call("#=qrnUck7W$Xr7MBLj7W0QtK28NCH7Qgj1N5e$igVDhSzo=");
    }
}
