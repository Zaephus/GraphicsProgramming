using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

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

        public Vector3 position;

        private Texture texture0;
        private Texture texture1;

        private Matrix4x4 modelMatrix;
        private Matrix4x4 viewMatrix;
        private Matrix4x4 projectionMatrix;

        public Quad(Vector3 _pos) {
            base.vertices = this.vertices;
            base.vertexColours = this.vertexColours;
            base.uvs = this.uvs;

            base.triangles = this.triangles;

            position = _pos;
        }

        protected override void OnLoad() {
            base.OnLoad();

            texture0 = new Texture("Textures/container.png");
            texture1 = new Texture("Textures/awesomeFace.png");

            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);

            Matrix4x4 translate = Matrix4x4.TranslateMatrix(position);
            Matrix4x4 rotation = Matrix4x4.RotateMatrix(Quaternion.FromEuler(0, 0, 90));
            Matrix4x4 scale = Matrix4x4.ScaleMatrix(Vector3.one * 0.5f);

            modelMatrix = translate * rotation * scale;

            viewMatrix = Matrix4x4.TranslateMatrix(new Vector3(0.0f, 0.0f, 1.0f));
            projectionMatrix = Matrix4x4.perspectiveProjection;

            shader.SetMatrix4x4("model", ref modelMatrix, false);
            shader.SetMatrix4x4("view", ref viewMatrix, false);
            shader.SetMatrix4x4("projection", ref projectionMatrix, false);

        }

        float rot = 90;
        float s = 0.5f;

        protected override void OnRender() {
            base.OnRender();

            texture0.Use(TextureUnit.Texture0);
            texture1.Use(TextureUnit.Texture1);

            // pos.x += 0.0001f;
            rot += 0.01f;
            // s += 0.00005f;
            
            Matrix4x4 translate = Matrix4x4.TranslateMatrix(position);
            Matrix4x4 rotation = Matrix4x4.RotateMatrix(Quaternion.FromEuler(45, 0, rot));
            Matrix4x4 scale = Matrix4x4.ScaleMatrix(Vector3.one * s);

            modelMatrix = translate * rotation * scale;

            shader.SetMatrix4x4("model", ref modelMatrix, false);
            shader.SetMatrix4x4("view", ref viewMatrix, false);
            shader.SetMatrix4x4("projection", ref projectionMatrix, false);
            
        }

        protected override void OnUnload() {
            base.OnUnload();
        }

    }

}