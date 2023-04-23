using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE80;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Explicare
{
    public class ExplicareToolWindow : BaseToolWindow<ExplicareToolWindow>
    {
        public override string GetTitle(int toolWindowId) => "Explicare";

        public override Type PaneType => typeof(Pane);

        public override async Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {

           var dte = await Package.GetServiceAsync(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;

            return new ExplicareToolWindowControl(dte,Package);
        }

        [Guid("b8381bcb-59d1-4b5c-9951-fdbfb44990c9")]
        internal class Pane : ToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }
    }
}