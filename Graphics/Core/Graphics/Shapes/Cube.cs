using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

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

        public Transform transform = new Transform(null);

        private Texture texture0;
        private Texture texture1;

        private Matrix4x4 modelMatrix;
        private Matrix4x4 viewMatrix;
        private Matrix4x4 projectionMatrix;

        private Camera camera;

        public Cube(Vector3 _pos) {
            base.vertices = this.vertices;
            base.uvs = this.uvs;
            base.normals = this.normals;

            base.triangles = this.triangles;

            transform.position = _pos;
        }

        protected override void OnLoad() {
            base.OnLoad();

            texture0 = new Texture("Textures/container.png");
            texture1 = new Texture("Textures/awesomeFace.png");

            shader.SetInt("texture0", 0);
            shader.SetInt("texture1", 1);

            transform.rotation = Quaternion.FromEuler(45, 45, 0);
            transform.scale = Vector3.one;

            modelMatrix = transform.objectMatrix;
            viewMatrix = Matrix4x4.TranslateMatrix(new Vector3(0.0f, 0.0f, 3.0f));
            projectionMatrix = Matrix4x4.CreatePerspectiveProjection(-0.0711f, 0.0711f, -0.04f, 0.04f, 0.1f, 100f);

            camera = new Camera(new Vector3(0.0f, 0.0f, -1f));

            viewMatrix = camera.ViewMatrix;
            Console.WriteLine(viewMatrix);
            projectionMatrix = Matrix4x4.Perspective(Math.Deg2Rad * 45, 1.0f, 1f, 1000.0f);

            Console.WriteLine(projectionMatrix);
            Console.WriteLine("");
            Console.WriteLine(projectionMatrix * viewMatrix * modelMatrix * vertices[0]);

            shader.SetMatrix4x4("model", ref modelMatrix);
            shader.SetMatrix4x4("view", ref viewMatrix);
            shader.SetMatrix4x4("projection", ref projectionMatrix);

        }

        protected override void OnRender() {
            base.OnRender();

            texture0.Use(TextureUnit.Texture0);
            texture1.Use(TextureUnit.Texture1);

            transform.Rotate(0.01f, 0.01f, 0);

            modelMatrix = transform.objectMatrix;

            shader.SetMatrix4x4("model", ref modelMatrix);
            shader.SetMatrix4x4("view", ref viewMatrix);
            shader.SetMatrix4x4("projection", ref projectionMatrix);

        }

        protected override void OnUnload() {
            base.OnUnload();
        }

    }

}