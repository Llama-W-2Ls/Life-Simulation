using System.Windows;
using Life.Generic;
using Life.Ecosystems;

namespace Life
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                Bunny bunny = new Bunny()
                {
                    StarvationTime = 2,
                    Speed = 10,
                    LosesSpeedWhenHungry = true
                };
                Simulation.Add(bunny);
            }

            Terrain terrain = new Terrain();
            Simulation.Add(terrain);

            Simulation.Display(this);
        }
    }
}