using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class Hexagon : Mesh {

        private Colour colour;

        public new Vector3[] vertices = {
            new Vector3(0.0f, 0.0f, 0.0f),      //0
            new Vector3(0.5f, 0.0f, 0.0f),      //1
            new Vector3(0.25f, 0.433f, 0.0f),   //2
            new Vector3(-0.25f, 0.433f, 0.0f),  //3
            new Vector3(-0.5f, 0.0f, 0.0f),     //4
            new Vector3(-0.25f, -0.433f, 0.0f), //5
            new Vector3(0.25f, -0.433f, 0.0f)   //6
        };

        public new Colour[] vertexColours = {
            Colour.white,
            Colour.red,
            Colour.red,
            Colour.green,
            Colour.green,
            Colour.blue,
            Colour.blue
        };

        public new uint[] triangles = {
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 5,
            0, 5, 6,
            0, 6, 1
        };

        public Hexagon(Colour _colour) {
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