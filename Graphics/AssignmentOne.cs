
using ZaephusEngine;
using OpenTK.Windowing.Common;

public class AssignmentOne : Game {

    private Camera camera = new() {
        projectionType = Camera.ProjectionType.Perspective,
        backgroundType = Camera.BackgroundType.Skybox,
        mainColour = Colour.blue,
        secondColour = Colour.cyan
    };
    
    // private GameObject colouredCube = new(new MeshRenderer(Mesh.Load("Resources\\Models\\uv_sphere.obj", false)));
    // private GameObject texturedCube = new(new MeshRenderer(Primitives.cube));

    private GameObject backpack = new(new MeshRenderer(Mesh.Load("Resources/Models/backpack/backpack.obj", true)));

    // private GameObject[] cubes = new GameObject[1000];

    private PointLight whiteLight = new(Colour.white, 10.0f);
    private PointLight blueLight = new(Colour.white, 10.0f);

    // private StandardMaterial cubeMat1 = new() {
    //     ObjectColour = Colour.green,
    //     AmbientStrength = 0.8f
    // };
    // private StandardMaterial cubeMat2 = new() {
    //     DiffuseMap = new Texture2D("Resources/Textures/Crate.png"),
    //     SpecularMap = new Texture2D("Resources/Textures/Crate_Specular.png"),
    //     AmbientStrength = 0.8f
    // };

    protected override void Start() {

        camera.AddComponent(new CameraController());
        camera.transform.position = new Vector3(0.0f, 0.0f, 3.0f);
        // camera.transform.Rotate(0.0f, 30.0f, 0.0f);

        // for(int i = 0; i < cubes.Length; i++) {
        //     cubes[i] = new GameObject(new MeshRenderer(Primitives.cube) {
        //         material = new StandardMaterial() {
        //             ObjectColour = Colour.RandomColour(),
        //             AmbientStrength = 0.6f
        //         }
        //         // material = cubeMat1
        //     });
        //     cubes[i].transform.position = Vector3.RandomVector(-50.0f, 50.0f);
        // }

        // colouredCube.transform.position = new Vector3(0.0f, -0.75f, 0.0f);
        // colouredCube.transform.rotation = Quaternion.FromEuler(0, -25, 0);

        // texturedCube.transform.position = new Vector3(0.0f, 0.75f, 0.0f);
        // texturedCube.transform.rotation = Quaternion.FromEuler(0, -25, 0);

        // colouredCube.GetComponent<MeshRenderer>().material = cubeMat1;
        // texturedCube.GetComponent<MeshRenderer>().material = cubeMat2;

        whiteLight.transform.position = new Vector3(1.6f, 0.0f, 0.0f);
        blueLight.transform.position = new Vector3(-0.3f, 0.0f, 1.2f);

        window.CursorState = CursorState.Grabbed;

    }

    protected override void Update(float _dt) {
        // backpack.transform.Rotate(10.0f * _dt, 10.0f * _dt, 0.0f);
        // texturedCube.transform.Rotate(10.0f * _dt, 10.0f * _dt, 0.0f);
    }

}