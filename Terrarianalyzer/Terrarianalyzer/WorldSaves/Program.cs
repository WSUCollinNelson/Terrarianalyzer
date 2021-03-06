using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terrarianalyzer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Forms setup
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Setup dependancy injection
            using (WindsorContainer container = new WindsorContainer())
            {
                // Create new world object and pass it into the application as it runs
                container.Register(Component.For<IProcessWorldSaves>().ImplementedBy<WorldSavesProcessor>());
                WorldObject world = container.Resolve<IProcessWorldSaves>().GetWorldObject();

                Application.Run(new Terrarianalyzer(world));
            }
        }
    }
}
