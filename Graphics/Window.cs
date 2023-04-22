using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

namespace ZaephusEngine {

    public class Window : GameWindow {

        public Window(int _width, int _height, string _title) : base (GameWindowSettings.Default, new NativeWindowSettings() { Size = (_width, _height), Title = _title}) {}

        protected override void OnUpdateFrame(FrameEventArgs _e) {
            
            base.OnUpdateFrame(_e);

            if(KeyboardState.IsKeyDown(Keys.Escape)) {
                Close();
            }
        }

    }

}