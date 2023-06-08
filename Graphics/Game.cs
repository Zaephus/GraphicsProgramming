
using System.Diagnostics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class Game {

        private float frameTime = 0.0167f;
        public float fps {
            get {
                return 1 / frameTime;
            }
            set {
                frameTime = 1 / value;
            }
        }

        private Window window = new Window("ZaephusEngine");

        protected virtual void Initialize() {
            window.Initialize();
        }

        protected virtual void ProcessInput() {

        }

        protected virtual void Update(double _dt) {
            window.Update(_dt);
        }

        protected virtual void Render() {
            window.Render();
        }

        protected virtual void Exit() {
            window.Exit();
        }
        
        // TODO: Add fixed time.
        // TODO: Add frame interpolation.
        public unsafe void Run() {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double lastTime = stopwatch.ElapsedMilliseconds * 0.001f;

            Initialize();
            while(GLFW.WindowShouldClose(window.WindowPtr) == false) {

                double currentTime = stopwatch.ElapsedMilliseconds * 0.001f;
                double dt = currentTime - lastTime;

                ProcessInput();
                
                Update(dt);
                Render();

                lastTime = currentTime;

            }

            Exit();

            stopwatch.Start();

        }
        
    }

}