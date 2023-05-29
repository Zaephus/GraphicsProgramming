

namespace ZaephusEngine {

    public class PointLight : Light {

        private float m_range;
        public float range {
            get {
                return m_range;
            }
            set {
                m_range = value;
                CalculateAttenuation(m_range);
            }
        }

        public float constantAttenuation;
        public float linearAttenuation;
        public float quadraticAttenuation;

        public PointLight(Colour _c, float _range) : base (_c) {
            range = _range;
            MeshRenderer mr = AddComponent(new MeshRenderer(Primitives.Cube)) as MeshRenderer;
            mr.material = new Material();
            mr.material.ObjectColour = _c;
            LightUtility.pointLights.Add(this);
        }

        ~PointLight() {
            LightUtility.pointLights.Remove(this);
        }

        private void CalculateAttenuation(float _range) {
            
            // Attenuation values supplied by OGRE 3D wiki:
            // https://wiki.ogre3d.org/tiki-index.php?page=-Point+Light+Attenuation 

            (float dist, float c, float l, float q)[] attValues = {
                (7.0f,    1.0f, 0.7f,    1.8f),
                (13.0f,   1.0f, 0.35f,   0.44f),
                (20.0f,   1.0f, 0.22f,   0.20f),
                (32.0f,   1.0f, 0.14f,   0.07f),
                (50.0f,   1.0f, 0.09f,   0.032f),
                (65.0f,   1.0f, 0.07f,   0.017f),
                (100.0f,  1.0f, 0.045f,  0.0075f),
                (160.0f,  1.0f, 0.027f,  0.0028f),
                (200.0f,  1.0f, 0.022f,  0.0019f),
                (325.0f,  1.0f, 0.014f,  0.0007f),
                (600.0f,  1.0f, 0.007f,  0.0002f),
                (3250.0f, 1.0f, 0.0014f, 0.000007f)
            };
            
            int min = 0;
            int max = 1;

            for(int i = 0; i < attValues.Length; i++) {
                if(attValues[i].dist > _range) {
                    if(i > 0) {
                        min = i-1;
                        max = i;
                    }
                    break;
                }
            }

            float t = (_range - attValues[min].dist) / (attValues[max].dist - attValues[min].dist);
            constantAttenuation = Math.LerpUnclamped(attValues[min].c, attValues[max].c, t);
            linearAttenuation = Math.LerpUnclamped(attValues[min].l, attValues[max].l, t);
            quadraticAttenuation = Math.LerpUnclamped(attValues[min].q, attValues[max].q, t);

        }

    }
    
}