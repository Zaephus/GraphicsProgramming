
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class Window : GameWindow {

        public static System.Action OnLoadMeshes;
        public static System.Action OnRenderMeshes;
        public static System.Action OnUnloadMeshes;

        public static System.Action<int, int> WindowResized;

        public static readonly int width = 1400;
        public static readonly int height = 800;

        public Colour backgroundColour = Colour.black;

        private Camera camera;
        
        private GameObject cube1 = new GameObject(new MeshRenderer(Primitives.Cube));
        private GameObject cube2 = new GameObject(new MeshRenderer(Primitives.Cube));

        private PointLight light1;
        private PointLight light2;

        private DirectionalLight sun;

        private Material cubeMat1;
        private Material cubeMat2;

        public Window(string _title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = _title }) {}

        protected override void OnLoad() {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(backgroundColour);

            camera = new Camera(CameraProjectionType.Perspective, 45, 0.0f, 0.1f, 100.0f);
            camera.transform.position = new Vector3(2.2f, 0.0f, -3.0f);
            camera.transform.Rotate(0.0f, -30.0f, 0.0f);

            cube1.transform.position = new Vector3(0.0f, -0.75f, 0.0f);
            cube1.transform.rotation = Quaternion.FromEuler(0, -25, 0);

            cube2.transform.position = new Vector3(0.0f, 0.75f, 0.0f);
            cube2.transform.rotation = Quaternion.FromEuler(0, -25, 0);

            light1 = new PointLight(Colour.white, 20.0f);

            light1.transform.position = new Vector3(1.6f, 0.0f, 0.0f);
            light1.transform.scale = Vector3.one * 0.2f;

            light2 = new PointLight(Colour.blue, 100.0f);

            light2.transform.position = new Vector3(-0.6f, 0.0f, 0.2f);
            light2.transform.scale = Vector3.one * 0.2f;

            sun = new DirectionalLight(Colour.yellow);
            sun.transform.rotation = Quaternion.FromEuler(-60, 10, 0);

            cubeMat1 = new Material();
            cubeMat1.ObjectColour = Colour.brown;
            cubeMat1.AmbientStrength = 0.1f;

            cubeMat2 = new Material();
            cubeMat2.DiffuseMap = new Texture2D("Textures/Crate.png");
            cubeMat2.SpecularMap = new Texture2D("Textures/Crate_Specular.png");
            cubeMat2.AmbientStrength = 0.1f;

            cube1.GetComponent<MeshRenderer>().material = cubeMat1;
            cube2.GetComponent<MeshRenderer>().material = cubeMat2;

            OnLoadMeshes?.Invoke();

        }

        protected override void OnRenderFrame(FrameEventArgs _e) {
            base.OnRenderFrame(_e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            OnRenderMeshes?.Invoke();

            SwapBuffers();

        }

        protected override void OnUpdateFrame(FrameEventArgs _e) {
            base.OnUpdateFrame(_e);

            if(KeyboardState.IsKeyDown(Keys.Escape)) {
                Close();
            }

            // cube.transform.Rotate(0.0f, 0.01f, 0.0f);
            // light.transform.Rotate(0.01f, 0.01f, 0.0f);

            // light.transform.position += new Vector3(0.0005f, 0.0f, -0.0005f);
            cubeMat1.shader.SetVector3("lightPosition", light1.transform.position);

            cube1.Update();
            light1.Update();

            // camera.transform.position += new Vector3(0.0005f, 0.0f, 0.0002f);
            // camera.transform.Rotate(0.0f, 0.0f, 0.0f);

        }

        protected override void OnResize(ResizeEventArgs _e) {
            base.OnResize(_e);

            WindowResized?.Invoke(_e.Width, _e.Height);

            GL.Viewport(0, 0, _e.Width, _e.Height);

        }

        protected override void OnUnload() {
            OnUnloadMeshes?.Invoke();

            cube1.Exit();
            light1.Exit();
        }

    }

}