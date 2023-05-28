using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class Shader : IDisposable {

        public int handle;

        private bool hasDisposedValue = false;

        public Shader(string _vertexPath, string _fragmentPath) {

            int vertexShader = CreateShader(_vertexPath, ShaderType.VertexShader);
            int fragmentShader = CreateShader(_fragmentPath, ShaderType.FragmentShader);

            handle = GL.CreateProgram();

            GL.AttachShader(handle, vertexShader);
            GL.AttachShader(handle, fragmentShader);

            GL.LinkProgram(handle);

            GL.GetProgram(handle, GetProgramParameterName.LinkStatus, out int _success);
            if(_success == 0) {
                string infoLog = GL.GetProgramInfoLog(handle);
                Console.WriteLine("General Info");
                Console.WriteLine(infoLog);
            }

            GL.DetachShader(handle, vertexShader);
            GL.DetachShader(handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

        }

        public void Use() {
            GL.UseProgram(handle);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool _isDisposing) {
            if(!hasDisposedValue) {
                GL.DeleteProgram(handle);
                hasDisposedValue = true;
            }
        }

        ~Shader() {
            if(!hasDisposedValue) {
                Console.WriteLine("GPU resource leak! Did you forget to call Dispose()?");
            }
        }

        private int CreateShader(string _path, ShaderType _type) {

            int shaderPointer = 0;

            string source = File.ReadAllText(_path);

            shaderPointer = GL.CreateShader(_type);
            GL.ShaderSource(shaderPointer, source);

            GL.CompileShader(shaderPointer);

            GL.GetShader(shaderPointer, ShaderParameter.CompileStatus, out int _success);
            if(_success == 0) {
                string infoLog = GL.GetShaderInfoLog(shaderPointer);
                Console.WriteLine(_type + " Info");
                Console.WriteLine(infoLog);
            }

            return shaderPointer;

        }

        public void SetInt(string _name, int _value) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            GL.Uniform1(location, _value);
        }

        public void SetFloat(string _name, float _value) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            GL.Uniform1(location, _value);
        }

        public void SetBool(string _name, bool _value) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            if(_value) {
                GL.Uniform1(location, 1);
            }
            else {
                GL.Uniform1(location, 0);
            }
        }

        public void SetVector2(string _name, Vector2 _vector) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            GL.Uniform2(location, _vector.x, _vector.y);
        }

        public void SetVector3(string _name, Vector3 _vector) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            GL.Uniform3(location, _vector.x, _vector.y, _vector.z);
        }

        public void SetVector4(string _name, Vector4 _vector) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            GL.Uniform4(location, _vector.x, _vector.y, _vector.z, _vector.w);
        }

        public void SetVector2Int(string _name, Vector2Int _vector) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            GL.Uniform2(location, _vector.x, _vector.y);
        }

        public void SetVector3Int(string _name, Vector3Int _vector) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            GL.Uniform3(location, _vector.x, _vector.y, _vector.z);
        }

        public void SetColour(string _name, Colour _colour) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            GL.Uniform4(location, _colour.R, _colour.G, _colour.B, _colour.A);
        }

        public void SetQuaternion(string _name, Quaternion _quaternion) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            GL.Uniform4(location, _quaternion.x, _quaternion.y, _quaternion.z, _quaternion.w);
        }

        public unsafe void SetMatrix4x4(string _name, ref Matrix4x4 _matrix) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            fixed(float* matrixPtr = &_matrix.m00) {
                GL.UniformMatrix4(location, 1, true, matrixPtr);
            }
        }

    }

}