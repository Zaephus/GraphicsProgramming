
using ZaephusEngine;

public class TerrainGenerator {

    public float baseY = 0.0f;
    public float amplitude = 3;

    public int octaves = 5;

    public float resolution = 100.0f;
    public Vector2Int size = new Vector2Int(400, 400);

    public Mesh Generate() {

        Mesh terrain = new Mesh {
            vertices = new Vector3[size.x * size.y],
            uvs = new Vector2[size.x * size.y],
            normals = new Vector3[size.x * size.y],
            triangles = new uint[((size.x-1) * (size.y-1)) * 6]
        };

        float[,] noise = GenerateNoiseMap(size.x, size.y, amplitude, octaves);

        int vertexIndex = 0;
        int triangleIndex = 0;
        for(int z = 0; z < size.x; z++) {
            for(int x = 0; x < size.y; x++) {

                terrain.vertices[vertexIndex] = new Vector3(x / resolution, baseY + noise[x, z], z / resolution);
                terrain.uvs[vertexIndex] = new Vector2(x, z);

                if(x < size.x-1 && z < size.y-1) {
                    terrain.triangles[triangleIndex]     = (uint)(vertexIndex);
                    terrain.triangles[triangleIndex + 1] = (uint)(vertexIndex + 1);
                    terrain.triangles[triangleIndex + 2] = (uint)(vertexIndex + size.x + 1);

                    terrain.triangles[triangleIndex + 3] = (uint)(vertexIndex);
                    terrain.triangles[triangleIndex + 4] = (uint)(vertexIndex + size.x + 1);
                    terrain.triangles[triangleIndex + 5] = (uint)(vertexIndex + size.x);
                    triangleIndex += 6;
                }

                vertexIndex++;

            }
        }

        terrain.RecalculateNormals();

        return terrain;

    }

    private float[,] GenerateNoiseMap(int _w, int _h, float _amplitude, int _octaves) {

        float[,] data = new float[_w,_h];

        float min = float.MaxValue;

        Noise2d.Reseed();

        float frequency = 0.5f;
        float amplitude = _amplitude;

        for(int i = 0; i < _octaves; i++) {
            for(int z = 0; z < _h; z++) {
                for(int x = 0; x < _w; x++) {

                    float noise = Noise2d.Noise(x * frequency / _w, z * frequency / _h);
                    noise = data[x, z] += noise * amplitude;

                    min = MathF.Min(min, noise);

                }
            }

            frequency *= 2;
            amplitude /= 2;
        }

        for(int x = 0; x < data.GetLength(0); x++) {
            for(int z = 0; z <  data.GetLength(1); z++) {
                data[x,z] -= min;
            }
        }

        return data;

    }
    
}