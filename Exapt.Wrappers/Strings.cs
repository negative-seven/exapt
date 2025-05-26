// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Exapt.Wrappers.Meta;

namespace Exapt.Wrappers;

[TypeWrapper("#=q$q_5RIWNSfdbsTA5NIdJHA==")]
public class Strings : StaticWrapper<Strings>
{
    private static bool initialized = false;

    public static void Initialize()
    {
        if (!initialized)
        {
            _ = CallStatic("#=q2IcpkAiRPnhyxP4lHls68w==");
            initialized = true;
        }
    }
}
