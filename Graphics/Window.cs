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

        public Colour backgroundColour = Colour.cyan;

        // private Hexagon hexagon = new Hexagon(Colour.magenta);
        // private Triangle triangle = new Triangle(Colour.green);
        // private Quad quad = new Quad(new Vector3(0.5f, 0.0f, 0.0f));

        private Camera camera;
        
        private Cube cube1 = new Cube(new Vector3(0.0f, 0.0f, 0.75f));
        private Cube cube2 = new Cube(new Vector3(0.5f, 0.0f, -0.75f));

        private Material mat1;
        private Material mat2;

        // private Cube cube1 = new Cube(new Vector3(0.0f, 0.0f, -1.5f));
        // private Cube cube2 = new Cube(new Vector3(0.5f, 0.5f, -3.0f));

        public Window(string _title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = _title }) {}

        protected override void OnLoad() {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(backgroundColour);

            camera = new Camera(CameraProjectionType.Perspective, 45, 0.0f, 0.1f, 100.0f);
            camera.transform.position = new Vector3(0.0f, 4.0f, 0.0f);
            camera.transform.rotation = Quaternion.FromEuler(91.0f, 0.0f, 0.0f);

            cube1.transform.rotation = Quaternion.FromEuler(45, 45, 0);
            cube2.transform.rotation = Quaternion.FromEuler(45, 45, 0);

            mat1 = new Material(Colour.green);
            mat2 = new Material(new Texture("Textures/container.png"));

            cube1.material = mat1;
            cube2.material = mat2;

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

            // cube1.transform.Rotate(0.01f, 0.01f, 0.0f);
            // cube2.transform.Rotate(0.01f, 0.01f, 0.0f);

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
        }

    }

}