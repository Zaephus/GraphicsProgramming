using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

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

}