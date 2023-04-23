using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class Window : GameWindow {

        public static System.Action OnLoadMeshes;
        public static System.Action OnRenderMeshes;
        public static System.Action OnUnloadMeshes;

        public Colour backgroundColour = Colour.cyan;

        // private Triangle triangle = new Triangle();
        private Hexagon hexagon = new Hexagon(Colour.magenta);

        public Window(int _width, int _height, string _title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (_width, _height), Title = _title}) {}

        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(backgroundColour);

            OnLoadMeshes?.Invoke();

        }

        protected override void OnRenderFrame(FrameEventArgs _e) {
            base.OnRenderFrame(_e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            OnRenderMeshes?.Invoke();

            SwapBuffers();

        }

        protected override void OnUpdateFrame(FrameEventArgs _e) {
            base.OnUpdateFrame(_e);

            if(KeyboardState.IsKeyDown(Keys.Escape)) {
                Close();
            }

        }

        protected override void OnResize(ResizeEventArgs _e) {
            base.OnResize(_e);

            GL.Viewport(0, 0, _e.Width, _e.Height);

        }

        protected override void OnUnload() {
            OnUnloadMeshes?.Invoke();
        }

    }

}