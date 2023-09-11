

namespace ZaephusEngine {

    public class Transform : Component {

        public Vector3 position { get; set; } = Vector3.zero;
        public Quaternion rotation { get; set; } = Quaternion.identity;
        public Vector3 scale { get; set; } = Vector3.one;

        public Matrix4x4 objectMatrix { 
            get {
                Matrix4x4 t = Matrix4x4.TranslateMatrix(position);
                Matrix4x4 r = Matrix4x4.RotateMatrix(rotation);
                Matrix4x4 s = Matrix4x4.ScaleMatrix(scale);
                return t * r * s;
            }
        }

        public void Rotate(float _xDeg, float _yDeg, float _zDeg) { Rotate(Quaternion.FromEuler(_xDeg, _yDeg, _zDeg)); }
        public void Rotate(Vector3 _eulerAnglesDeg) { Rotate(Quaternion.FromEuler(_eulerAnglesDeg)); }
        public void Rotate(Quaternion _q) {
            rotation *= _q;
        }

        public void RotateAround(Vector3 _axis, float _deg) {
            rotation *= Quaternion.FromAxisAngle(_axis, _deg * Math.Deg2Rad);
        }

        public Vector3 right   { get { return (rotation * Vector3.right).normalized; } }
        public Vector3 forward { get { return (rotation * Vector3.forward).normalized; } }
        public Vector3 up      { get { return (rotation * Vector3.up).normalized; } }

    }

}