// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Exapt;

public class InvalidPreliminaryCheckException : Exception
{
    public InvalidPreliminaryCheckException() { }

    public InvalidPreliminaryCheckException(string message)
        : base(message) { }

    public InvalidPreliminaryCheckException(string message, Exception innerException)
        : base(message, innerException) { }
}
