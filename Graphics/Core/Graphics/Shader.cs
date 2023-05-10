using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class Shader : IDisposable {

        public int handle;

        private bool hasDisposedValue = false;

        public Shader(string _vertexPath, string _fragmentPath) {

            int vertexShader = 0;
            int fragmentShader = 0;

            string vertexShaderSource = File.ReadAllText(_vertexPath);
            string fragmentShaderSource = File.ReadAllText(_fragmentPath);

            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);

            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);

            GL.CompileShader(vertexShader);

            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int _vertexSuccess);
            if(_vertexSuccess == 0) {
                string infoLog = GL.GetShaderInfoLog(vertexShader);
                Console.WriteLine("Vertex Shader Info");
                Console.WriteLine(infoLog);
            }

            GL.CompileShader(fragmentShader);

            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int _fragmentSuccess);
            if(_fragmentSuccess == 0) {
                string infoLog = GL.GetShaderInfoLog(fragmentShader);
                Console.WriteLine("Fragment Shader Info");
                Console.WriteLine(infoLog);
            }

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

        // TODO: Fix SetBool function.
        public void SetBool(string _name, bool _value) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            switch(_value) {
                case false:
                    GL.Uniform1(location, -1);
                    break;
                case true:
                    GL.Uniform1(location, 1);
                    break;
            }
        }

        public void SetInt(string _name, int _value) {
            Use();
            int location = GL.GetUniformLocation(handle, _name);
            GL.Uniform1(location, _value);
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