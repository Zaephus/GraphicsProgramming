
using Assimp;
using Assimp.Configs;

namespace ZaephusEngine {

    public class Mesh {

        public virtual Vector3[] vertices { get; set; } = new Vector3[0];
        public virtual Colour[] vertexColours { get; set; } = new Colour[0];
        public virtual Vector2[] uvs { get; set; } = new Vector2[0];
        public virtual Vector3[] normals { get; set; } = new Vector3[0];
        public virtual Vector3[] tangents { get; set; } = new Vector3[0];
        public virtual Vector3[] biTangents { get; set; } = new Vector3[0];

        public virtual int[] triangles { get; set; }

        public Vertex[] VertexArray {
            get {
                Vertex[] vertexArray = new Vertex[vertices.Length];
                for(int i = 0; i < vertexArray.Length; i++) {
                    vertexArray[i] = new Vertex(
                        i < vertices.Length         ? vertices[i]       : Vector3.zero,
                        i < vertexColours.Length    ? vertexColours[i]  : Colour.white,
                        i < uvs.Length              ? uvs[i]            : Vector2.zero,
                        i < normals.Length          ? normals[i]        : Vector3.zero,
                        i < tangents.Length         ? tangents[i]       : Vector3.zero,
                        i < biTangents.Length       ? biTangents[i]     : Vector3.zero
                    );
                }
                return vertexArray;
            }
        }

        // TODO: Fix Recalculation of Normals.
        public void RecalculateNormals() {
            Vector3[] n = new Vector3[vertices.Length];

            for(int i = 0; i < triangles.Length; i += 3) {
                Vector3 vertA = vertices[triangles[i]];
                Vector3 vertB = vertices[triangles[i+1]];
                Vector3 vertC = vertices[triangles[i+2]];

                Vector3 result = Vector3.Cross(vertB - vertA, vertC - vertA);

                // TODO: Verify if should be -= result.
                n[triangles[i]]   -= result;
                n[triangles[i+1]] -= result;
                n[triangles[i+2]] -= result;
            }

            for(int i = 0; i < n.Length; i++) {
                n[i].Normalize();
            }

            normals = n;
        }

        // TODO: Make Load only return a mesh.
        // Maybe with a delegate?
        public static (Mesh, Material) Load(string _path) {

            AssimpContext importer = new();
            importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));

            LogStream logStream = new(delegate(string _message, string _userData) {
                Console.WriteLine(_message);
            });
            logStream.Attach();

            Scene scene = importer.ImportFile(_path, PostProcessPreset.TargetRealTimeMaximumQuality);

            if(scene.MeshCount <= 0) {
                Console.WriteLine("Model did not contain a mesh.");
                return (null, null);
            }

            // TODO: Add support for loading a model with multiple meshes.
            Assimp.Mesh loadedMesh = scene.Meshes[0];            
            Mesh mesh = new() {
                vertices      = Array.ConvertAll(loadedMesh.Vertices.ToArray(),                     item => (Vector3)item),
                vertexColours = Array.ConvertAll(loadedMesh.VertexColorChannels[0].ToArray(),       item => (Colour)item),
                uvs           = Array.ConvertAll(loadedMesh.TextureCoordinateChannels[0].ToArray(), item => (Vector2)(Vector3)item),
                normals       = Array.ConvertAll(loadedMesh.Normals.ToArray(),                      item => (Vector3)item),
                tangents      = Array.ConvertAll(loadedMesh.Tangents.ToArray(),                     item => (Vector3)item),
                biTangents    = Array.ConvertAll(loadedMesh.BiTangents.ToArray(),                   item => (Vector3)item),

                triangles = loadedMesh.GetIndices()
            };

            // TODO: Add support for loading a model with multiple materials.

            if(scene.MaterialCount <= 0) {
                Console.WriteLine("Model did not contain a material.");
                return (mesh, new Material());
            }

            // TODO: Fix ambient strength.
            // TODO: Load all other textures.
            // TODO: Add support for embedded textures.
            Assimp.Material loadedMat = scene.Materials[0];
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

            return (mesh, mat);

        }

    }

}