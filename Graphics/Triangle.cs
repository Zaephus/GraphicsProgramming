using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class Triangle {

        private int vertexBufferObject;
        private int vertexArrayObject;

        private Shader shader;

        public Vector3[] vertices = {
            new Vector3(-0.5f, -0.5f, 0.0f),
            new Vector3(0.5f, -0.5f, 0.0f),
            new Vector3(0.0f, 0.5f, 0.0f)
        };

        public Triangle() {
            Window.OnLoadMeshes += OnLoad;
            Window.OnRenderMeshes += OnRender;
            Window.OnUnloadMeshes += OnUnload;
        }

        private void OnLoad() {

            vertexBufferObject = GL.GenBuffer();
            vertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * 3 * sizeof(float), GetVerticesFloatArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            shader = new Shader("Core/Graphics/shader.vert", "Core/Graphics/shader.frag");

        }

        private void OnRender() {
            shader.Use();
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }

        private void OnUnload() {
            shader.Dispose();
        }

        private float[] GetVerticesFloatArray() {

            List<float> verts = new List<float>();

            for(int i = 0; i < vertices.Length; i++) {
                for(int j = 0; j < 3; j++) {
                    verts.Add(vertices[i][j]);
                }
            }

            return verts.ToArray();

        }

    }

}