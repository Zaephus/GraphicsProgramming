
using ZaephusEngine;

public class LoadedObjectTest : Game {

    private Camera camera = new Camera(CameraProjectionType.Perspective, 45, 0.0f, 0.1f, 100.0f);
    private GameObject dagger = new GameObject(new MeshRenderer(FileLoader.LoadModel("Resources/Models/SM_KnightsDagger/SM_KnightsDagger.obj")));

    private DirectionalLight sun = new DirectionalLight(Colour.white);

    private Material redMat = new Material {
        ObjectColour = Colour.red,
        AmbientStrength = 0.1f
    };

    protected override void Start() {
        camera.transform.position = new Vector3(0.0f, 0.0f, 3.5f);
        
        sun.transform.Rotate(0.0f, 120.0f, 0.0f);
        sun.transform.Rotate(60.0f, 0.0f, 0.0f);

        dagger.transform.rotation = Quaternion.FromEuler(90.0f, 0.0f, 0.0f);
        dagger.transform.scale = Vector3.one * 0.25f;
        // sphere.GetComponent<MeshRenderer>().material = redMat;
    }

    protected override void Update(float _dt) {
        // sun.transform.Rotate(0.0f, 50.0f * _dt, 0.0f);
        dagger.transform.Rotate(80.0f * _dt, 0.0f, 0.0f);
    }

    protected override void Exit() {}

}