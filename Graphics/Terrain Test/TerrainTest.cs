
using ZaephusEngine;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Window = ZaephusEngine.Window;
using Math = ZaephusEngine.Math;

public class TerrainTest : Game {

    private Camera camera = new Camera(CameraProjectionType.Perspective, 45, 0.0f, 0.1f, 100.0f);

    private TerrainGenerator generator = new TerrainGenerator {
        baseY = 0.0f,
        amplitude = 1.6f,
        octaves = 5,
        resolution = 100.0f,
        size = new Vector2Int(1000, 1000)
    };

    private DirectionalLight sun = new DirectionalLight(Colour.white);

    private GameObject terrain = new GameObject(new MeshRenderer());
    private Material terrainMat = new Material("Core/Graphics/Shaders/Vertex.glsl", "Core/Graphics/Shaders/TerrainFragment.glsl") {
        ObjectColour = Colour.white,
        AmbientStrength = 0.1f,
        SpecularStrength = 0.1f,
        Shininess = 1.0f
    };

    protected override void Start() {

        camera.AddComponent(new CameraController());
        camera.transform.position = new Vector3(2f, 2.5f, 6f);

        sun.transform.Rotate(-30, 0, 0);
        sun.transform.Rotate(0, 180, 0);

        terrain.GetComponent<MeshRenderer>().mesh = generator.Generate();

        terrain.GetComponent<MeshRenderer>().material = terrainMat;

        Window.HandleInput += HandleInput;
        window.CursorState = CursorState.Grabbed;

    }
    
    // You cannot send values to a shader in Start, because shader binding happens after Start.
    protected override void PostInitialize() {
        terrain.GetComponent<MeshRenderer>().material.shader.SetFloat("maxHeight", generator.amplitude);
        terrain.GetComponent<MeshRenderer>().material.shader.SetColour("colourOne", Colour.blue);
        terrain.GetComponent<MeshRenderer>().material.shader.SetColour("colourTwo", Colour.green);
        terrain.GetComponent<MeshRenderer>().material.shader.SetColour("colourThree", new Colour(0.0f, 0.6f, 0.05f));
        terrain.GetComponent<MeshRenderer>().material.shader.SetColour("colourFour", Colour.grey);
        terrain.GetComponent<MeshRenderer>().material.shader.SetColour("colourFive", Colour.white);
    }

    protected override void Update(float _dt) {
        camera.Update(_dt);
    }

    private void HandleInput(KeyboardState _keyState, MouseState _mouseState) {
        if(_keyState.IsKeyDown(Keys.Space)) {
            terrain.GetComponent<MeshRenderer>().mesh = generator.Generate();
        }
    }

    protected override void Exit() {}
    
}