
using System.IO;
using Assimp;
using Assimp.Configs;
using System.Linq;

namespace ZaephusEngine {

    public static class FileLoader {

        public static (Mesh, Material) LoadModel(string _path) {

            AssimpContext importer = new AssimpContext();
            importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));

            LogStream logStream = new LogStream(delegate(String _message, String _userData) {
                Console.WriteLine(_message);
            });
            logStream.Attach();

            Scene model = importer.ImportFile(_path, PostProcessPreset.TargetRealTimeMaximumQuality);

            if(model.MeshCount <= 0) {
                Console.WriteLine("Model did not contain a mesh.");
                return (null, null);
            }

            // TODO: Add support for loading a model with multiple meshes.
            Assimp.Mesh loadedMesh = model.Meshes[0];            
            Mesh mesh = new Mesh {
                vertices = Array.ConvertAll(loadedMesh.Vertices.ToArray(), item => (Vector3)item),
                vertexColours = Array.ConvertAll(loadedMesh.VertexColorChannels[0].ToArray(), item => (Colour)item),
                uvs = Array.ConvertAll(loadedMesh.TextureCoordinateChannels[0].ToArray(), item => (Vector2)(Vector3)item),
                normals = Array.ConvertAll(loadedMesh.Normals.ToArray(), item => (Vector3)item),
                tangents = Array.ConvertAll(loadedMesh.Tangents.ToArray(), item => (Vector3)item),
                biTangents = Array.ConvertAll(loadedMesh.BiTangents.ToArray(), item => (Vector3)item),

                triangles = Array.ConvertAll(loadedMesh.GetIndices(), item => (uint)item)
            };

            // TODO: Load material.

            if(model.MaterialCount <= 0) {
                Console.WriteLine("Model did not contain a material.");
                return (mesh, new Material());
            }

            // TODO: Fix ambient strength.
            // TODO: Load all other textures.
            // TODO: Add support for embedded textures.
            Assimp.Material loadedMat = model.Materials[0];
            Material mat = new Material {
                ObjectColour = loadedMat.ColorDiffuse,
                AmbientStrength = (loadedMat.ColorAmbient.R + loadedMat.ColorAmbient.G + loadedMat.ColorAmbient.B) / 3,
                SpecularStrength = (loadedMat.ColorSpecular.R + loadedMat.ColorSpecular.G + loadedMat.ColorSpecular.B) / 3,
                Shininess = loadedMat.Shininess
            };

            if(loadedMat.TextureDiffuse.FilePath != null) {
                string diffusePath = Path.Combine(new FileInfo(_path).Directory.FullName, loadedMat.TextureDiffuse.FilePath);
                if(File.Exists(diffusePath)) {
                    mat.DiffuseMap = new Texture2D(diffusePath);
                    // TODO: Object colour with diffuse texture.
                    mat.ObjectColour = Colour.white;
                }
            }
            if(loadedMat.TextureSpecular.FilePath != null) {
                string specularPath = Path.Combine(new FileInfo(_path).Directory.FullName, loadedMat.TextureSpecular.FilePath);
                if(File.Exists(specularPath)) {
                    mat.SpecularMap = new Texture2D(specularPath);
                }
            }

            return (mesh, mat);
            
        }

    }

}