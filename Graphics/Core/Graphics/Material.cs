
using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class Material {

        protected Shader shader;

        protected bool isInitialized;

        private List<Texture2D> textures = new();

        public Material() : this(Shader.standard) {}
        public Material(Shader _shader) {
            shader = _shader;
        }

        public virtual void Initialize() {
            shader.Bind();

            isInitialized = true;

            ApplyLighting();

            shader.Use();
        }

        public virtual void Render() {

            ApplyLighting();

            shader.Use();

            foreach(Texture2D texture in textures) {
                texture.Use();
            }

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

        public void SetInt(string _name, int _value) {
            shader.Use();
            int location = shader.GetUniformLocation(_name);
            GL.Uniform1(location, _value);
        }

        public void SetFloat(string _name, float _value) {
            shader.Use();
            int location = shader.GetUniformLocation(_name);
            GL.Uniform1(location, _value);
        }

        public void SetBool(string _name, bool _value) {
            shader.Use();
            int location = shader.GetUniformLocation(_name);
            if(_value) {
                GL.Uniform1(location, 1);
            }
            else {
                GL.Uniform1(location, 0);
            }
        }

        public void SetVector2(string _name, Vector2 _vector) {
            shader.Use();
            int location = shader.GetUniformLocation(_name);
            GL.Uniform2(location, _vector.x, _vector.y);
        }

        public void SetVector3(string _name, Vector3 _vector) {
            shader.Use();
            int location = shader.GetUniformLocation(_name);
            GL.Uniform3(location, _vector.x, _vector.y, _vector.z);
        }

        public void SetVector4(string _name, Vector4 _vector) {
            shader.Use();
            int location = shader.GetUniformLocation(_name);
            GL.Uniform4(location, _vector.x, _vector.y, _vector.z, _vector.w);
        }

        public void SetVector2Int(string _name, Vector2Int _vector) {
            shader.Use();
            int location = shader.GetUniformLocation(_name);
            GL.Uniform2(location, _vector.x, _vector.y);
        }

        public void SetVector3Int(string _name, Vector3Int _vector) {
            shader.Use();
            int location = shader.GetUniformLocation(_name);
            GL.Uniform3(location, _vector.x, _vector.y, _vector.z);
        }

        public void SetColour(string _name, Colour _colour) {
            shader.Use();
            int location = shader.GetUniformLocation(_name);
            GL.Uniform4(location, _colour.R, _colour.G, _colour.B, _colour.A);
        }

        public void SetQuaternion(string _name, Quaternion _quaternion) {
            shader.Use();
            int location = shader.GetUniformLocation(_name);
            GL.Uniform4(location, _quaternion.x, _quaternion.y, _quaternion.z, _quaternion.w);
        }

        public unsafe void SetMatrix4x4(string _name, ref Matrix4x4 _matrix) {
            shader.Use();
            int location = shader.GetUniformLocation(_name);
            fixed(float* matrixPtr = &_matrix.m00) {
                GL.UniformMatrix4(location, 1, true, matrixPtr);
            }
        }

        public void SetTexture2D(string _name, Texture2D _texture) {
            shader.Use();

            int unit = textures.IndexOf(textures.Find(item => item.name == _name));

            if(unit < 0) {
                unit = textures.Count;
            }

            _texture.Bind(unit);
            shader.SetInt(_name, unit);
            _texture.name = _name;
            textures.Add(_texture);

            int location = shader.GetUniformLocation(_name);
            GL.Uniform1(location, unit);

        }

    }

}