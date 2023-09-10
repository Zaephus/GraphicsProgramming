
using ZaephusEngine;
using OpenTK.Windowing.Common;

public class TestScene : Game {

    private Camera camera = new() {
        projectionType = Camera.ProjectionType.Perspective,
        backgroundType = Camera.BackgroundType.Skybox,
        mainColour = Colour.blue,
        secondColour = Colour.cyan
    };

    private DirectionalLight sun = new(Colour.white);

    private GameObject test = new(new MeshRenderer(Primitives.cube));

    private StandardMaterial greenMat = new() {
        ObjectColour = Colour.green,
        AmbientStrength = 0.8f
    };

    private StandardMaterial cubeMat2 = new() {
        DiffuseMap = new Texture2D("Resources/Textures/Crate.png"),
        SpecularMap = new Texture2D("Resources/Textures/Crate_Specular.png"),
        AmbientStrength = 0.8f
    };

    private Material testMat = new(Shader.test);

    protected override void Start() {

        camera.AddComponent(new CameraController());
        camera.transform.position = new Vector3(0.0f, 0.0f, 3.0f);

        sun.transform.rotation = Quaternion.FromEuler(20.0f, 20.0f, 20.0f);

        test.GetComponent<MeshRenderer>().material = cubeMat2;

        window.CursorState = CursorState.Grabbed;

    }

    protected override void Update(float _dt) {

    }

}