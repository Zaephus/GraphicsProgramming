using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class Mesh {

        protected int vertexBufferObject;
        protected int vertexArrayObject;
        protected int elementBufferObject;

        protected Shader shader;

        public Vector3[] vertices = new Vector3[0];
        public Colour[] vertexColours = new Colour[0];
        public Vector2[] uvs = new Vector2[0];
        public Vector3[] normals = new Vector3[0];
        public Vector3[] tangents = new Vector3[0];
        public Vector3[] biTangents = new Vector3[0];

        public uint[] triangles;

        public Mesh() {
            Window.OnLoadMeshes += OnLoad;
            Window.OnRenderMeshes += OnRender;
            Window.OnUnloadMeshes += OnUnload;
        }

        protected virtual unsafe void OnLoad() {
            
            vertexBufferObject = GL.GenBuffer();
            vertexArrayObject = GL.GenVertexArray();
            elementBufferObject = GL.GenBuffer();

            GL.BindVertexArray(vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);

            // TODO Add world position to vertex positions.
            Vertex[] vertexArray = new Vertex[vertices.Length];
            for(int i = 0; i < vertexArray.Length; i++) {
                vertexArray[i] = new Vertex(
                    i < vertices.Length         ? vertices[i]       : Vector3.zero,
                    i < vertexColours.Length    ? vertexColours[i]  : Colour.white,
                    i < uvs.Length              ? uvs[i]            : Vector2.zero,
                    i < normals.Length          ? normals[i]        : Vector3.zero,
                    i < tangents.Length         ? tangents[i]       : Vector3.zero,
                    i < biTangents.Length       ? biTangents[i]     : Vector3.zero
                );
            }

            GL.BufferData(BufferTarget.ArrayBuffer, vertexArray.Length * sizeof(Vertex), vertexArray, BufferUsageHint.StaticDraw);
            GL.BufferData(BufferTarget.ElementArrayBuffer, triangles.Length * sizeof(uint), triangles, BufferUsageHint.StaticDraw);

            // Vertex Position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(Vertex), 0);
            GL.EnableVertexAttribArray(0);

            // Vertex Colour
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, sizeof(Vertex), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            // UV's
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, sizeof(Vertex), 7 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            // Normals
            GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, sizeof(Vertex), 9 * sizeof(float));
            GL.EnableVertexAttribArray(3);

            // Tangents
            GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, sizeof(Vertex), 12 * sizeof(float));
            GL.EnableVertexAttribArray(4);

            // Bi Tangents
            GL.VertexAttribPointer(5, 3, VertexAttribPointerType.Float, false, sizeof(Vertex), 15 * sizeof(float));
            GL.EnableVertexAttribArray(5);

            shader = new Shader("Core/Graphics/Shaders/Vertex.glsl", "Core/Graphics/Shaders/Fragment.glsl");

        }

        protected virtual void OnRender() {
            shader.Use();
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, triangles.Length, DrawElementsType.UnsignedInt, 0);
        }

        protected virtual void OnUnload() {
            shader.Dispose();
        }

    }

}