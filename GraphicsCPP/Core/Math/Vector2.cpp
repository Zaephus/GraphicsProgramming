
#include "Vector2.h"

Vector2::Vector2(float _x, float _y) {
    x = _x;
    y = _y;
}

float Vector2::GetMagnitude() {
    return sqrt(x*x + y*y);
}

void Vector2::Normalize() {
    *this = *this / GetMagnitude();
}

Vector2 Vector2::operator/(float _val) {
    return Vector2(x/_val, y/_val);
}