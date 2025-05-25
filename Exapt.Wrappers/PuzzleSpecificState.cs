// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Collections.ObjectModel;
using Exapt.Wrappers.Meta;

namespace Exapt.Wrappers;

[ClassWrapper("#=qwCVetOJ8d2dzLv7iF5FKO0$IyhevGyzpxoCo3cuHWMg=")]
public class PuzzleSpecificState : NonStaticWrapper<PuzzleSpecificState>
{
    internal PuzzleSpecificState(object inner)
        : base(inner) { }

    public LeftArmSpecificState? AsLeftArmSpecificState()
    {
        return Inner.GetType().Name == LeftArmSpecificState.WrappedType.Name ? new LeftArmSpecificState(Inner) : null;
    }

    public SawayamaWonderdiscSpecialState? AsSawayamaWonderdiscSpecificState()
    {
        return Inner.GetType().Name == SawayamaWonderdiscSpecialState.WrappedType.Name
            ? new SawayamaWonderdiscSpecialState(Inner)
            : null;
    }
}

[ClassWrapper("SpecialPuzzleLogics+#=q1jrxtBuiTtOGmDnxQ5uXzg==")]
public class LeftArmSpecificState : NonStaticWrapper<LeftArmSpecificState>
{
    public IEnumerable<object> OutputValues =>
        ((System.Collections.IEnumerable)Get("#=qB3Tsmhkf9HlKMfPEPfCTpQ==")!).Cast<object>();

    internal LeftArmSpecificState(object inner)
        : base(inner) { }
}

[ClassWrapper("SpecialPuzzleLogics+ArcadeGet")]
public class SawayamaWonderdiscSpecialState : NonStaticWrapper<SawayamaWonderdiscSpecialState>
{
    public ReadOnlyCollection<bool> OutputMatches => ((List<bool>)Get("#=qUa63$IPg1OR89eclcOlolw==")!).AsReadOnly();

    internal SawayamaWonderdiscSpecialState(object inner)
        : base(inner) { }
}
