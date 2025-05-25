// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics.CodeAnalysis;
using Exapt.Wrappers.Meta;

namespace Exapt.Wrappers;

[ClassWrapper("#=q$O9G2KbklYQ6FwBlZ$EShQ==")]
public class Globals : StaticWrapper<Globals>
{
    public static void SetRandom([NotNull] Random random)
    {
        SetStatic("#=qyD9BljpzrnFagKpRdzAf5A==", random.Inner);
    }
}
