

namespace ZaephusEngine {

    public static class Primitives {

        static Primitives() {
            capsule  = Model.Load("Core\\Graphics\\Primitives\\capsule.obj", false).meshes[0];
            cube     = Model.Load("Core\\Graphics\\Primitives\\cube.obj", false).meshes[0];
            cylinder = Model.Load("Core\\Graphics\\Primitives\\cylinder.obj", false).meshes[0];
            quad     = Model.Load("Core\\Graphics\\Primitives\\quad.obj", false).meshes[0];
            sphere   = Model.Load("Core\\Graphics\\Primitives\\sphere.obj", false).meshes[0];
        }

        public static readonly Mesh capsule;
        public static readonly Mesh cube;
        public static readonly Mesh cylinder;
        public static readonly Mesh quad;
        public static readonly Mesh sphere;

    }

}