
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

    // private GameObject capsule = new(new MeshRenderer(Primitives.capsule));
    // private GameObject sphere = new(new MeshRenderer(Primitives.sphere));
    private GameObject cube = new(new MeshRenderer(Primitives.cube));
    // private GameObject quad = new(new MeshRenderer(Primitives.quad));
    // private GameObject cylinder = new(new MeshRenderer(Primitives.cylinder));

    private GameObject windmill = new(new MeshRenderer(Model.Load("Resources\\Models\\M_Windmill_Var2_4 v2.fbx")));

    private LitMaterial greenMat = new() {
        ObjectColour = Colour.green,
        AmbientStrength = 0.8f
    };

    private LitMaterial cubeMat2 = new() {
        DiffuseMap = new Texture2D("Resources/Textures/Crate.png"),
        SpecularMap = new Texture2D("Resources/Textures/Crate_Specular.png"),
        AmbientStrength = 0.8f
    };

    private UnlitMaterial windmillMat = new() {
        DiffuseMap = new Texture2D("Resources\\Textures\\Windmill_Atlas.png")
    };

    private Material testMat = new(Shader.test);

    protected override void Start() {

        camera.AddComponent(new CameraController());
        camera.transform.position = new Vector3(0.0f, 0.0f, 3.0f);

        sun.transform.rotation = Quaternion.FromEuler(120.0f, 20.0f, -20.0f);

        // capsule.GetComponent<MeshRenderer>().material = cubeMat2;

        // sphere.GetComponent<MeshRenderer>().material = cubeMat2;
        // sphere.transform.position = new Vector3(1.0f, 0.0f, 0.0f);

        // Console.WriteLine(windmill.GetComponent<MeshRenderer>().materials.GetType());

        windmill.GetComponent<MeshRenderer>().material.renderFace = RenderFace.All;
        windmill.GetComponent<MeshRenderer>().material.SetFloat("material.ambientStrength", 0.6f);
        windmill.GetComponent<MeshRenderer>().materials[1] = windmillMat;

        // cube.GetComponent<MeshRenderer>().material = windmillMat;
        cube.transform.position = new Vector3(2.0f, 0.0f, 0.0f);

        // quad.GetComponent<MeshRenderer>().material = cubeMat2;
        // quad.transform.position = new Vector3(3.0f, 0.0f, 0.0f);
        
        // cylinder.GetComponent<MeshRenderer>().material = cubeMat2;
        // cylinder.transform.position = new Vector3(4.0f, 0.0f, 0.0f);


        window.CursorState = CursorState.Grabbed;

    }

    protected override void Update(float _dt) {

    }

}