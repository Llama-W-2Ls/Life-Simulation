using Life.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Life.Ecosystems
{
    public class Berry : Behaviour
    {
        public float FoodSource = 2;

        public GeometryModel3D AnimalModel;

        public Berry(int x = 0, int y = 0, int z = 0)
        {
            Point3D location = new Point3D(x, y, z);

            #region Model
            MeshGeometry3D model = FileConversion.TxtToMesh(@"AnimalModels/Berry.txt");
            AnimalModel = new GeometryModel3D()
            {
                Geometry = model,
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Red)),
            };
            #endregion

            #region Positioning and adjustments
            TranslateTransform3D position = new TranslateTransform3D()
            {
                OffsetX = location.X,
                OffsetY = location.Y,
                OffsetZ = location.Z
            };
            RotateTransform3D rotation = new RotateTransform3D(new AxisAngleRotation3D()
            {
                Axis = new Vector3D(0, 0, 0),
                Angle = 0
            });
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
                    rotation,
                    scale
                },
            };
            AnimalModel.Transform = Transforms;
            #endregion

            Walk(position, Simulation.random);
        }
    }
}
