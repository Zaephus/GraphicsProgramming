
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
        private GameObject sphere = new GameObject(new MeshRenderer(FileLoader.LoadModel("Resources/Models/uv_sphere.obj")));

        private PointLight light1;
        private PointLight light2;

        private DirectionalLight sun;

        private Material cubeMat1;
        private Material cubeMat2;

        private Material sphereMat;

        public Window(string _title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = _title }) {}

        public void Initialize() {

            Context?.MakeCurrent();
            OnLoad();

            OnResize(new ResizeEventArgs(Size));

            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(backgroundColour);

            camera = new Camera(CameraProjectionType.Perspective, 45, 0.0f, 0.1f, 100.0f);
            camera.transform.position = new Vector3(2.2f, 0.0f, -3.0f);
            camera.transform.Rotate(0.0f, -30.0f, 0.0f);

            cube1.transform.position = new Vector3(0.0f, -0.75f, 0.0f);
            cube1.transform.rotation = Quaternion.FromEuler(0, -25, 0);

            cube2.transform.position = new Vector3(0.0f, 0.75f, 0.0f);
            cube2.transform.rotation = Quaternion.FromEuler(0, -25, 0);

            sphere.transform.scale = Vector3.one * 0.2f;

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
            cubeMat2.DiffuseMap = new Texture2D("Resources/Textures/Crate.png");
            cubeMat2.SpecularMap = new Texture2D("Resources/Textures/Crate_Specular.png");
            cubeMat2.AmbientStrength = 0.1f;

            sphereMat = new Material();
            sphereMat.ObjectColour = Colour.red;
            sphereMat.AmbientStrength = 0.1f;

            cube1.GetComponent<MeshRenderer>().material = cubeMat1;
            cube2.GetComponent<MeshRenderer>().material = cubeMat2;

            sphere.GetComponent<MeshRenderer>().material = sphereMat;

            OnLoadMeshes?.Invoke();

        }

        public void Render() {
            OnRenderFrame(new FrameEventArgs());

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            OnRenderMeshes?.Invoke();

            SwapBuffers();
        }

        public void Update(double _dt) {
            
            ProcessWindowEvents();
            
            OnUpdateFrame(new FrameEventArgs());

            if(KeyboardState.IsKeyDown(Keys.Escape)) {
                Close();
            }

            // cube1.transform.Rotate(0.01f, 0.01f, 0.0f);
            // cube2.transform.Rotate(0.01f, 0.01f, 0.0f);
            // light.transform.Rotate(0.01f, 0.01f, 0.0f);

            // light.transform.position += new Vector3(0.0005f, 0.0f, -0.0005f);
            cubeMat1.shader.SetVector3("lightPosition", light1.transform.position);

            cube1.Update();
            light1.Update();

            // camera.transform.position += new Vector3(0.0005f, 0.0f, 0.0002f);
            // camera.transform.Rotate(0.0f, 0.0f, 0.0f);
        }

        public void Exit() {
            OnUnloadMeshes?.Invoke();

            cube1.Exit();
            light1.Exit();
        }

        private void ProcessWindowEvents() {
            ProcessInputEvents();
            ProcessWindowEvents(IsEventDriven);
        }

        protected override void OnResize(ResizeEventArgs _e) {
            base.OnResize(_e);

            WindowResized?.Invoke(_e.Width, _e.Height);

            GL.Viewport(0, 0, _e.Width, _e.Height);

        }

    }

}