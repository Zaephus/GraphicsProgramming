
using System.Diagnostics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ZaephusEngine {

    public class Game {

        public static Action InitCall;
        public static Action<float> UpdateCall;
        public static Action RenderCall;
        public static Action ExitCall;

        protected Window window = new Window("ZaephusEngine");

        protected virtual void Start() {}
        private void Initialize() {
            Start();
            window.Initialize();
            InitCall?.Invoke();
            PostInitialize();
        }
        protected virtual void PostInitialize() {}

        protected virtual void Update(float _dt) {}
        private void OnUpdate(float _dt) {
            Update(_dt);
            window.Update(_dt);
            UpdateCall?.Invoke(_dt);
        }

        protected virtual void Render() {}
        private void OnRender() {
            Render();
            window.Render();
            RenderCall?.Invoke();
            window.SwapBuffers();
        }

        protected virtual void Exit() {}
        private void OnExit() {
            Exit();
            window.Exit();
            ExitCall?.Invoke();
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
                
                OnUpdate((float)dt);
                OnRender();

                lastTime = currentTime;

            }

            OnExit();

            stopwatch.Stop();

        }
        
    }

}