
using System;
using System.Collections;
using System.Collections.Generic;

namespace ZaephusEngine {

    // TODO: Add IFormattable to Vector4 class.
    // TODO: Add epsilon to Vector4 class.
    public struct Vector4 : IEquatable<Vector4> {

        public float x;
        public float y;
        public float z;
        public float w;

        public float this[int _index] {
            get {
                switch(_index) {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    case 3:
                        return w;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4 index.");
                }
            }
            set {
                switch(_index) {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    case 3:
                        w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4 index.");
                }
            }
        }

        public Vector4(Vector2 _v, float _z, float _w) : this(_v.x, _v.y, _z, _w) {}
        public Vector4(Vector3 _v, float _w) : this(_v.x, _v.y, _v.z, _w) {}
        public Vector4(float _x, float _y, float _z, float _w) {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }

        public float magnitude {
            get {
                return (float)MathF.Sqrt(x*x + y*y + z*z + w*w);
            }
        }

        public Vector4 normalized {
            get {
                Vector4 norm = new Vector4(x, y, z, w);
                norm.Normalize();
                return norm;
            }
        }

        public void Normalize() {
            float mag = magnitude;
            this = this / mag;
        }

        public override int GetHashCode() {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
        }

        public override bool Equals(object _other) {
            if(!(_other is Vector4)) {
                return false;
            }
            return Equals((Vector4)_other);
        }

        public bool Equals(Vector4 _other) {
            return x == _other.x && y == _other.y && z == _other.z && w == _other.w;
        }
        
        public override string ToString() {
            return $"x: {x}, y: {y}, z: {z}, w: {w}";
        }

        public static Vector4 operator+(Vector4 _lhs, Vector4 _rhs) {
            return new Vector4(_lhs.x + _rhs.x, _lhs.y + _rhs.y, _lhs.z + _rhs.z, _lhs.w + _rhs.w);
        }

        public static Vector4 operator-(Vector4 _lhs, Vector4 _rhs) {
            return new Vector4(_lhs.x - _rhs.x, _lhs.y - _rhs.y, _lhs.z - _rhs.z, _lhs.w - _rhs.w);
        }

        public static Vector4 operator-(Vector4 _v) {
            return -1 * _v;
        }

        public static Vector4 operator*(Vector4 _lhs, Vector4 _rhs) {
            return new Vector4(_lhs.x * _rhs.x, _lhs.y * _rhs.y, _lhs.z * _rhs.z, _lhs.w * _rhs.w);
        }

        public static Vector4 operator*(Vector4 _v, float _s) {
            return new Vector4(_v.x * _s, _v.y * _s, _v.z * _s, _v.w * _s);
        }

        public static Vector4 operator*(float _s, Vector4 _v) {
            return new Vector4(_v.x * _s, _v.y * _s, _v.z * _s, _v.w * _s);
        }

        public static Vector4 operator/(Vector4 _v, float _s) {
            return new Vector4(_v.x / _s, _v.y / _s, _v.z / _s, _v.w / _s);
        }

        public static bool operator==(Vector4 _lhs, Vector4 _rhs) {
            return _lhs.x == _rhs.x && _lhs.y == _rhs.y && _lhs.z == _rhs.z && _lhs.w == _rhs.w;
        }

        public static bool operator!=(Vector4 _lhs, Vector4 _rhs) {
            return !(_lhs == _rhs);
        }

        public static Vector4 RandomVector(float _minInclusive, float _maxExclusive) {
            return new Vector4(Random.Range(_minInclusive, _maxExclusive),
                               Random.Range(_minInclusive, _maxExclusive),
                               Random.Range(_minInclusive, _maxExclusive),
                               Random.Range(_minInclusive, _maxExclusive));
        }

        public static Vector4 zero { get { return new Vector4(0, 0, 0, 0); } }
        public static Vector4 one { get { return new Vector4(1, 1, 1, 1); } }

    }

}