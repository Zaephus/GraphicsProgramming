
using ZaephusEngine;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Window = ZaephusEngine.Window;

public class AssignmentOne : Game {

    private Camera camera = new Camera {
        projectionType = Camera.ProjectionType.Perspective,
        backgroundType = Camera.BackgroundType.Skybox,
        mainColour = Colour.blue,
        secondColour = Colour.cyan
    };
    
    private GameObject colouredCube = new GameObject(new MeshRenderer(Primitives.Cube));
    private GameObject texturedCube = new GameObject(new MeshRenderer(Primitives.Cube));

    private PointLight whiteLight = new PointLight(Colour.white, 20.0f);
    private PointLight blueLight = new PointLight(Colour.blue, 100.0f);

    private Material cubeMat1 = new Material {
        ObjectColour = Colour.white,
        AmbientStrength = 0.1f
    };
    private Material cubeMat2 = new Material {
        DiffuseMap = new Texture2D("Resources/Textures/Crate.png"),
        SpecularMap = new Texture2D("Resources/Textures/Crate_Specular.png"),
        AmbientStrength = 0.1f
    };

    protected override void Start() {

        camera.AddComponent(new CameraController());
        camera.transform.position = new Vector3(0.0f, 0.0f, 3.0f);
        // camera.transform.Rotate(0.0f, 30.0f, 0.0f);

        colouredCube.transform.position = new Vector3(0.0f, -0.75f, 0.0f);
        colouredCube.transform.rotation = Quaternion.FromEuler(0, -25, 0);

        texturedCube.transform.position = new Vector3(0.0f, 0.75f, 0.0f);
        texturedCube.transform.rotation = Quaternion.FromEuler(0, -25, 0);

        colouredCube.GetComponent<MeshRenderer>().material = cubeMat1;
        texturedCube.GetComponent<MeshRenderer>().material = cubeMat2;

        whiteLight.transform.position = new Vector3(1.6f, 0.0f, 0.0f);

        blueLight.transform.position = new Vector3(-0.3f, 0.0f, 1.2f);

        window.CursorState = CursorState.Grabbed;

    }

    protected override void Update(float _dt) {
        colouredCube.transform.Rotate(10.0f * _dt, 10.0f * _dt, 0.0f);
        texturedCube.transform.Rotate(10.0f * _dt, 10.0f * _dt, 0.0f);
    }

    protected override void Exit() {}

}