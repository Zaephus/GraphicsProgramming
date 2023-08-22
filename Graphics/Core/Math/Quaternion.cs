

namespace ZaephusEngine {

    // TODO: Add IFormattable to Quaternion class.
    public struct Quaternion : IEquatable<Quaternion> {

        public float x;
        public float y;
        public float z;
        public float w;

        public float this[int _index] {
            get {
                switch(_index) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index.");
                }
            }
            set {
                switch(_index) {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index.");
                }
            }
        }

        public Quaternion(float _x, float _y, float _z, float _w) {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }

        public float magnitude {
            get {
                return Dot(this, this);
            }
        }

        public Quaternion normalized {
            get {
                return Normalize(this);
            }
        }

        public static Quaternion Normalize(Quaternion _q) {
            float mag = _q.magnitude;
            if(mag == 0) { // TODO: Add Epsilon value.
                return identity;
            }
            return new Quaternion(
                _q.x / mag,
                _q.y / mag,
                _q.z / mag,
                _q.w / mag
            );
        }

        public void Normalize() {
            this = Normalize(this);
        }

        public static float Dot(Quaternion _lhs, Quaternion _rhs) {
            return _lhs.x * _rhs.x + _lhs.y * _rhs.y + _lhs.z * _rhs.z;
        }

        public static Quaternion FromAxisAngle(Vector3 _axis, float _angleRad) {
            Vector3 axis = _axis.normalized;
            return new Quaternion(
                axis.x * MathF.Sin(_angleRad * 0.5f),
                axis.y * MathF.Sin(_angleRad * 0.5f),
                axis.z * MathF.Sin(_angleRad * 0.5f),
                MathF.Cos(_angleRad * 0.5f)
            );
        }

        // TODO: Fix operation order.
        public static Quaternion FromEuler(float _xDeg, float _yDeg, float _zDeg) {
            return FromEuler(new Vector3(_xDeg, _yDeg, _zDeg));
        }
        public static Quaternion FromEuler(Vector3 _eulerDeg) {
            Vector3 euler = _eulerDeg * (MathF.PI / 180);
            return new Quaternion(
                MathF.Sin(euler.x * 0.5f) * MathF.Cos(euler.y * 0.5f) * MathF.Cos(euler.z * 0.5f) - MathF.Cos(euler.x * 0.5f) * MathF.Sin(euler.y * 0.5f) * MathF.Sin(euler.z * 0.5f),
                MathF.Cos(euler.x * 0.5f) * MathF.Sin(euler.y * 0.5f) * MathF.Cos(euler.z * 0.5f) - MathF.Sin(euler.x * 0.5f) * MathF.Cos(euler.y * 0.5f) * MathF.Sin(euler.z * 0.5f),
                MathF.Cos(euler.x * 0.5f) * MathF.Cos(euler.y * 0.5f) * MathF.Sin(euler.z * 0.5f) - MathF.Sin(euler.x * 0.5f) * MathF.Sin(euler.y * 0.5f) * MathF.Cos(euler.z * 0.5f),
                MathF.Cos(euler.x * 0.5f) * MathF.Cos(euler.y * 0.5f) * MathF.Cos(euler.z * 0.5f) - MathF.Sin(euler.x * 0.5f) * MathF.Sin(euler.y * 0.5f) * MathF.Sin(euler.z * 0.5f)
            );
        }

        public Vector3 ToEuler() {
            return ToEuler(this);
        }

        public static Vector3 ToEuler(Quaternion _q) {

            float x = 0;
            float y = MathF.Asin(2* (_q.w * _q.y - _q.x * _q.z));
            float z = 0;

            if(y == MathF.PI * 0.5f) {
                z = -2 * MathF.Atan2(_q.x, _q.w);
            }
            else if(y == -MathF.PI * 0.5f) {
                z = 2 * MathF.Atan2(_q.x, _q.w);
            }
            else {
                x = MathF.Atan2(2 * (_q.w * _q.x + _q.y * _q.z), (_q.w * _q.w - _q.x * _q.x - _q.y * _q.y + _q.z * _q.z));
                z = MathF.Atan2(2 * (_q.w * _q.z + _q.x * _q.y), (_q.w * _q.w + _q.x * _q.x - _q.y * _q.y - _q.z * _q.z));
            }

            return new Vector3(x, y, z) * (180 / MathF.PI);

        }

        public static float AngleBetween(Quaternion _lhs, Quaternion _rhs) {
            float dot = Dot(_lhs, _rhs);
            return MathF.Acos(dot / (_lhs.magnitude * _rhs.magnitude));
        }

        public override int GetHashCode() {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
        }

        public override bool Equals(object _other) {
            if(_other is not Quaternion) {
                return false;
            }
            return Equals((Quaternion)_other);
        }

        public bool Equals(Quaternion _other) {
            return x == _other.x && y == _other.y && z == _other.z && w == _other.w;
        }

        public override string ToString() {
            return $"x: {x}, y: {y}, z: {z}, w: {w}";
        }


        public static Quaternion operator+(Quaternion _lhs, Quaternion _rhs) {
            return new Quaternion(_lhs.x + _rhs.x, _lhs.y + _rhs.y, _lhs.z + _rhs.z, _lhs.w + _rhs.w);
        }

        public static Quaternion operator-(Quaternion _lhs, Quaternion _rhs) {
            return new Quaternion(_lhs.x - _rhs.x, _lhs.y - _rhs.y, _lhs.z - _rhs.z, _lhs.w - _rhs.w);
        }

        public static Quaternion operator-(Quaternion _q) {
            return -1 * _q;
        }

        // Rotate a point around a Quaternion.
        public static Vector3 operator*(Quaternion _rot, Vector3 _vec) {
            Quaternion rotInverse = new Quaternion(-_rot.x, -_rot.y, -_rot.z, _rot.w);
            Quaternion vecQ = new Quaternion(_vec.x, _vec.y, _vec.z, 0);

            Quaternion result = _rot * vecQ * rotInverse;

            return new Vector3(result.x, result.y, result.z);
        }
        public static Quaternion operator*(Quaternion _lhs, Quaternion _rhs) {
            return new Quaternion(
                _lhs.w * _rhs.x + _lhs.x * _rhs.w + _lhs.y * _rhs.z - _lhs.z * _rhs.y,
                _lhs.w * _rhs.y + _lhs.y * _rhs.w + _lhs.z * _rhs.x - _lhs.x * _rhs.z,
                _lhs.w * _rhs.z + _lhs.z * _rhs.w + _lhs.x * _rhs.y - _lhs.y * _rhs.x,
                _lhs.w * _rhs.w - _lhs.x * _rhs.x - _lhs.y * _rhs.y - _lhs.z * _rhs.z
            );
        }

        public static Quaternion operator*(Quaternion _q, float _s) {
            return new Quaternion(_q.x * _s, _q.y * _s, _q.z * _s, _q.w * _s);
        }

        public static Quaternion operator*(float _s, Quaternion _q) {
            return new Quaternion(_q.x * _s, _q.y * _s, _q.z * _s, _q.w * _s);
        }

        public static Quaternion operator/(Quaternion _q, float _s) {
            return new Quaternion(_q.x / _s, _q.y / _s, _q.z / _s, _q.w / _s);
        }

        public static bool operator==(Quaternion _lhs, Quaternion _rhs) {
            return _lhs.x == _rhs.x && _lhs.y == _rhs.y && _lhs.z == _rhs.z && _lhs.w == _rhs.w;
        }

        public static bool operator!=(Quaternion _lhs, Quaternion _rhs) {
            return !(_lhs == _rhs);
        }

        public static Quaternion identity { get { return new Quaternion(0, 0, 0, 1); } }

    }

}