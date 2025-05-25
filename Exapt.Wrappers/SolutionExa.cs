// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Exapt.Wrappers.Meta;

namespace Exapt.Wrappers;

public class SolutionExa : NonStaticWrapper<SolutionExa>
{
    public Code Code
    {
        get
        {
            object a = Get("#=qspUergJPSYLfh2YdnSsRQ0oECwKCGjIWlINxFjteWhs=")!;
            object b = Utils.Get(a, "#=qm2WdvgSYgJdwJfJhUUVLTA==")!;
            object c = Utils.Get(b, "#=qxVFmzYr3PSpuzJKbb9hW3g==")!;
            return new Code(c);
        }
    }

    internal SolutionExa(object inner)
        : base(inner) { }
}
