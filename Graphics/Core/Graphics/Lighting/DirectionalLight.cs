

namespace ZaephusEngine {

    public class DirectionalLight : Light {

        public DirectionalLight(Colour _c) : base (_c) {
            LightUtility.dirLights.Add(this);
        }

        ~DirectionalLight() {
            LightUtility.dirLights.Remove(this);
        }

    }
    
}