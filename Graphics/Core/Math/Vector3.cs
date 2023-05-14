
using System;
using System.Collections;
using System.Collections.Generic;

namespace ZaephusEngine {

    // TODO: Add IFormattable to Vector3 class.
    // TODO: Add epsilon to Vector3 class.
    public struct Vector3 : IEquatable<Vector3> {

        public float x;
        public float y;
        public float z;

        public float this[int _index] {
            get {
                switch(_index) {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index.");
                }
            }
            set {
                switch(_index) {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index.");
                }
            }
        }

        public Vector3(float _x, float _y) : this(_x, _y, 0f) {}
        public Vector3(float _x, float _y, float _z) {
            x = _x;
            y = _y;
            z = _z;
        }

        public float magnitude {
            get {
                return (float)MathF.Sqrt(x*x + y*y + z*z);
            }
        }

        public Vector3 normalized {
            get {
                Vector3 norm = new Vector3(x, y, z);
                norm.Normalize();
                return norm;
            }
        }

        public void Normalize() {
            this = this / magnitude;
        }

        public static float Distance(Vector3 _lhs, Vector3 _rhs) {
            return MathF.Sqrt(((_rhs.x - _lhs.x) * (_rhs.x - _lhs.x)) + ((_rhs.y - _lhs.y) * (_rhs.y - _lhs.y)) + ((_rhs.z - _lhs.z) * (_rhs.z - _lhs.z)));
        }

        public static float Dot(Vector3 _lhs, Vector3 _rhs) {
            return _lhs.x * _rhs.x + _lhs.y * _rhs.y + _lhs.z * _rhs.z;
        }

        public static Vector3 Cross(Vector3 _lhs, Vector3 _rhs) {
            return new Vector3(
                _lhs.y * _rhs.z - _lhs.z * _rhs.y,
                _lhs.z * _rhs.x - _lhs.x * _rhs.z,
                _lhs.x * _rhs.y - _lhs.y * _rhs.x
            );
        }

        public static float Angle(Vector3 _from, Vector3 _to) {
            float dot = Vector3.Dot(_from, _to);
            return MathF.Acos(dot / (_from.magnitude * _to.magnitude));
        }

        public override int GetHashCode() {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }

        public override bool Equals(object _other) {
            if(!(_other is Vector3)) {
                return false;
            }
            return Equals((Vector3)_other);
        }

        public bool Equals(Vector3 _other) {
            return x == _other.x && y == _other.y && z == _other.z;
        }

        public override string ToString() {
            return $"x: {x}, y: {y}, z: {z}";
        }

        public static Vector3 operator+(Vector3 _lhs, Vector3 _rhs) {
            return new Vector3(_lhs.x + _rhs.x, _lhs.y + _rhs.y, _lhs.z + _rhs.z);
        }

        public static Vector3 operator-(Vector3 _lhs, Vector3 _rhs) {
            return new Vector3(_lhs.x - _rhs.x, _lhs.y - _rhs.y, _lhs.z - _rhs.z);
        }

        public static Vector3 operator-(Vector3 _v) {
            return -1 * _v;
        }

        public static Vector3 operator*(Vector3 _lhs, Vector3 _rhs) {
            return new Vector3(_lhs.x * _rhs.x, _lhs.y * _rhs.y, _lhs.z * _rhs.z);
        }

        public static Vector3 operator*(Vector3 _lhs, float _rhs) {
            return new Vector3(_lhs.x * _rhs, _lhs.y * _rhs, _lhs.z * _rhs);
        }

        public static Vector3 operator*(float _rhs, Vector3 _lhs) {
            return new Vector3(_lhs.x * _rhs, _lhs.y * _rhs, _lhs.z * _rhs);
        }

        public static Vector3 operator/(Vector3 _lhs, float _rhs) {
            return new Vector3(_lhs.x / _rhs, _lhs.y / _rhs, _lhs.z / _rhs);
        }

        public static bool operator==(Vector3 _lhs, Vector3 _rhs) {
            return _lhs.x == _rhs.x && _lhs.y == _rhs.y && _lhs.z == _rhs.z;
        }

        public static bool operator!=(Vector3 _lhs, Vector3 _rhs) {
            return !(_lhs == _rhs);
        }

        public static Vector3 RandomVector(float _minInclusive, float _maxExclusive) {
            return new Vector3(Random.Range(_minInclusive, _maxExclusive),
                               Random.Range(_minInclusive, _maxExclusive),
                               Random.Range(_minInclusive, _maxExclusive));
        }

        public static Vector3 zero { get { return new Vector3(0, 0, 0); } }
        public static Vector3 one { get { return new Vector3(1, 1, 1); } }
        public static Vector3 right { get { return new Vector3(1, 0, 0); } }
        public static Vector3 up { get { return new Vector3(0, 1, 0); } }
        public static Vector3 forward { get { return new Vector3(0, 0, 1); } }

    }

}