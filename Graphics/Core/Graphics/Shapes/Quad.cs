using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class Quad : Mesh {

        public new Vector3[] vertices = {
            new Vector3(-0.5f, -0.5f, 0.0f),
            new Vector3(0.5f, -0.5f, 0.0f),
            new Vector3(-0.5f, 0.5f, 0.0f),
            new Vector3(0.5f, 0.5f, 0.0f)
        };

        public new Colour[] vertexColours = {
            Colour.red,
            Colour.green,
            Colour.blue,
            Colour.white
        };

        public new Vector2[] uvs = {
            new Vector2(0.0f, 0.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f)
        };

        public new uint[] triangles = {
            0, 1, 2,
            1, 3, 2
        };

        Texture texture0;
        Texture texture1;

        public Quad() {
            base.vertices = this.vertices;
            base.vertexColours = this.vertexColours;
            base.uvs = this.uvs;

            base.triangles = this.triangles;
        }

        protected override void OnLoad() {
            base.OnLoad();

            texture0 = new Texture("Textures/container.png");
            texture0.Use(TextureUnit.Texture0);

            texture1 = new Texture("Textures/awesomeFace.png");
            texture1.Use(TextureUnit.Texture1);

            shader.Use();
            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);

        }

        protected override void OnRender() {
            base.OnRender();

            texture0.Use(TextureUnit.Texture0);
            texture1.Use(TextureUnit.Texture1);
            
        }

        protected override void OnUnload() {
            base.OnUnload();
        }

    }

}