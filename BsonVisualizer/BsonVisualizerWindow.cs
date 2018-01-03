using Ejdb.BSON;
using Microsoft.VisualStudio.Debugger;
using Microsoft.VisualStudio.Debugger.Evaluation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BsonVisualizer
{
    public partial class BsonVisualizerWindow : Form
    {
        public DkmSuccessEvaluationResult dkmEvalResult { get; set; }

        public BsonVisualizerWindow()
        {
            InitializeComponent();
        }

        private void reload() {
            // this.dkmEvalResult.InspectionContext;
            txtOutput.Text = "";
            ulong address = this.dkmEvalResult.Address.Value;

            DkmProcess process = this.dkmEvalResult.InspectionContext.Thread.Process;
            byte[] buffer = new byte[1024];
            process.ReadMemory(
                address,
                DkmReadMemoryFlags.None,
                buffer
                );

            using (BSONIterator it = new BSONIterator(buffer))
            {
                while (it.Next() != BSONType.EOO)
                {
                    BSONValue value = it.FetchCurrentValue();
                    String s = value.ToString();

                    txtOutput.Text += s;
                    txtOutput.Text += "\n";
                    continue;
                }
            }

            return;
        }

        private void BsonVisualizerWindow_Load(object sender, EventArgs e)
        {
            this.reload();

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.reload();
        }
    }
}
