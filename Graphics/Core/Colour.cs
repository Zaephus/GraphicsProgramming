
using OpenTK.Mathematics;

namespace ZaephusEngine {
    
    // TODO: Add IFormattable to Colour class.
    public struct Colour : IEquatable<Colour> {

        public float R;
        public float G;
        public float B;
        public float A;

        public Colour() {
            R = 1;
            G = 1;
            B = 1;
            A = 1;
        }

        public Colour(float _r, float _g, float _b) {
            R = _r;
            G = _g;
            B = _b;
            A = 1;
        }

        public Colour(float _r, float _g, float _b, float _a) {
            R = _r;
            G = _g;
            B = _b;
            A = _a;
        }

        public override int GetHashCode() {
            return ((Vector4)this).GetHashCode();
        }

        public override bool Equals(object _other) {
            if(!(_other is Colour)) {
                return false;
            }
            return Equals((Colour)_other);
        }

        public bool Equals(Colour _other) {
            return R.Equals(_other.R) && G.Equals(_other.G) && B.Equals(_other.B) && A.Equals(_other.A);
        }
    
        public override string ToString() {
            return $"R: {R}, G: {G}, B: {B}, A: {A}";
        }

        public static Colour operator+(Colour _a, Colour _b) {
            return new Colour((float)(_a.R + _b.R), (float)(_a.G + _b.G), (float)(_a.B + _b.B), (float)(_a.A + _b.A));
        }

        public static Colour operator-(Colour _a, Colour _b) {
            return new Colour((float)(_a.R - _b.R), (float)(_a.G - _b.G), (float)(_a.B - _b.B), (float)(_a.A - _b.A));
        }

        public static Colour operator*(Colour _a, Colour _b) {
            return new Colour((float)(_a.R * _b.R), (float)(_a.G * _b.G), (float)(_a.B * _b.B), (float)(_a.A * _b.A));
        }

        public static Colour operator*(Colour _a, float _b) {
            return new Colour((float)(_a.R * _b), (float)(_a.G * _b), (float)(_a.B * _b), (float)(_a.A * _b));
        }

        public static Colour operator*(float _b, Colour _a) {
            return new Colour((float)(_a.R * _b), (float)(_a.G * _b), (float)(_a.B * _b), (float)(_a.A * _b));
        }

        public static Colour operator/(Colour _a, float _b) {
            return new Colour((float)(_a.R / _b), (float)(_a.G / _b), (float)(_a.B / _b), (float)(_a.A / _b));
        }

        public static bool operator==(Colour _lhs, Colour _rhs) {
            return _lhs.R == _rhs.R && _lhs.G == _rhs.G && _lhs.B == _rhs.B && _lhs.A == _rhs.A;
        }

        public static bool operator!=(Colour _lhs, Colour _rhs) {
            return !(_lhs == _rhs);
        }

        public static implicit operator Vector4(Colour _c) {
            return new Vector4(_c.R, _c.G, _c.B, _c.A);
        }

        public static implicit operator Color4(Colour _c) {
            return new Color4(_c.R, _c.G, _c.B, _c.A);
        }

        public static implicit operator Colour(Vector4 _v) {
            return new Colour((float)_v.x, (float)_v.y, (float)_v.z, (float)_v.w);
        }

        public static Colour red { get { return new Colour(1, 0, 0, 1); } }
        public static Colour green { get { return new Colour(0, 1, 0, 1); } }
        public static Colour blue { get { return new Colour(0, 0, 1, 1); } }
        public static Colour white { get { return new Colour(1, 1, 1, 1); } }
        public static Colour black { get { return new Colour(0, 0, 0, 1); } }
        public static Colour yellow { get { return new Colour(1, 0.921f, 0.016f, 1); } }
        public static Colour cyan { get { return new Colour(0, 1, 1, 1); } }
        public static Colour magenta { get { return new Colour(1, 0, 1, 1); } }
        public static Colour grey { get { return new Colour(0.5f, 0.5f, 0.5f, 1); } }
        public static Colour brown { get { return new Colour(0.5f, 0.25f, 0.0f, 1.0f); } }

    }

}