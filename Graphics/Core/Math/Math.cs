

namespace ZaephusEngine {

    public static class Math {

        // TODO: Replace MathF functions.

        public const float epsilon = Single.Epsilon;
        

        public static float Deg2Rad {
            get {
                return MathF.PI / 180f;
            }
        }

        public static float Rad2Deg {
            get {
                return 180f / MathF.PI;
            }
        }

        public static float Clamp(float _value, float _min, float _max) {
            if(_value < _min) {
                return _min;
            }
            else if(_value > _max) {
                return _max;
            }
            else {
                return _value;
            }
        }

        public static int Clamp(int _value, int _min, int _max) {
            if(_value < _min) {
                return _min;
            }
            else if(_value > _max) {
                return _max;
            }
            else {
                return _value;
            }
        }

        public static float Clamp01(float _value) {
            if(_value < 0.0f) {
                return 0.0f;
            }
            else if(_value > 1.0f) {
                return 1.0f;
            }
            else {
                return _value;
            }
        }

        public static float Lerp(float _a, float _b, float _t) {
            return _a + (_b - _a) * Clamp01(_t);
        }

        public static float LerpUnclamped(float _a, float _b, float _t) {
            return _a + (_b - _a) * _t;
        }

    }

}