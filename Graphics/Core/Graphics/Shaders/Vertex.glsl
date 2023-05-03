#version 330 core

layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec4 inColour;
layout(location = 2) in vec2 inUV;
layout(location = 3) in vec3 inNormal;
layout(location = 4) in vec3 inTangent;
layout(location = 5) in vec3 inBiTangent;

out vec4 vertexColour;
out vec2 uv;
out vec3 normal;
out vec3 tangent;
out vec3 biTangent;

uniform mat4 transform;

void main() {
    gl_Position = vec4(inPosition, 1.0) * transform;

    vertexColour = inColour;
    uv = inUV;
    normal = inNormal;
    tangent = inTangent;
    biTangent = inBiTangent;    
}