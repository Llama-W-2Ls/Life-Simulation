namespace Life.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Media3D;
    using Life.Ecosystems;

    public static class Simulation
    {
        private readonly static Main main = new Main();
        private static Model3DGroup group = main.ModelGroup;
        public static List<GeometryModel3D> Models = new List<GeometryModel3D>();
        public static Random random = new Random();

        #region Add Functions

        public static void Add(Bunny bunny)
        {
            group.Children.Add(bunny.AnimalModel);
            bunny.Live();
        }

        public static void Add(Terrain terrain)
        {
            foreach (GeometryModel3D item in terrain.ModelGroup)
            {
                group.Children.Add(item);
            }
        }

        public static void Add(Berry berry)
        {
            group.Children.Add(berry.AnimalModel);
        }

        #endregion

        #region Remove Functions

        public static void Remove(Bunny bunny)
        {
            group.Children.Remove(bunny.AnimalModel);
            bunny.Alive = false;
        }

        public static void Remove(Terrain terrain)
        {
            foreach (GeometryModel3D item in terrain.ModelGroup)
            {
                group.Children.Remove(item);
            }
        }

        #endregion Remove

        #region Display Functions

        /// <summary>
        /// Changes MainWindow into a Simulation Window:
        /// <para>- Adds a 3D Viewport with a movable camera:</para>
        /// <para> - Can pan around using MiddleMouseButton + Left CTRL</para>
        /// <para> - Can look around using MiddleMouseButton</para>
        /// <para> - Can zoom out using scroll wheel</para>
        /// <para>- Viewport displays all added objects onto MainWindow</para>
        /// </summary>
        public static void Display(Window Main)
        {
            Main.Content = main.Content;
            Main.Closed += Main_Closed;
            Main.WindowState = WindowState.Maximized;
        }

        #endregion

        private static void Main_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

    public partial class Main : Window
    {
        public Model3DGroup ModelGroup = new Model3DGroup();

        public Main()
        {
            Height = SystemParameters.PrimaryScreenHeight;
            Width = SystemParameters.PrimaryScreenWidth;
            WindowState = WindowState.Maximized;
            Title = "MySimulation";

            #region Initialising 3D Scene Objects

            Viewport3D Viewport = new Viewport3D();
            ModelVisual3D ModelVisual = new ModelVisual3D();

            #endregion

            #region Camera Stuff

            PerspectiveCamera MainCamera = new PerspectiveCamera
            {
                Position = new Point3D(0, 0, 5),
                FieldOfView = 60,
            };
            Border border = new Border()
            {
                Width = Width,
                Height = Height,
                Background = new SolidColorBrush(Colors.Aquamarine)
            };
            CameraPan cameraPan = new CameraPan(MainCamera, border);

            Viewport.Camera = MainCamera;

            #endregion

            #region Directional Lighting

            AmbientLight light = new AmbientLight
            {
                Color = Colors.Gray,
            };

            DirectionalLight ambientLight = new DirectionalLight
            {
                Color = Colors.White,
                Direction = new Vector3D(1, -1, -1),
            };

            ModelGroup.Children.Add(ambientLight);
            ModelGroup.Children.Add(light);

            #endregion

            #region Adding Children To Groups And Parents

            ModelVisual.Content = ModelGroup;
            Viewport.Children.Add(ModelVisual);
            border.Child = Viewport;

            #endregion

            Content = border;
        }
    }
}
