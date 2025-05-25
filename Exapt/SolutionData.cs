// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Exapt;

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public record SolutionData : IEquatable<SolutionData>
{
    public required string PuzzleId { get; set; }

    public bool Completed => Statistics is not null;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public SolutionStatistics? Statistics { get; set; }
}

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public record SolutionStatistics
{
    public required int Cycles { get; set; }

    public required int Size { get; set; }

    public required int Activity { get; set; }
}
