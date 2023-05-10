

namespace ZaephusEngine {

    public class Camera {

        public Vector3 position;

        public Matrix4x4 ViewMatrix {
            get {
                return Matrix4x4.TranslateMatrix(-1 *position);
            }
        }

        public Camera(Vector3 _pos) {
            position = _pos;
        }

    }

}