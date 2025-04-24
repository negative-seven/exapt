# Exapt

A CLI wrapper around [Exapunks](https://store.steampowered.com/app/716490/EXAPUNKS/) for validating solutions.

## Usage

Run Exapt from the root directory with the following command:

```sh
dotnet run --project Exapt -- -e <game directory> <solution file>
```

where `<game directory>` is the path to the root directory of the 2022-10-13 version of Exapunks. This version of the game can be obtained by running one of the following commands in the Steam console:

```text
download_depot 716490 716491 2461759427682173730 # Windows
download_depot 716490 716493 623155301854862726 # Linux
```
