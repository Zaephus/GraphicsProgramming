
using System;
using System.Collections;
using System.Collections.Generic;

namespace ZaephusEngine {

    // TODO: Add IFormattable to Vector2 class.
    // TODO: Add epsilon to Vector2 class.
    public struct Vector2 : IEquatable<Vector2> {

        public float x;
        public float y;

        public float this[int _index] {
            get {
                switch(_index) {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index.");
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
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index.");
                }
            }
        }

        public Vector2(float _x, float _y) {
            x = _x;
            y = _y;
        }

        public float magnitude {
            get {
                return (float)MathF.Sqrt(x*x + y*y);
            }
        }

        public Vector2 normalized {
            get {
                Vector2 norm = new Vector2(x, y);
                norm.Normalize();
                return norm;
            }
        }

        public void Normalize() {
            this = this / magnitude;
        }

        public void Rotate(float _rad) {
            float a = x * MathF.Cos(_rad) - y * MathF.Sin(_rad);
            float b = x * MathF.Sin(_rad) + y * MathF.Cos(_rad);

            x = a;
            y = b;
        }

        public static float Distance(Vector2 _lhs, Vector2 _rhs) {
            return MathF.Sqrt(((_rhs.x - _lhs.x) * (_rhs.x - _lhs.x)) + ((_rhs.y - _lhs.y) * (_rhs.y - _lhs.y)));
        }

        public static float Dot(Vector2 _lhs, Vector2 _rhs) {
            return _lhs.x * _rhs.x + _lhs.y * _rhs.y;
        }

        public static float Cross(Vector2 _lhs, Vector2 _rhs) {
            return _lhs.y * _rhs.x - _lhs.x * _rhs.y;
        }

        public static float Angle(Vector2 _from, Vector2 _to) {
            float dot = Vector2.Dot(_from, _to);
            return MathF.Acos(dot / (_from.magnitude * _to.magnitude));
        }

        public override int GetHashCode() {
            return x.GetHashCode() ^ (y.GetHashCode() << 2);
        }

        public override bool Equals(object _other) {
            if(!(_other is Vector2)) {
                return false;
            }
            return Equals((Vector2)_other);
        }

        public bool Equals(Vector2 _other) {
            return x == _other.x && y == _other.y;
        }

        public override string ToString() {
            return $"x: {x}, y: {y}";
        }

        public static Vector2 operator+(Vector2 _lhs, Vector2 _rhs) {
            return new Vector2(_lhs.x + _rhs.x, _lhs.y + _rhs.y);
        }

        public static Vector2 operator-(Vector2 _lhs, Vector2 _rhs) {
            return new Vector2(_lhs.x - _rhs.x, _lhs.y - _rhs.y);
        }

        public static Vector2 operator-(Vector2 _v) {
            return -1 * _v;
        }

        public static Vector2 operator*(Vector2 _lhs, Vector2 _rhs) {
            return new Vector2(_lhs.x * _rhs.x, _lhs.y * _rhs.y);
        }

        public static Vector2 operator*(Vector2 _lhs, float _rhs) {
            return new Vector2(_lhs.x * _rhs, _lhs.y * _rhs);
        }

        public static Vector2 operator*(float _rhs, Vector2 _lhs) {
            return new Vector2(_lhs.x * _rhs, _lhs.y * _rhs);
        }

        public static Vector2 operator/(Vector2 _lhs, float _rhs) {
            return new Vector2(_lhs.x / _rhs, _lhs.y / _rhs);
        }

        public static bool operator==(Vector2 _lhs, Vector2 _rhs) {
            return _lhs.x == _rhs.x && _lhs.y == _rhs.y;
        }

        public static bool operator!=(Vector2 _lhs, Vector2 _rhs) {
            return !(_lhs == _rhs);
        }

        public static implicit operator Vector2(Vector3 _v) {
            return new Vector2(_v.x, _v.y);
        }

        public static implicit operator Vector3(Vector2 _v) {
            return new Vector3(_v.x, _v.y, 0);
        }

        public static implicit operator Vector2(Assimp.Vector2D _v) {
            return new Vector2(_v.X, _v.Y);
        }

        public static Vector2 RandomVector(float _minInclusive, float _maxExclusive) {
            return new Vector2(Random.Range(_minInclusive, _maxExclusive),
                               Random.Range(_minInclusive, _maxExclusive));
        }

        public static Vector2 zero { get { return new Vector2(0, 0); } }
        public static Vector2 one { get { return new Vector2(1, 1); } }
        public static Vector2 right { get { return new Vector2(1, 0); } }
        public static Vector2 up { get { return new Vector2(0, 1); } }

    }

}