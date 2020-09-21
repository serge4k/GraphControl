using System;
using System.Windows.Forms;
using GraphControl.Definitions;
using GraphControl.Interfaces;
using GraphControl.Utilites;
using GraphControlWinFormsTestApp.Interfaces;

namespace GraphControlWinFormsTestApp
{
    public partial class GraphControlTestForm : Form, IGraphControlTestFormView
    {
        private readonly GraphControlWinForms.GraphControl graphControlWinForms;

        public GraphControlTestForm()
        {
            InitializeComponent();

            this.graphControlWinForms = new GraphControlWinForms.GraphControl();
            this.graphControlWinForms.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            // Configure formatters
            var dtFormatter = new DateTimeValueFormatter("{0:HH:mm:ss.f\nyyyy.MM.dd}", TimeSpan.TicksPerMillisecond);
            dtFormatter.AddFormat(1000, "{0:HH:mm:ss.fff\nyyyy.MM.dd}");
            dtFormatter.AddStepFormat(1000, "{0:G} ms", 1);
            dtFormatter.AddStepFormat(Double.MaxValue, "{0:G} s", 1000);
            this.graphControlWinForms.ItemFormatter.Register(Axis.X, dtFormatter);
            this.graphControlWinForms.ItemFormatter.Register(Axis.Y, new DoubleValueFormatter("0.######"));

            // Configure margin
            this.graphControlWinForms.GraphPadding = new Padding(90, 10, 10, 50);

            Controls.Add(this.graphControlWinForms);
        }

        public void RegisterDataProvider(IGraphDataProvider dataProvider)
        {
            this.graphControlWinForms.RegisterDataProvider(dataProvider);
        }

        public void ShowView()
        {
        }

        public void RefreshView()
        {
        }

        private void GraphControlForm_Load(object sender, System.EventArgs e)
        {
            this.graphControlWinForms.SetBounds(0, 0, ClientSize.Width, ClientSize.Height, BoundsSpecified.Size);
        }
    }
}
