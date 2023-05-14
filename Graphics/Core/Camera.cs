

namespace ZaephusEngine {

    public enum CameraProjectionType {
        Perspective = 0,
        Orthographic = 1
    }

    public class Camera : GameObject {

        public static Camera ActiveCamera;

        public Matrix4x4 ViewMatrix {
            get {
                Matrix4x4 t = Matrix4x4.TranslateMatrix(new Vector3(-transform.position.x, -transform.position.y, transform.position.z));
                Matrix4x4 d = CreateViewMatrix();
                return d * t;
            }
        }

        public Matrix4x4 ProjectionMatrix { get; private set; } = Matrix4x4.identity;

        private CameraProjectionType camType;

        private float fovY;
        private float camSize;
        private float nearPlane;
        private float farPlane;

        /// <summary>Creates a new camera.</summary>
        /// <param name="_type">Sets the type of the camera: Perspective or Orthographic.</param>
        /// <param name="_fovY">Vertical FOV in degrees for perspective camera. Leave 0 if orthographic.</param>
        /// <param name="_size">Size value for orthographic camera. Leave 0 if perspective.</param>
        /// <param name="_near">Distance of near plane from camera.</param>
        /// <param name="_far">Distance fo far plane from camera.</param>
        public Camera(CameraProjectionType _type, float _fovY, float _size, float _near, float _far) {

            if(ActiveCamera == null) {
                ActiveCamera = this;
            }

            transform = new Transform(this);

            Window.WindowResized += OnWindowResized;

            camType = _type;
            fovY = _fovY;
            camSize = _size;
            nearPlane = _near;
            farPlane = _far;

            switch(_type) {
                case CameraProjectionType.Perspective:
                    ProjectionMatrix = CalculatePerspectiveProjection(fovY, ((float)Window.width / (float)Window.height), nearPlane, farPlane);
                    break;
                
                case CameraProjectionType.Orthographic:
                    ProjectionMatrix = CalculateOrthoGraphicProjection(camSize, ((float)Window.width / (float)Window.height), nearPlane, farPlane);
                    break;
            }

        }

        private void OnWindowResized(int _width, int _height) {
            switch(camType) {
                case CameraProjectionType.Perspective:
                    ProjectionMatrix = CalculatePerspectiveProjection(fovY, ((float)_width / (float)_height), nearPlane, farPlane);
                    break;
                
                case CameraProjectionType.Orthographic:
                    ProjectionMatrix = CalculateOrthoGraphicProjection(camSize, ((float)_width / (float)_height), nearPlane, farPlane);
                    break;
            }
        }

        private Matrix4x4 CalculatePerspectiveProjection(float _fovY, float _aspect, float _near, float _far) {

            float fovY = _fovY * Math.Deg2Rad;

            if(fovY <= 0 || fovY > MathF.PI) {
                throw new ArgumentOutOfRangeException(nameof(fovY));
            }

            if(_aspect <= 0) {
                throw new ArgumentOutOfRangeException(nameof(_aspect));
            }

            if(_far <= 0) {
                throw new ArgumentOutOfRangeException(nameof(_far));
            }

            if(_near <= 0 || _near >= _far) {
                throw new ArgumentOutOfRangeException(nameof(_near));
            }
            
            float t = _near * MathF.Tan(0.5f * fovY);
            float r = _aspect  * t;

            float a = _near / r;
            float b = _near / t;

            float c = -(_far + _near) / (_far - _near);
            float d = -(2.0f * _far * _near) / (_far - _near);

            return new Matrix4x4(
                a, 0, 0, 0,
                0, b, 0, 0,
                0, 0, c, d,
                0, 0, -1, 0
            );

        }

        private Matrix4x4 CalculateOrthoGraphicProjection(float _sizeY, float _aspect, float _near, float _far) {

            if(_sizeY <= 0) {
                throw new ArgumentOutOfRangeException(nameof(_sizeY));
            }

            if(_aspect <= 0) {
                throw new ArgumentOutOfRangeException(nameof(_aspect));
            }

            if(_far <= 0) {
                throw new ArgumentOutOfRangeException(nameof(_far));
            }

            if(_near <= 0 || _near >= _far) {
                throw new ArgumentOutOfRangeException(nameof(_near));
            }

            float t = _sizeY / 2.0f;
            float r = _aspect * t;

            float a = 1.0f / r;
            float b = 1.0f / t;

            float c = -2.0f / (_far - _near);
            float d = -(_far + _near) / (_far - _near);

            return new Matrix4x4(
                a, 0, 0, 0,
                0, b, 0, 0,
                0, 0, c, d,
                0, 0, 0, 1
            );

        }

        private Matrix4x4 CreateViewMatrix() {
            
            Vector3 r = transform.right;
            Vector3 u = transform.up;
            Vector3 f = transform.forward;

            return new Matrix4x4(
                r.x, r.y, r.z, 0,
                u.x, u.y, u.z, 0,
                f.x, f.y, f.z, 0,
                0,   0,   0,   1
            );

        }

    }

}