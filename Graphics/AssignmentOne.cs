
using ZaephusEngine;

public class AssignmentOne : Game {

    private Camera camera = new Camera(CameraProjectionType.Perspective, 45, 0.0f, 0.1f, 100.0f);
    
    // private GameObject cube1 = new GameObject(new MeshRenderer(Primitives.Cube));
    // private GameObject cube2 = new GameObject(new MeshRenderer(Primitives.Cube));
    private GameObject sphere = new GameObject(new MeshRenderer(FileLoader.LoadModel("Resources/Models/uv_sphere.obj")));

    private PointLight light1 = new PointLight(Colour.white, 20.0f);
    // private PointLight light2 = new PointLight(Colour.blue, 100.0f);

    private DirectionalLight sun = new DirectionalLight(Colour.yellow);

    private Material cubeMat1 = new Material {
        ObjectColour = Colour.brown,
        AmbientStrength = 0.1f
    };
    // private Material cubeMat2 = new Material {
    //     DiffuseMap = new Texture2D("Resources/Textures/Crate.png"),
    //     SpecularMap = new Texture2D("Resources/Textures/Crate_Specular.png"),
    //     AmbientStrength = 0.1f
    // };

    private Material sphereMat = new Material {
        ObjectColour = Colour.red,
        AmbientStrength = 0.1f
    };

    protected override void Start() {

        camera.transform.position = new Vector3(2.2f, 0.0f, -3.0f);
        camera.transform.Rotate(0.0f, -30.0f, 0.0f);

        // cube1.transform.position = new Vector3(0.0f, -0.75f, 0.0f);
        // cube1.transform.rotation = Quaternion.FromEuler(0, -25, 0);

        // cube2.transform.position = new Vector3(0.0f, 0.75f, 0.0f);
        // cube2.transform.rotation = Quaternion.FromEuler(0, -25, 0);

        light1.transform.position = new Vector3(1.6f, 0.0f, 0.0f);
        light1.transform.scale = Vector3.one * 0.2f;

        // light2.transform.position = new Vector3(-0.6f, 0.0f, 0.2f);
        // light2.transform.scale = Vector3.one * 0.2f;
        
        sun.transform.rotation = Quaternion.FromEuler(-60, 10, 0);

        // cube1.GetComponent<MeshRenderer>().material = cubeMat1;
        // cube2.GetComponent<MeshRenderer>().material = cubeMat2;

        sphere.GetComponent<MeshRenderer>().material = sphereMat;

    }

    protected override void Update(float _dt) {

        // cube1.transform.Rotate(0.01f, 0.01f, 0.0f);
        // cube2.transform.Rotate(0.01f, 0.01f, 0.0f);
        // light.transform.Rotate(0.01f, 0.01f, 0.0f);

        // light.transform.position += new Vector3(0.0005f, 0.0f, -0.0005f);

        // cube1.Update();
        light1.Update();

        // camera.transform.position += new Vector3(0.0005f, 0.0f, 0.0002f);
        // camera.transform.Rotate(0.0f, 0.0f, 0.0f);

    }

    protected override void Exit() {
        // cube1.Exit();
        light1.Exit();
    }

}