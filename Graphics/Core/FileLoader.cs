
using Assimp;
using Assimp.Configs;
using System.Linq;

namespace ZaephusEngine {

    public static class FileLoader {

        public static Mesh LoadModel(string _path) {

            AssimpContext importer = new AssimpContext();
            importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));

            LogStream logStream = new LogStream(delegate(String _message, String _userData) {
                Console.WriteLine(_message);
            });
            logStream.Attach();

            Scene model = importer.ImportFile(_path, PostProcessPreset.TargetRealTimeMaximumQuality);

            if(model.MeshCount <= 0) {
                Console.WriteLine("Model did not contain a mesh");
                return null;
            }

            Assimp.Mesh loadedMesh = model.Meshes[0];            
            Mesh mesh = new Mesh();
            
            mesh.vertices = Array.ConvertAll(loadedMesh.Vertices.ToArray(), item => (Vector3)item);
            mesh.vertexColours = Array.ConvertAll(loadedMesh.VertexColorChannels[0].ToArray(), item => (Colour)item);
            mesh.uvs = Array.ConvertAll(loadedMesh.TextureCoordinateChannels[0].ToArray(), item => (Vector2)(Vector3)item);
            mesh.normals = Array.ConvertAll(loadedMesh.Normals.ToArray(), item => (Vector3)item);
            mesh.tangents = Array.ConvertAll(loadedMesh.Tangents.ToArray(), item => (Vector3)item);
            mesh.biTangents = Array.ConvertAll(loadedMesh.BiTangents.ToArray(), item => (Vector3)item);

            mesh.triangles = Array.ConvertAll(loadedMesh.GetIndices(), item => (uint)item);

            // TODO: Load material.

            return mesh;
            
        }

    }

}