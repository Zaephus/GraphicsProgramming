using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class Triangle : Mesh {

        private Colour colour;

        public new Vector3[] vertices = {
            new Vector3(-0.5f, -0.5f, 0.0f),
            new Vector3(0.5f, -0.5f, 0.0f),
            new Vector3(0.0f, 0.5f, 0.0f)
        };

        public new Colour[] vertexColours = {
            Colour.red,
            Colour.green,
            Colour.blue
        };

        public new uint[] triangles = {
            0, 1, 2
        };

        public Triangle(Colour _colour) {
            colour = _colour;

            base.vertices = this.vertices;
            base.vertexColours = this.vertexColours;
            base.triangles = this.triangles;

        }

        protected override void OnLoad() {
            base.OnLoad();
        }

        protected override void OnRender() {
            base.OnRender();
        }

        protected override void OnUnload() {
            base.OnUnload();
        }

    }

}