// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Exapt.Wrappers.Meta;

namespace Exapt.Wrappers;

[TypeWrapper("Renderer")]
public class Renderer : StaticWrapper<Renderer>
{
    public static void Initialize(RendererType type, bool debugMode)
    {
        _ = CallStatic("#=qjnHbEJCc8TIMoikd2FebJA==", type, debugMode);
    }
}
