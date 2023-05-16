using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class Cube : Mesh {

        #region Model
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
        #endregion

        public Transform transform = new Transform(null);

        private Matrix4x4 finalMatrix;

        public Cube(Vector3 _pos) {
            base.vertices = this.vertices;
            base.uvs = this.uvs;
            base.normals = this.normals;

            base.triangles = this.triangles;

            transform.position = _pos;
        }

        protected override void OnLoad() {
            base.OnLoad();

            transform.rotation = Quaternion.FromEuler(45, 45, 0);
            transform.scale = Vector3.one * 0.5f;

        }

        protected override void OnRender() {
            base.OnRender();

            finalMatrix = Camera.ActiveCamera.ProjectionMatrix * Camera.ActiveCamera.ViewMatrix * transform.objectMatrix;
            material.shader.SetMatrix4x4("finalMatrix", ref finalMatrix, true);

        }

        protected override void OnUnload() {
            base.OnUnload();
        }

    }

}