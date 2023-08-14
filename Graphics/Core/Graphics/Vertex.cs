

namespace ZaephusEngine {
    
    public struct Vertex {

        public Vector3 vertexPosition;
        public Colour vertexColour;
        public Vector2 UV;
        public Vector3 normal;
        public Vector3 tangent;
        public Vector3 biTangent;

        public Vertex(Vector3 _pos) : this(_pos, Colour.white, Vector2.zero, Vector3.zero, Vector3.zero, Vector3.zero) {}
        public Vertex(Vector3 _pos, Colour _col) : this(_pos, _col, Vector2.zero, Vector3.zero, Vector3.zero, Vector3.zero) {}
        public Vertex(Vector3 _pos, Colour _col, Vector2 _uv, Vector3 _normal, Vector3 _tan, Vector3 _biTan) {
            vertexPosition = _pos;
            vertexColour = _col;
            UV = _uv;
            normal = _normal;
            tangent = _tan;
            biTangent = _biTan;            
        }

    }

}