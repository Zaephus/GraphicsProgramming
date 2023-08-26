
using ZaephusEngine;

public class LoadedSphereTest : Game {

    private Camera camera = new Camera {
        projectionType = Camera.ProjectionType.Perspective,
        mainColour = Colour.blue
    };
    private GameObject sphere = new GameObject(new MeshRenderer(Mesh.Load("Resources/Models/uv_sphere.obj", false)));

    private DirectionalLight sun = new DirectionalLight(Colour.white);

    private StandardMaterial redMat = new StandardMaterial {
        ObjectColour = Colour.red,
        AmbientStrength = 0.1f
    };

    protected override void Start() {
        camera.transform.position = new Vector3(2.2f, 0.0f, 3.0f);
        camera.transform.Rotate(0.0f, 30.0f, 0.0f);
        
        sun.transform.rotation = Quaternion.FromEuler(-60, 10, 0);

        sphere.GetComponent<MeshRenderer>().material = redMat;
    }

    protected override void Update(float _dt) {
        sun.transform.Rotate(0.0f, 50.0f * _dt, 0.0f);
    }

    protected override void Exit() {}

}