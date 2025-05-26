// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Exapt.Wrappers.Meta;

namespace Exapt.Wrappers;

[TypeWrapper("Solution")]
public class Solution : NonStaticWrapper<Solution>
{
    public PuzzleId PuzzleId => new(Call("#=q0bXK6vTpnQXmi0XWqqn0yA==")!);

    public IEnumerable<SolutionExa> SolutionExas
    {
        get
        {
            IEnumerable<object> inners = Enumerable.Cast<object>(
                (System.Collections.IEnumerable)Get("#=qzJMkmdeH1slKh8PKZTwTEA==")!
            );
            return inners.Select(e => new SolutionExa(e));
        }
    }

    internal Solution(object inner)
        : base(inner) { }

    public static Solution? FromFile(string filename)
    {
        Maybe solution = new(CallStatic("#=qrAtvddUJvCjJyuYaeXTtoA==", filename)!);
        return solution.IsSome() ? new Solution(solution.Unwrap()) : null;
    }
}
