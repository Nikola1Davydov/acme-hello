# acme.hello — test extension for the AnalyseTool publishing pipeline

## Local loop (no GitHub needed)

1. Put the AnalyseTool.Sdk nupkg into `packages\` (test phase only — not on nuget.org yet):

       dotnet pack <your-AnalyzeTool-clone>\src\AnalyseTool.Sdk\AnalyseTool.Sdk.csproj -c "Release R25" -p:GeneratePackageOnBuild=false -o packages

2. Button test (fastest): build and drop the folder into the dev zone

       dotnet build -c "Release R25"
       xcopy /y plugin.json "%LOCALAPPDATA%\AnalyseTool\extensions\acme.hello\"
       xcopy /y "bin\Release R25\net8.0-windows\Acme.Hello.dll" "%LOCALAPPDATA%\AnalyseTool\extensions\acme.hello\"

   Revit → AnalyseTool → Reload (new button needs one Revit restart the first time).

3. Package test: `dotnet build -t:PackExtension` → `artifacts\acme.hello-1.0.0.zip`
   → Settings → Extensions → Install from file…
   (delete the dev-zone folder from step 2 first — one id can only exist once).

## Publish loop

    git tag v1.0.0
    git push --tags

The workflow builds the zip for Revit 2025/2026/2027 and attaches it to the GitHub Release.

## Update loop

1. Bump `"version"` in plugin.json to `1.0.1` (and tweak Hello.cs so the change is visible).
2. Commit, push, `git tag v1.0.1 && git push --tags`.
3. In AnalyseTool: Settings → Extensions → Check updates → "→ 1.0.1" badge → Update.

Manifest version must match the tag (without the `v`).
