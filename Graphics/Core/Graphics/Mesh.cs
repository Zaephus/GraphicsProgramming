
using Assimp;
using Assimp.Configs;
using OpenTK.Graphics.OpenGL4;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace ZaephusEngine {

    public class Mesh {

        public virtual Vector3[] vertices { get; set; } = new Vector3[0];
        public virtual Colour[] vertexColours { get; set; } = new Colour[0];
        public virtual Vector2[] uvs { get; set; } = new Vector2[0];
        public virtual Vector3[] normals { get; set; } = new Vector3[0];
        public virtual Vector3[] tangents { get; set; } = new Vector3[0];
        public virtual Vector3[] biTangents { get; set; } = new Vector3[0];

        public virtual int[] triangles { get; set; }

        private int vertexArrayObject;
        private int vertexBufferObject;
        private int elementBufferObject;

        public Vertex[] VertexArray {
            get {
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
                return vertexArray;
            }
        }

        public Mesh() {
            Game.InitCall += Initialize;
        }

        public unsafe void Initialize() {

            vertexArrayObject = GL.GenVertexArray();
            vertexBufferObject = GL.GenBuffer();
            elementBufferObject = GL.GenBuffer();

            GL.BindVertexArray(vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, VertexArray.Length * sizeof(Vertex), VertexArray, BufferUsageHint.DynamicDraw);
            GL.BufferData(BufferTarget.ElementArrayBuffer, triangles.Length * sizeof(uint), triangles, BufferUsageHint.DynamicDraw);

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

            
            Game.InitCall -= Initialize;

        }

        public void Render() {
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, triangles.Length, DrawElementsType.UnsignedInt, 0);
        }

        // TODO: Fix Recalculation of Normals.
        public void RecalculateNormals() {
            Vector3[] n = new Vector3[vertices.Length];

            for(int i = 0; i < triangles.Length; i += 3) {
                Vector3 vertA = vertices[triangles[i]];
                Vector3 vertB = vertices[triangles[i+1]];
                Vector3 vertC = vertices[triangles[i+2]];

                Vector3 result = Vector3.Cross(vertB - vertA, vertC - vertA);

                // TODO: Verify if should be -= result.
                n[triangles[i]]   -= result;
                n[triangles[i+1]] -= result;
                n[triangles[i+2]] -= result;
            }

            for(int i = 0; i < n.Length; i++) {
                n[i].Normalize();
            }

            normals = n;
        }

    }

}