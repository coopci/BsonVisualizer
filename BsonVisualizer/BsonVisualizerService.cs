using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BsonVisualizer
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Reflection.Emit;

    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Debugger.Evaluation;
    using Microsoft.VisualStudio.Debugger.Interop;
    using EnvDTE;
    using System.Windows.Interop;

    internal class BsonVisualizerService : IBsonVisualizerService, IVsCppDebugUIVisualizer
    {/// <inheritdoc />
        int IVsCppDebugUIVisualizer.DisplayValue(uint ownerHwnd, uint visualizerId, IDebugProperty3 debugProperty)
        {
            System.Console.Out.WriteLine("enter BsonVisualizerService.DisplayValue");
            
            try
            {
                var dkmEvalResult = DkmSuccessEvaluationResult.ExtractFromProperty(debugProperty);

                //using (var viewModel = new VisualizerWindowViewModel(this.modBuilder))
                // {
                //    viewModel.VisualizeSqliteInstance(dkmEvalResult);
                //}

                


                // Invoke plotter window to show vector contents
                BsonVisualizerWindow plotterWindow = new BsonVisualizerWindow();

                plotterWindow.dkmEvalResult = dkmEvalResult;
                plotterWindow.Text = "Bson window";
                plotterWindow.ShowDialog();

                //WindowInteropHelper helper = new WindowInteropHelper(plotterWindow);
                //helper.Owner = (IntPtr)ownerHwnd;
                // plotterWindow.ShowModal(series);
                //plotterWindow.

            }
            catch (Exception e)
            {
                Debug.Fail("Visualization failed: " + e.Message);
                return e.HResult;
            }

            return VSConstants.S_OK;
        }

    }
}
