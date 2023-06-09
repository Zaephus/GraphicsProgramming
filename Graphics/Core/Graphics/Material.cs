
using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class Material {

        public Shader shader;

        private bool isInitialized;

        public Material() : this("Core/Graphics/Shaders/Vertex.glsl", "Core/Graphics/Shaders/Fragment.glsl") {}
        public Material(string _vertexShaderPath, string _fragmentShaderPath) {
            shader = new Shader(_vertexShaderPath, _fragmentShaderPath);
        }

        public void Initialize() {
            shader.Bind();

            isInitialized = true;

            ObjectColour = ObjectColour;
            AmbientStrength = AmbientStrength;
            SpecularStrength = SpecularStrength;
            Shininess = Shininess;
            DiffuseMap = DiffuseMap;
            SpecularMap = SpecularMap;

            ApplyLighting();

            shader.Use();
        }

        public void Render() {

            ApplyLighting();

            shader.Use();
            DiffuseMap.Use();
            SpecularMap.Use();

        }

        public void Dispose() {
            shader.Dispose();
        }

        private void ApplyLighting() {
            shader.SetInt("dirLightNum", LightUtility.dirLights.Count);
            for(int i = 0; i < LightUtility.dirLights.Count; i++) {
                SetDirLight(LightUtility.dirLights[i], i);
            }

            shader.SetInt("pointLightNum", LightUtility.pointLights.Count);
            for(int i = 0; i < LightUtility.pointLights.Count; i++) {
                SetPointLight(LightUtility.pointLights[i], i);
            }
        }

        private void SetDirLight(DirectionalLight _light, int _index) {
            shader.SetColour($"dirLights[{_index}].colour", _light.colour);
            shader.SetVector3($"dirLights[{_index}].direction", _light.transform.forward);
        }

        private void SetPointLight(PointLight _light, int _index) {
            shader.SetColour($"pointLights[{_index}].colour", _light.colour);
            shader.SetVector3($"pointLights[{_index}].position", _light.transform.position);

            shader.SetFloat($"pointLights[{_index}].constant", _light.constantAttenuation);
            shader.SetFloat($"pointLights[{_index}].linear", _light.linearAttenuation);
            shader.SetFloat($"pointLights[{_index}].quadratic", _light.quadraticAttenuation);
        }

        // private void SetSpotLight(Light _light, int _index) {
        //     Console.WriteLine("Spot Light Setting not defined yet.");
        // }

        private Colour objectColour = Colour.white;
        public Colour ObjectColour {
            get {
                return objectColour;
            }
            set {
                objectColour = value;
                if(isInitialized) {
                    shader.SetColour("material.colour", objectColour);
                }
            }
        }

        private float ambientStrength = 1.0f;
        public float AmbientStrength {
            get {
                return ambientStrength;
            }
            set {
                ambientStrength = value;
                if(isInitialized) {
                    shader.SetFloat("material.ambientStrength", ambientStrength);
                }
            }
        }

        private float specularStrength = 1.0f;
        public float SpecularStrength {
            get {
                return specularStrength;
            }
            set {
                specularStrength = value;
                if(isInitialized) {
                    shader.SetFloat("material.specularStrength", specularStrength);
                }
            }
        }

        private float shininess = 32.0f;
        public float Shininess {
            get {
                return shininess;
            }
            set {
                shininess = value;
                if(isInitialized) {
                    shader.SetFloat("material.shininess", shininess);
                }
            }
        }

        private Texture2D diffuseMap = new Texture2D(Colour.white);
        public Texture2D DiffuseMap {
            get {
                return diffuseMap;
            }
            set {
                diffuseMap = value;
                if(isInitialized) {
                    diffuseMap.Bind(TextureUnit.Texture0);
                    shader.SetInt("material.diffuseMap", 0);
                }
            }
        }

        private Texture2D specularMap = new Texture2D(Colour.white);
        public Texture2D SpecularMap {
            get {
                return specularMap;
            }
            set {
                specularMap = value;
                if(isInitialized) {
                    specularMap.Bind(TextureUnit.Texture1);
                    shader.SetInt("material.specularMap", 1);
                }
            }
        }

    }

}