

namespace ZaephusEngine {

    public class Mesh {

        public virtual Vector3[] vertices { get; set; } = new Vector3[0];
        public virtual Colour[] vertexColours { get; set; } = new Colour[0];
        public virtual Vector2[] uvs { get; set; } = new Vector2[0];
        public virtual Vector3[] normals { get; set; } = new Vector3[0];
        public virtual Vector3[] tangents { get; set; } = new Vector3[0];
        public virtual Vector3[] biTangents { get; set; } = new Vector3[0];

        public virtual uint[] triangles { get; set; }

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

    }

}