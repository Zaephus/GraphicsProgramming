using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class Shader : IDisposable {

        private int handle;

        private string vertexPath;
        private string fragmentPath;

        private bool isDisposed = false;
        private bool isBound = false;

        public Shader(string _vertexPath, string _fragmentPath) {
            vertexPath = _vertexPath;
            fragmentPath = _fragmentPath;
        }

        public void Bind() {

            int vertexShader = CreateShader(vertexPath, ShaderType.VertexShader);
            int fragmentShader = CreateShader(fragmentPath, ShaderType.FragmentShader);

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

            isBound = true;

        }

        public void Use() {
            GL.UseProgram(handle);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool _isDisposing) {
            if(!isDisposed) {
                GL.DeleteProgram(handle);
                isDisposed = true;
            }
        }

        ~Shader() {
            if(!isDisposed && isBound) {
                Console.WriteLine("GPU resource leak! Did you forget to call Dispose()?");
            }
        }

        private int CreateShader(string _path, ShaderType _type) {

            string source = File.ReadAllText(_path);

            int shaderPointer = GL.CreateShader(_type);
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

        public int GetUniformLocation(string _name) {
            return GL.GetUniformLocation(handle, _name);
        }

        public static Shader standard { get { return new Shader("Core/Graphics/Shaders/Vertex.glsl", "Core/Graphics/Shaders/Fragment.glsl"); } }

    }

}