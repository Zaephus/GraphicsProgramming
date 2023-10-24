
using ZaephusEngine;
using Random = ZaephusEngine.Random;

public class ScenicScene : Game {

    private Camera camera = new() {
        projectionType = Camera.ProjectionType.Perspective,
        backgroundType = Camera.BackgroundType.Skybox,
        mainColour = Colour.blue,
        secondColour = Colour.cyan
    };

    private DirectionalLight sun = new(new(250f/255f, 1.0f, 210f/255f));
    
    private static LitMaterial floorMat = new() {
        ObjectColour = new(65f/255f, 170f/255f, 60f/255f),
        AmbientStrength = 0.4f
    };
    private GameObject floor = new(new MeshRenderer(Primitives.quad) { material = floorMat } );

    private static LitMaterial windmillMat = new() {
        DiffuseMap = new Texture2D("Resources\\Textures\\Windmill_Atlas.png"),
        AmbientStrength = 0.4f,
        SpecularStrength = 0.4f
    };

    private GameObject[] windmills = {
        new(new MeshRenderer(Model.Load("Resources\\Models\\M_Windmill_Var2_4 v2.fbx"))),
        new(new MeshRenderer(Model.Load("Resources\\Models\\M_Windmill_Var2_4 v2.fbx"))),
        new(new MeshRenderer(Model.Load("Resources\\Models\\M_Windmill_Var2_4 v2.fbx"))),
        new(new MeshRenderer(Model.Load("Resources\\Models\\M_Windmill_Var2_4 v2.fbx"))),
        new(new MeshRenderer(Model.Load("Resources\\Models\\M_Windmill_Var2_4 v2.fbx"))),
        new(new MeshRenderer(Model.Load("Resources\\Models\\M_Windmill_Var2_4 v2.fbx"))),
        new(new MeshRenderer(Model.Load("Resources\\Models\\M_Windmill_Var2_4 v2.fbx"))),
        new(new MeshRenderer(Model.Load("Resources\\Models\\M_Windmill_Var2_4 v2.fbx")))
    };

    protected override void Start() {

        camera.transform.position = new(0.0f, 2.0f, 0.0f);

        sun.transform.rotation = Quaternion.FromEuler(120.0f, 20.0f, -20.0f);

        floor.transform.scale = Vector3.one * 1000f;

        foreach(GameObject go in windmills) {
            go.GetComponent<MeshRenderer>().material.renderFace = RenderFace.All;
            go.GetComponent<MeshRenderer>().material.SetFloat("material.ambientStrength", 0.4f);
            go.GetComponent<MeshRenderer>().materials[1] = windmillMat;
            go.transform.rotation = Quaternion.FromEuler(0.0f, Random.Range(-10.0f, 40.0f), 0.0f);
        }

        windmills[0].transform.position = new(-13.0f, 0.0f, -30.0f);
        windmills[1].transform.position = new(4.0f, 0.0f, -24.0f);
        windmills[2].transform.position = new(0.5f, 0.0f, -10.0f);
        windmills[3].transform.position = new(2.0f, 0.0f, -6.0f);
        windmills[4].transform.position = new(-8.0f, 0.0f, -14.0f);
        windmills[5].transform.position = new(-3.5f, 0.0f, -20.0f);
        windmills[6].transform.position = new(6.0f, 0.0f, -12.0f);
        windmills[7].transform.position = new(-2.5f, 0.0f, -7.0f);

    }

    protected override void Update(float _dt) {

    }

}