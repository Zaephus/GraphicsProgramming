
using System;
using System.Collections;
using System.Collections.Generic;

namespace ZaephusEngine {

    // TODO: Add IFormattable to Vector3Int class.
    // TODO: Add epsilon to Vector3Int class.
    public struct Vector3Int : IEquatable<Vector3Int> {

        public int x;
        public int y;
        public int z;

        public int this[int _index] {
            get {
                switch(_index) {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3Int index.");
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
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3Int index.");
                }
            }
        }

        public Vector3Int(float _x, float _y) : this((int)MathF.Round(_x), (int)MathF.Round(_y), 0) {}
        public Vector3Int(float _x, float _y, float _z) : this((int)MathF.Round(_x), (int)MathF.Round(_y), (int)MathF.Round(_z)) {}
        public Vector3Int(int _x, int _y) : this(_x, _y, 0) {}
        public Vector3Int(int _x, int _y, int _z) {
            x = _x;
            y = _y;
            z = _z;
        }

        public float magnitude {
            get {
                return (float)MathF.Sqrt(x*x + y*y + z*z);
            }
        }

        public Vector3Int normalized {
            get {
                Vector3Int norm = new Vector3Int(x, y, z);
                norm.Normalize();
                return norm;
            }
        }

        public void Normalize() {
            this = this / (int)MathF.Round(magnitude);
        }

        public static float Distance(Vector3Int _lhs, Vector3Int _rhs) {
            return MathF.Sqrt(((_rhs.x - _lhs.x) * (_rhs.x - _lhs.x)) + ((_rhs.y - _lhs.y) * (_rhs.y - _lhs.y)) + ((_rhs.z - _lhs.z) * (_rhs.z - _lhs.z)));
        }

        public static float Dot(Vector3Int _lhs, Vector3Int _rhs) {
            return _lhs.x * _rhs.x + _lhs.y * _rhs.y + _lhs.z * _rhs.z;
        }

        public static Vector3Int Cross(Vector3Int _lhs, Vector3Int _rhs) {
            return new Vector3Int(
                _lhs.y * _rhs.z - _lhs.z * _rhs.y,
                _lhs.z * _rhs.x - _lhs.x * _rhs.z,
                _lhs.x * _rhs.y - _lhs.y * _rhs.x
            );
        }

        public static float Angle(Vector3Int _from, Vector3Int _to) {
            float dot = Dot(_from, _to);
            return MathF.Acos(dot / (_from.magnitude * _to.magnitude));
        }

        public override int GetHashCode() {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }

        public override bool Equals(object _other) {
            if(!(_other is Vector3Int)) {
                return false;
            }
            return Equals((Vector3Int)_other);
        }

        public bool Equals(Vector3Int _other) {
            return x == _other.x && y == _other.y && z == _other.z;
        }

        public override string ToString() {
            return $"x: {x}, y: {y}, z: {z}";
        }

        public static Vector3Int operator+(Vector3Int _lhs, Vector3Int _rhs) {
            return new Vector3Int(_lhs.x + _rhs.x, _lhs.y + _rhs.y, _lhs.z + _rhs.z);
        }

        public static Vector3Int operator-(Vector3Int _lhs, Vector3Int _rhs) {
            return new Vector3Int(_lhs.x - _rhs.x, _lhs.y - _rhs.y, _lhs.z - _rhs.z);
        }

        public static Vector3Int operator-(Vector3Int _v) {
            return -1 * _v;
        }

        public static Vector3Int operator*(Vector3Int _lhs, Vector3Int _rhs) {
            return new Vector3Int(_lhs.x * _rhs.x, _lhs.y * _rhs.y, _lhs.z * _rhs.z);
        }

        public static Vector3Int operator*(Vector3Int _lhs, float _rhs) {
            return new Vector3Int(_lhs.x * _rhs, _lhs.y * _rhs, _lhs.z * _rhs);
        }

        public static Vector3Int operator*(float _rhs, Vector3Int _lhs) {
            return new Vector3Int(_lhs.x * _rhs, _lhs.y * _rhs, _lhs.z * _rhs);
        }

        public static Vector3Int operator/(Vector3Int _lhs, float _rhs) {
            return new Vector3Int(_lhs.x / _rhs, _lhs.y / _rhs, _lhs.z / _rhs);
        }

        public static bool operator==(Vector3Int _lhs, Vector3Int _rhs) {
            return _lhs.x == _rhs.x && _lhs.y == _rhs.y && _lhs.z == _rhs.z;
        }

        public static bool operator!=(Vector3Int _lhs, Vector3Int _rhs) {
            return !(_lhs == _rhs);
        }

        public static Vector3Int RandomVector(int _minInclusive, int _maxInclusive) {
            return new Vector3Int(Random.Range(_minInclusive, _maxInclusive),
                               Random.Range(_minInclusive, _maxInclusive),
                               Random.Range(_minInclusive, _maxInclusive));
        }

        public static Vector3Int zero { get { return new Vector3Int(0, 0, 0); } }
        public static Vector3Int one { get { return new Vector3Int(1, 1, 1); } }
        public static Vector3Int right { get { return new Vector3Int(1, 0, 0); } }
        public static Vector3Int left { get { return new Vector3Int(-1, 0, 0); } }
        public static Vector3Int up { get { return new Vector3Int(0, 1, 0); } }
        public static Vector3Int down { get { return new Vector3Int(0, -1, 0); } }
        public static Vector3Int forward { get { return new Vector3Int(0, 0, 1); } }
        public static Vector3Int back { get { return new Vector3Int(0, 0, -1); } }

    }

}