

namespace ZaephusEngine {

    public class Primitives {

        public static Mesh Cube {
            get {
                Mesh cube = new Mesh();

                cube.vertices = new Vector3[] {
                    new Vector3(0.5f, -0.5f, -0.5f),
                    new Vector3(0.5f, -0.5f, 0.5f),
                    new Vector3(-0.5f, -0.5f, 0.5f),
                    new Vector3(-0.5f, -0.5f, -0.5f),

                    new Vector3(0.5f, 0.5f, -0.5f),
                    new Vector3(0.5f, 0.5f, 0.5f),

                    new Vector3(0.5f, 0.5f, 0.5f),
                    new Vector3(-0.5f, 0.5f, 0.5f),
                    
                    new Vector3(-0.5f, 0.5f, 0.5f),
                    new Vector3(-0.5f, 0.5f, -0.5f),

                    new Vector3(-0.5f, 0.5f, -0.5f),
                    new Vector3(0.5f, 0.5f, -0.5f),

                    new Vector3(-0.5f, 0.5f, -0.5f),
                    new Vector3(-0.5f, 0.5f, 0.5f),

                    new Vector3(0.5f, -0.5f, 0.5f),
                    new Vector3(-0.5f, -0.5f, 0.5f),

                    new Vector3(-0.5f, -0.5f, 0.5f),
                    new Vector3(-0.5f, -0.5f, -0.5f),

                    new Vector3(-0.5f, -0.5f, -0.5f),
                    new Vector3(0.5f, -0.5f, -0.5f),

                    new Vector3(0.5f, -0.5f, -0.5f),
                    new Vector3(0.5f, -0.5f, 0.5f),

                    new Vector3(0.5f, 0.5f, -0.5f),
                    new Vector3(0.5f, 0.5f, 0.5f)
                };
                
                cube.uvs = new Vector2[] {
                    new Vector2(1.0f, 0.0f),
                    new Vector2(1.0f, 1.0f),
                    new Vector2(0.0f, 1.0f),
                    new Vector2(0.0f, 0.0f),

                    new Vector2(2.0f, 0.0f),
                    new Vector2(2.0f, 1.0f),

                    new Vector2(1.0f, 2.0f),
                    new Vector2(0.0f, 2.0f),

                    new Vector2(-1.0f, 1.0f),
                    new Vector2(-1.0f, 0.0f),

                    new Vector2(0.0f, -1.0f),
                    new Vector2(1.0f, -1.0f),

                    new Vector2(3.0f, 0.0f),
                    new Vector2(3.0f, 1.0f),

                    new Vector2(1.0f, 1.0f),
                    new Vector2(0.0f, 1.0f),

                    new Vector2(0.0f, 1.0f),
                    new Vector2(0.0f, 0.0f),

                    new Vector2(0.0f, 0.0f),
                    new Vector2(1.0f, 0.0f),

                    new Vector2(1.0f, 0.0f),
                    new Vector2(1.0f, 1.0f),

                    new Vector2(2.0f, 0.0f),
                    new Vector2(2.0f, 1.0f)
                };

                cube.normals = new Vector3[] {
                    new Vector3(0.0f, -1.0f, 0.0f),
                    new Vector3(0.0f, -1.0f, 0.0f),
                    new Vector3(0.0f, -1.0f, 0.0f),
                    new Vector3(0.0f, -1.0f, 0.0f),

                    new Vector3(1.0f, 0.0f, 0.0f),
                    new Vector3(1.0f, 0.0f, 0.0f),

                    new Vector3(0.0f, 0.0f, 1.0f),
                    new Vector3(0.0f, 0.0f, 1.0f),

                    new Vector3(-1.0f, 0.0f, 0.0f),
                    new Vector3(-1.0f, 0.0f, 0.0f),

                    new Vector3(0.0f, 0.0f, -1.0f),
                    new Vector3(0.0f, 0.0f, -1.0f),

                    new Vector3(0.0f, 1.0f, 0.0f),
                    new Vector3(0.0f, 1.0f, 0.0f),

                    new Vector3(0.0f, 0.0f, 1.0f),
                    new Vector3(0.0f, 0.0f, 1.0f),

                    new Vector3(-1.0f, 0.0f, 0.0f),
                    new Vector3(-1.0f, 0.0f, 0.0f),

                    new Vector3(0.0f, 0.0f, -1.0f),
                    new Vector3(0.0f, 0.0f, -1.0f),

                    new Vector3(1.0f, 0.0f, 0.0f),
                    new Vector3(1.0f, 0.0f, 0.0f),

                    new Vector3(0.0f, 1.0f, 0.0f),
                    new Vector3(0.0f, 1.0f, 0.0f)
                };

                cube.triangles = new uint[] {
                    // Down
                    0, 1, 2,
                    0, 2, 3,

                    // Back
                    14, 6, 7,
                    14, 7, 15,

                    // Right
                    20, 4, 5,
                    20, 5, 21,

                    // Left
                    16, 8, 9,
                    16, 9, 17,

                    // Front
                    18, 10, 11,
                    18, 11, 19,

                    // Up
                    22, 12, 13,
                    22, 13, 23
                };

                return cube;
            }
        }

    }

}