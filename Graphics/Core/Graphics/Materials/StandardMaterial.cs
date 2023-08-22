

namespace ZaephusEngine {

    public class StandardMaterial : Material {

        public StandardMaterial() : base() {}

        public override void Initialize() {
            base.Initialize();

            ObjectColour = ObjectColour;
            AmbientStrength = AmbientStrength;
            SpecularStrength = SpecularStrength;
            Shininess = Shininess;
            DiffuseMap = DiffuseMap;
            SpecularMap = SpecularMap;
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

        private float ambientStrength = 1.0f;
        public float AmbientStrength {
            get {
                return ambientStrength;
            }
            set {
                ambientStrength = value;
                if(isInitialized) {
                    SetFloat("material.ambientStrength", ambientStrength);
                }
            }
        }

        private float specularStrength = 1.0f;
        public float SpecularStrength {
            get {
                return specularStrength;
            }
            set {
                specularStrength = value;
                if(isInitialized) {
                    SetFloat("material.specularStrength", specularStrength);
                }
            }
        }

        private float shininess = 32.0f;
        public float Shininess {
            get {
                return shininess;
            }
            set {
                shininess = value;
                if(isInitialized) {
                    SetFloat("material.shininess", shininess);
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

        private Texture2D specularMap = new(Colour.white);
        public Texture2D SpecularMap {
            get {
                return specularMap;
            }
            set {
                specularMap = value;
                if(isInitialized) {
                    SetTexture2D("material.specularMap", specularMap);
                }
            }
        }

    }

}