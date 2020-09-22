using GraphControlCore.Interfaces.Services;
using GraphControlCore.Interfaces;
using GraphControlCore.Utilities;
using GraphControlWinFormsTestApp.Interfaces;
using GraphControlWinFormsTestApp.Presenters;
using GraphControlWinFormsTestApp.Services;
using System;
using System.Windows.Forms;

namespace GraphControlWinFormsTestApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new GraphControlTestForm();

            // Register dependencies
            var controller = new ApplicationController(new DependInjectWrapper());
            controller.RegisterInstance<IApplicationController>(controller)
                .RegisterInstance<IDataProviderService>(new SinusDataProviderService())
                .RegisterInstance<IGraphControlTestFormView>(form)
                .RegisterInstance(new ApplicationContext());

            // Parse parameters
            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    var param = arg.Split('=');
                    if (param.Length == 2)
                    {
                        if (param[0].Equals("testPoints", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (UInt32.TryParse(param[1], out uint testPoints))
                            {
                                controller.GetInstance<IDataProviderService>().TestPoints = testPoints;
                            }
                        }
                    }
                }
            }

            controller.Run<GraphControlTestFormPresenter>();

            Application.Run(form);
        }


    }
}
