namespace Explicare
{
    [Command(PackageIds.MyCommand)]
    internal sealed class ExplicareToolWindowCommand : BaseCommand<ExplicareToolWindowCommand>
    {
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            return ExplicareToolWindow.ShowAsync();
        }
    }
}
