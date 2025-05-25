// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Exapt;

public class SolutionFileLoadException : Exception
{
    public SolutionFileLoadException() { }

    public SolutionFileLoadException(string? message)
        : base(message) { }

    public SolutionFileLoadException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
