// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Exapt.Wrappers.Meta;

namespace Exapt.Wrappers;

internal sealed class MaybeSolution : NonStaticWrapper<MaybeSolution>
{
    internal MaybeSolution(object inner)
        : base(inner) { }

    public bool IsSome()
    {
        return (bool)Call("#=qgQ5O1bL8ilWB5y5e4ATciQ==")!;
    }

    public object Unwrap()
    {
        return Call("#=qmoDLSpl1snp9jPcvZqncJg==")!;
    }
}
