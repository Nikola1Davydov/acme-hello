using AnalyseTool.Sdk;

namespace Acme.Hello;

[RevitCommand(Description = "Returns the active document's title.", ReadOnly = true)]
internal sealed class Hello : IRevitTask
{
    public async Task<object?> ExecuteAsync(IRevitContext ctx, CancellationToken ct)
    {
        var name = await ctx.RunInRevitAsync<string?>(app =>
            app.ActiveUIDocument?.Document.Title ?? "(no active document)");
        return new { message = $"Hello from v1.0.0! Document: {name}" };
    }
}
