namespace Life.Ecosystems
{
    using Life.Generic;
    using System;
    using System.Timers;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Media3D;

    public class Bunny : Behaviour
    {
        public GeometryModel3D AnimalModel;
        private TranslateTransform3D position;

        public float Food = 10;
        /// <summary>
        /// Time taken for food supply to drop by 1
        /// </summary>
        public int StarvationTime = 10;
        public bool IsLiving = false;

        public Bunny(int x = 0, int y = 0, int z = 0)
        {
            Point3D location = new Point3D(x, z, y);

            #region Model
            MeshGeometry3D model = FileConversion.TxtToMesh(@"AnimalModels/Rabbit.txt");
            AnimalModel = new GeometryModel3D()
            {
                Geometry = model,
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Orange)),
            };
            #endregion

            #region Positioning and adjustments
            position = new TranslateTransform3D()
            {
                OffsetX = location.X,
                OffsetY = location.Y,
                OffsetZ = location.Z
            };
            ScaleTransform3D scale = new ScaleTransform3D()
            {
                ScaleX = 0.1,
                ScaleY = 0.1,
                ScaleZ = 0.1
            };
            Transform3DGroup Transforms = new Transform3DGroup
            {
                Children = new Transform3DCollection()
                {
                    position,
                    scale
                },
            };
            AnimalModel.Transform = Transforms;
            #endregion
        }

        public void Live() // Starts simulating behaviour
        {
            if (!IsLiving)
            {
                Walk(position, Simulation.random);
                Hunger(this);
                IsLiving = true;
            }
        }
    }
}
