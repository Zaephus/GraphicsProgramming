
using Assimp;
using Assimp.Configs;

namespace ZaephusEngine {

    public class Model {

        public Mesh[] meshes = null;
        public Material[] materials = null;

        public Mesh mesh {
            get {
                if(meshes[0] == null) {
                    return null;
                }
                else {
                    return meshes[0];
                }
            }
        }

        public Material material {
            get {
                if(materials[0] == null) {
                    return null;
                }
                else {
                    return materials[0];
                }
            }
        }

        // TODO: Convert dictionary key from path to hash.
        private static Dictionary<string, Model> loadedModels = new();

        public Model() {}
        public Model(Mesh[] _meshes, Material[] _materials) {
            meshes = _meshes;
            materials = _materials;
        }

        public static Model Load(string _path, bool _saveModel = true) {

            if(loadedModels.ContainsKey(_path)) {
                return loadedModels[_path];
            }
            else {
                Model model = LoadFromFile(_path);
                if(_saveModel) {
                    loadedModels.Add(_path, model);
                }
                return model;
            }

        }

        private static Model LoadFromFile(string _path) {

            AssimpContext importer = new();
            importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));

            LogStream logStream = new(delegate(string _message, string _userData) {
                Console.WriteLine(_message);
            });
            logStream.Attach();

            Scene scene = importer.ImportFile(_path, PostProcessPreset.TargetRealTimeMaximumQuality);

            if(scene.MeshCount <= 0) {
                Console.WriteLine("Model did not contain a mesh.");
                return new Model();
            }

            Mesh[] meshes = new Mesh[scene.MeshCount];
            for(int i = 0; i < meshes.Length; i++) {

                Assimp.Mesh loadedMesh = scene.Meshes[i];            
                meshes[i] = new() {
                    vertices      = Array.ConvertAll(loadedMesh.Vertices.ToArray(),                     item => (Vector3)item),
                    vertexColours = Array.ConvertAll(loadedMesh.VertexColorChannels[0].ToArray(),       item => (Colour)item),
                    uvs           = Array.ConvertAll(loadedMesh.TextureCoordinateChannels[0].ToArray(), item => (Vector2)(Vector3)item),
                    normals       = Array.ConvertAll(loadedMesh.Normals.ToArray(),                      item => (Vector3)item),
                    tangents      = Array.ConvertAll(loadedMesh.Tangents.ToArray(),                     item => (Vector3)item),
                    biTangents    = Array.ConvertAll(loadedMesh.BiTangents.ToArray(),                   item => (Vector3)item),

                    triangles = loadedMesh.GetIndices()
                };

            }

            if(scene.MaterialCount <= 0) {
                Console.WriteLine("Model did not contain a material.");
                return new Model(meshes, null);
            }

            // TODO: Fix ambient strength.
            // TODO: Load all other textures.
            // TODO: Add support for embedded textures.

            StandardMaterial[] materials = new StandardMaterial[scene.MaterialCount]; 
            for(int i = 0; i < materials.Length; i++) {

                Assimp.Material loadedMat = scene.Materials[i];
                StandardMaterial mat = new() {
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

                materials[i] = mat;
                
            }

            importer.Dispose();

            return new Model(meshes, materials);

        }

    }

}