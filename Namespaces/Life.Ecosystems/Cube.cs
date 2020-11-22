using Life.Generic;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Life.Ecosystems
{
    public class Cube
    {
        public GeometryModel3D AnimalModel;

        public Cube(Point3D location = new Point3D())
        {
            MeshGeometry3D model = FileConversion.TxtToMesh(@"AnimalModels/Cube.txt");
            AnimalModel = new GeometryModel3D()
            {
                Geometry = model,
                Material = new DiffuseMaterial(new SolidColorBrush(Colors.Green)),
            };

            TranslateTransform3D position = new TranslateTransform3D()
            {
                OffsetX = location.X,
                OffsetY = location.Y,
                OffsetZ = location.Z
            };
            AnimalModel.Transform = position;
        }
    }
}
