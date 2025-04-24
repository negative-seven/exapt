// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using CommandLine;
using CommandLine.Text;
using Newtonsoft.Json;

namespace Exapt;

public static class Program
{
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
    private sealed class Arguments
    {
        [Value(
            0,
            MetaName = "solution-file",
            HelpText = "The path to the solution file to be validated.",
            Required = true
        )]
        public required string SolutionFilepath { get; set; }

        [Option(
            'e',
            "exapunks-directory",
            HelpText = "A path to the root directory of the 2022-10-13 Windows version of Exapunks",
            Required = true
        )]
        public required string ExapunksDirectory { get; set; }
    }
#pragma warning restore CA1812 // Avoid uninstantiated internal classes

    private static void Main(string[] args)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        Parser parser = new(p => p.HelpWriter = null);
        ParserResult<Arguments> parserResult = parser.ParseArguments<Arguments>(args);
        _ = parserResult
            .WithParsed(InnerMain)
            .WithNotParsed(_ =>
                Console.Error.Write(
                    HelpText.AutoBuild(
                        parserResult,
                        h =>
                        {
                            h.AdditionalNewLineAfterOption = false;
                            return h;
                        }
                    )
                )
            );
        parser.Dispose();
    }

    private static void InnerMain(Arguments arguments)
    {
        Initialize(arguments.ExapunksDirectory);

        SolutionData result = Simulate(arguments.SolutionFilepath);
        Console.WriteLine(JsonConvert.SerializeObject(result));
    }

    public static void Initialize(string exapunksDirectory)
    {
        exapunksDirectory = Path.GetFullPath(exapunksDirectory);

        AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
        {
            string? assemblyFileName = args.Name.Split(',')[0] switch
            {
                "Burbank" => "EXAPUNKS.exe",
                "Steamworks.NET" => "Steamworks.NET.dll",
                _ => null,
            };
            return assemblyFileName is not null
                ? Assembly.LoadFile(Path.Join(exapunksDirectory, assemblyFileName))
                : null;
        };

        Wrappers.Meta.Globals.ExapunksDirectory = exapunksDirectory;
        Wrappers.Globals.SetRandom(new Wrappers.Random(1));
        Wrappers.Strings.Initialize();
        Wrappers.Puzzles.Initialize();
        Wrappers.GameLogic.Instance = new Wrappers.GameLogic();
        try
        {
            Wrappers.Renderer.Initialize(Wrappers.RendererType.Direct3D, false);
        }
        catch (DllNotFoundException)
        {
            // loading D3D failed, try OpenGL instead
            Wrappers.Renderer.Initialize(Wrappers.RendererType.OpenGl, false);
            // note, this window won't actually be displayed, as the HIDDEN flag is
            // passed to SDL_WindowCreate by default
            Wrappers.GameLogic.Instance.CreateWindow("exapt", 640, 480, 0);
        }
        Wrappers.GameLogic.Instance.InitializeFontsA(() => { });
        Wrappers.GameLogic.Instance.InitializeFontsB();

        // patch out steam calls (this loads the classes from SteamPatch.cs)
        HarmonyLib.Harmony harmony = new(nameof(Program));
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    public static SolutionData Simulate(string solutionFile)
    {
        Solution solution = new(solutionFile);

        Simulation.UseOptimizationsForPuzzle(solution.PuzzleId);

        bool failed = false;
        int worstCycles = 0;
        int? codeSize = null;
        int worstActivity = 0;
        for (int testIndex = 0; testIndex < 100; testIndex++)
        {
            Simulation simulation = solution.CreateSimulation(testIndex);
            codeSize ??= simulation.CodeSize;

            for (int i = 0; i < 999999 && !simulation.Completed; i++)
            {
                simulation.Step();
            }

            if (simulation.Completed)
            {
                worstCycles = Math.Max(worstCycles, simulation.Cycles);
                worstActivity = Math.Max(worstActivity, simulation.Activity);
            }
            else
            {
                failed = true;
                break;
            }
        }
        return new SolutionData()
        {
            PuzzleId = solution.PuzzleId,
            Statistics = failed
                ? null
                : new SolutionStatistics
                {
                    Cycles = worstCycles,
                    Size = codeSize ?? throw new UnreachableException(),
                    Activity = worstActivity,
                },
        };
    }
}
