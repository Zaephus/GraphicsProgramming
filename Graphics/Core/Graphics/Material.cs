using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class Material {

        public Shader shader;

        public Colour colour;

        public Texture texture0;

        public Material() : this(Colour.white, new Texture(), "Core/Graphics/Shaders/Vertex.glsl", "Core/Graphics/Shaders/Fragment.glsl") {}
        public Material(Colour _c) : this(_c, new Texture(), "Core/Graphics/Shaders/Vertex.glsl", "Core/Graphics/Shaders/Fragment.glsl") {}
        public Material(Texture _t) : this(Colour.white, _t, "Core/Graphics/Shaders/Vertex.glsl", "Core/Graphics/Shaders/Fragment.glsl") {}
        public Material(string _vertexShaderPath, string _fragmentShaderPath) : this(Colour.white, new Texture(), _vertexShaderPath, _fragmentShaderPath) {}
        public Material(Colour _c, Texture _t, string _vertexShaderPath, string _fragmentShaderPath) {
            shader = new Shader(_vertexShaderPath, _fragmentShaderPath);
            
            colour = _c;
            texture0 = _t;
        }

        public void OnLoad() {

            shader.Use();
            
            shader.SetColour("colour", colour);
            shader.SetInt("texture0", 0);
        }

        public void OnRender() {
            shader.Use();
            texture0.Use();
        }

        public void OnUnload() {
            shader.Dispose();
        }

    }

}