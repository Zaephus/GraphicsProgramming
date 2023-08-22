
using ZaephusEngine;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Window = ZaephusEngine.Window;
using Math = ZaephusEngine.Math;

public class TerrainTest : Game {

    private Camera camera = new() {
        projectionType = Camera.ProjectionType.Perspective
    };

    private TerrainGenerator generator = new() {
        baseY = 0.0f,
        amplitude = 1.6f,
        octaves = 5,
        resolution = 100.0f,
        size = new Vector2Int(1000, 1000)
    };

    private DirectionalLight sun = new(Colour.white);

    private GameObject terrain = new(new MeshRenderer());
    private Material terrainMat = new(new Shader("Core/Graphics/Shaders/Vertex.glsl", "Core/Graphics/Shaders/TerrainFragment.glsl"));

    protected override void Start() {

        terrainMat.SetFloat("material.ambientStrength", 0.1f);
        terrainMat.SetFloat("material.specularStrength", 0.1f);
        terrainMat.SetFloat("material.shininess", 1.0f);

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
    // protected override void PostInitialize() {
    //     terrain.GetComponent<MeshRenderer>().material.shader.SetFloat("maxHeight", generator.amplitude);
    //     terrain.GetComponent<MeshRenderer>().material.shader.SetColour("colourOne", Colour.blue);
    //     terrain.GetComponent<MeshRenderer>().material.shader.SetColour("colourTwo", Colour.green);
    //     terrain.GetComponent<MeshRenderer>().material.shader.SetColour("colourThree", new Colour(0.0f, 0.6f, 0.05f));
    //     terrain.GetComponent<MeshRenderer>().material.shader.SetColour("colourFour", Colour.grey);
    //     terrain.GetComponent<MeshRenderer>().material.shader.SetColour("colourFive", Colour.white);
    // }

    protected override void Update(float _dt) {
    }

    private void HandleInput(KeyboardState _keyState, MouseState _mouseState) {
        if(_keyState.IsKeyDown(Keys.Space)) {
            terrain.GetComponent<MeshRenderer>().mesh = generator.Generate();
        }
    }

    protected override void Exit() {}
    
}