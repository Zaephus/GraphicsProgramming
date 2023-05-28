
using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class Material {

        public Shader shader;

        public Material() : this("Core/Graphics/Shaders/Vertex.glsl", "Core/Graphics/Shaders/Fragment.glsl") {}
        public Material(string _vertexShaderPath, string _fragmentShaderPath) {
            shader = new Shader(_vertexShaderPath, _fragmentShaderPath);
        }

        public void OnLoad() {
            ObjectColour = ObjectColour;
            LightColour = LightColour;
            AmbientStrength = AmbientStrength;
            SpecularStrength = SpecularStrength;
            Shininess = Shininess;
            DiffuseMap = DiffuseMap;
            SpecularMap = SpecularMap;

            shader.Use();
        }

        public void OnRender() {
            shader.Use();
            DiffuseMap.Use();
            SpecularMap.Use();
        }

        public void OnUnload() {
            shader.Dispose();
        }

        private Colour objectColour = Colour.white;
        public Colour ObjectColour {
            get {
                return objectColour;
            }
            set {
                objectColour = value;
                shader.SetColour("material.colour", objectColour);
            }
        }

        // TODO: Transfer to a Light class.
        private Colour lightColour = Colour.white;
        public Colour LightColour {
            get {
                return lightColour;
            }
            set {
                lightColour = value;
                shader.SetColour("lightColour", lightColour);
            }
        }

        // TODO: Transfer to a Light class.
        private float ambientStrength = 1.0f;
        public float AmbientStrength {
            get {
                return ambientStrength;
            }
            set {
                ambientStrength = value;
                shader.SetFloat("material.ambientStrength", ambientStrength);
            }
        }

        private float specularStrength = 1.0f;
        public float SpecularStrength {
            get {
                return specularStrength;
            }
            set {
                specularStrength = value;
                shader.SetFloat("material.specularStrength", specularStrength);
            }
        }

        private float shininess = 32.0f;
        public float Shininess {
            get {
                return shininess;
            }
            set {
                shininess = value;
                shader.SetFloat("material.shininess", shininess);
            }
        }

        private Texture2D diffuseMap = new Texture2D(Colour.white);
        public Texture2D DiffuseMap {
            get {
                return diffuseMap;
            }
            set {
                diffuseMap = value;
                diffuseMap.Bind(TextureUnit.Texture0);
                shader.SetInt("material.diffuseMap", 0);
            }
        }

        private Texture2D specularMap = new Texture2D(Colour.white);
        public Texture2D SpecularMap {
            get {
                return specularMap;
            }
            set {
                specularMap = value;
                specularMap.Bind(TextureUnit.Texture1);
                shader.SetInt("material.specularMap", 1);
            }
        }
    }

}