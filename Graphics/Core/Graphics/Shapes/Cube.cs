using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class Cube : Mesh {

        public new Vector3[] vertices = {
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),

            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),

            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),
            
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),

            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),

            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),

            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),

            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),

            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),

            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),

            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, 0.5f)
        };

        public new Vector2[] uvs = {
            new Vector2(1.0f, 0.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(0.0f, 0.0f),

            new Vector2(2.0f, 0.0f),
            new Vector2(2.0f, 1.0f),

            new Vector2(1.0f, 2.0f),
            new Vector2(0.0f, 2.0f),

            new Vector2(-1.0f, 1.0f),
            new Vector2(-1.0f, 0.0f),

            new Vector2(0.0f, -1.0f),
            new Vector2(1.0f, -1.0f),

            new Vector2(3.0f, 0.0f),
            new Vector2(3.0f, 1.0f),

            new Vector2(1.0f, 1.0f),
            new Vector2(0.0f, 1.0f),

            new Vector2(0.0f, 1.0f),
            new Vector2(0.0f, 0.0f),

            new Vector2(0.0f, 0.0f),
            new Vector2(1.0f, 0.0f),

            new Vector2(1.0f, 0.0f),
            new Vector2(1.0f, 1.0f),

            new Vector2(2.0f, 0.0f),
            new Vector2(2.0f, 1.0f)
        };

        public new Vector3[] normals = {
            new Vector3(0.0f, -1.0f, 0.0f),
            new Vector3(0.0f, -1.0f, 0.0f),
            new Vector3(0.0f, -1.0f, 0.0f),
            new Vector3(0.0f, -1.0f, 0.0f),

            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 0.0f, 0.0f),

            new Vector3(0.0f, 0.0f, 1.0f),
            new Vector3(0.0f, 0.0f, 1.0f),

            new Vector3(-1.0f, 0.0f, 0.0f),
            new Vector3(-1.0f, 0.0f, 0.0f),

            new Vector3(0.0f, 0.0f, -1.0f),
            new Vector3(0.0f, 0.0f, -1.0f),

            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),

            new Vector3(0.0f, 0.0f, 1.0f),
            new Vector3(0.0f, 0.0f, 1.0f),

            new Vector3(-1.0f, 0.0f, 0.0f),
            new Vector3(-1.0f, 0.0f, 0.0f),

            new Vector3(0.0f, 0.0f, -1.0f),
            new Vector3(0.0f, 0.0f, -1.0f),

            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 0.0f, 0.0f),

            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f)
        };

        public new uint[] triangles = {
            // Down
            0, 1, 2,
            0, 2, 3,

            // Back
            14, 6, 7,
            14, 7, 15,

            // Right
            20, 4, 5,
            20, 5, 21,

            // Left
            16, 8, 9,
            16, 9, 17,

            // Front
            18, 10, 11,
            18, 11, 19,

            // Up
            22, 12, 13,
            22, 13, 23
        };

        public Vector3 position;

        private Texture texture0;
        private Texture texture1;

        private Matrix4x4 modelMatrix;
        private Matrix4x4 viewMatrix;
        private Matrix4x4 projectionMatrix;

        public Cube(Vector3 _pos) {
            base.vertices = this.vertices;
            base.uvs = this.uvs;
            base.normals = this.normals;

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
            modelMatrix = translate * Matrix4x4.RotateMatrix(Quaternion.FromEuler(0, 150, 0)) * Matrix4x4.ScaleMatrix(Vector3.one * 0.5f);

            viewMatrix = Matrix4x4.TranslateMatrix(new Vector3(0.0f, 0.0f, 1.0f));
            projectionMatrix = Matrix4x4.perspectiveProjection;

            shader.SetMatrix4x4("model", ref modelMatrix);
            shader.SetMatrix4x4("view", ref viewMatrix);
            shader.SetMatrix4x4("projection", ref projectionMatrix);

        }

        float yRot = 20;

        protected override void OnRender() {
            base.OnRender();

            texture0.Use(TextureUnit.Texture0);
            texture1.Use(TextureUnit.Texture1);

            yRot += 0.01f;

            Matrix4x4 translate = Matrix4x4.TranslateMatrix(position);
            modelMatrix = translate * Matrix4x4.RotateMatrix(Quaternion.FromEuler(yRot, yRot, 0)) * Matrix4x4.ScaleMatrix(Vector3.one * 0.5f);
            
            shader.SetMatrix4x4("model", ref modelMatrix);
            shader.SetMatrix4x4("view", ref viewMatrix);
            shader.SetMatrix4x4("projection", ref projectionMatrix);

        }

        protected override void OnUnload() {
            base.OnUnload();
        }

    }

}