using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class Window : GameWindow {

        public Colour backgroundColour = Colour.cyan;

        private int vertexBufferObject;

        private Triangle triangle = new Triangle();

        public Window(int _width, int _height, string _title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (_width, _height), Title = _title}) {}

        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(backgroundColour);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, triangle.vertices.Length * sizeof(float), triangle.vertices, BufferUsageHint.StaticDraw);

        }

        protected override void OnRenderFrame(FrameEventArgs _e) {
            base.OnRenderFrame(_e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

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

    }

}