
using OpenTK.Graphics.OpenGL4;

namespace ZaephusEngine {

    public class Camera : GameObject {

        public enum ProjectionType {
            Perspective = 0,
            Orthographic = 1
        }

        public enum BackgroundType {
            SolidColour = 1,
            Skybox = 2
        }

        public static Camera activeCamera;

        public Matrix4x4 ViewMatrix {
            get {
                Matrix4x4 t = Matrix4x4.TranslateMatrix(-transform.position);
                Matrix4x4 d = CreateViewMatrix();
                return d * t;
            }
        }

        public Matrix4x4 ProjectionMatrix { get; private set; } = Matrix4x4.identity;

        public required ProjectionType projectionType;

        public float fovY = 45.0f;
        public float camSize = 0.0f;
        public float nearPlane = 0.1f;
        public float farPlane = 1000.0f;

        public BackgroundType backgroundType = BackgroundType.SolidColour;

        public Colour mainColour = Colour.black;
        public Colour secondColour = Colour.black;

        private GameObject skybox = null;

        public Camera() {
            activeCamera ??= this;
        }

        protected override void Start() {
            Window.WindowResized += OnWindowResized;
            
            OnWindowResized(Window.width, Window.height);

            GL.ClearColor(mainColour);
            
            if(backgroundType == BackgroundType.Skybox) {
                skybox = new GameObject(new MeshRenderer(Primitives.sphere) {
                    renderOrder = RenderOrder.Late,
                    material = new Material(new Shader("Core/Graphics/Shaders/SkyboxVertex.glsl", "Core/Graphics/Shaders/SkyboxFragment.glsl"))
                });
                skybox.GetComponent<MeshRenderer>().material.renderFace = RenderFace.Back;
                skybox.GetComponent<MeshRenderer>().material.SetColour("topColour", mainColour);
                skybox.GetComponent<MeshRenderer>().material.SetColour("botColour", secondColour);
            }
            
        }
        
        protected override void Update(float _dt) {
            if(backgroundType == BackgroundType.Skybox) {
                skybox.transform.position = transform.position;
            }
        }

        private void OnWindowResized(int _width, int _height) {
            switch(projectionType) {
                case ProjectionType.Perspective:
                    ProjectionMatrix = CalculatePerspectiveProjection(fovY, (float)_width / (float)_height, nearPlane, farPlane);
                    break;
                
                case ProjectionType.Orthographic:
                    ProjectionMatrix = CalculateOrthoGraphicProjection(camSize, (float)_width / (float)_height, nearPlane, farPlane);
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