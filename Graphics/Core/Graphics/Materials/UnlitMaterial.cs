

namespace ZaephusEngine {

    public class UnlitMaterial : Material {

        public UnlitMaterial() : base(Shader.unlit) {
            isLit = false;
        }

        public override void Initialize() {
            base.Initialize();

            ObjectColour = ObjectColour;
            DiffuseMap = DiffuseMap;
        }

        private Colour objectColour = Colour.white;
        public Colour ObjectColour {
            get {
                return objectColour;
            }
            set {
                objectColour = value;
                if(isInitialized) {
                    SetColour("material.colour", objectColour);
                }
            }
        }

        private Texture2D diffuseMap = new(Colour.white);
        public Texture2D DiffuseMap {
            get {
                return diffuseMap;
            }
            set {
                diffuseMap = value;
                if(isInitialized) {
                    SetTexture2D("material.diffuseMap", diffuseMap);
                }
            }
        }

    }

}