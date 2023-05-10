

namespace ZaephusEngine {

    public class Transform : Component {

        public Vector3 position { get; set; }
        public Quaternion rotation { get; set; }
        public Vector3 scale { get; set; }

        public Matrix4x4 objectMatrix {
            get {
                Matrix4x4 t = Matrix4x4.TranslateMatrix(position);
                Matrix4x4 r = Matrix4x4.RotateMatrix(rotation);
                Matrix4x4 s = Matrix4x4.ScaleMatrix(scale);

                return t * r * s;
            }
        }

        public Transform(GameObject _parent) : base(_parent) {}

        public override void Start() {}
        public override void Update() {}
        public override void Exit() {}

        public void Rotate(float _xDeg, float _yDeg, float _zDeg) { Rotate(Quaternion.FromEuler(_xDeg, _yDeg, _zDeg)); }
        public void Rotate(Vector3 _eulerAnglesDeg) { Rotate(Quaternion.FromEuler(_eulerAnglesDeg)); }
        public void Rotate(Quaternion _q) {
            rotation *= _q;
        }

        // TODO: Add direction vectors.
    }

}