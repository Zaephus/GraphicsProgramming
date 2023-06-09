
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class Window : GameWindow {

        public static System.Action<int, int> WindowResized;

        public static readonly int width = 1400;
        public static readonly int height = 800;

        public Colour backgroundColour = Colour.black;

        public Window(string _title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = _title }) {}

        public void Initialize() {

            Context?.MakeCurrent();
            OnLoad();

            OnResize(new ResizeEventArgs(Size));

            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(backgroundColour);

        }

        public void Render() {
            OnRenderFrame(new FrameEventArgs());
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void Update(float _dt) {
            
            ProcessWindowEvents();
            
            OnUpdateFrame(new FrameEventArgs(_dt));

            if(KeyboardState.IsKeyDown(Keys.Escape)) {
                Close();
            }

        }

        public void Exit() {}

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