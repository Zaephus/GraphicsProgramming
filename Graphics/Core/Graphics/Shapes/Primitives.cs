

namespace ZaephusEngine {

    public class Primitives {

        public static Mesh Cube {
            get {
                return new Mesh {
                    vertices = new Vector3[] {
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
                    },

                    uvs = new Vector2[] {
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
                    },

                    normals = new Vector3[] {
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
                    },

                    triangles = new uint[] {
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
                    }
                };
            }
        }

        public static Mesh Sphere {
            get {
                Mesh sphere = new Mesh();

                uint faceResolution = 2;

                List<Vector3> vertices = new();
                List<uint> triangles = new();
                List<Vector2> uvs = new();

                for(int x = 0; x < faceResolution; x++) {
                    for(int y = 0; y < faceResolution; y++) {
                        for(int z = 0; z < faceResolution; z++) {

                            if(z > 0 && z < faceResolution-1) {
                                if(y > 0 && y < faceResolution-1) {
                                    continue;
                                }
                            }

                            vertices.Add(new Vector3(x, y, z));
                            uvs.Add(new Vector2(x, y));

                            uint i = (uint)vertices.Count - 1;

                            triangles.Add(i);
                            triangles.Add(i + 1);
                            triangles.Add(i + faceResolution + 1);

                            triangles.Add(i);
                            triangles.Add(i + faceResolution + 1);
                            triangles.Add(i + faceResolution);

                        }
                    }
                }

                sphere.vertices = vertices.ToArray();
                sphere.uvs = uvs.ToArray();
                sphere.triangles = triangles.ToArray();

                // sphere.RecalculateNormals();

                return sphere;
            }
        }

    }

}