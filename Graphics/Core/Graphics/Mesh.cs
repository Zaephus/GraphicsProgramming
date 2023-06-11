

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