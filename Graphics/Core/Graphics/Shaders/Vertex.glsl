#version 330 core

layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec4 inColour;
layout(location = 2) in vec2 inUV;
layout(location = 3) in vec3 inNormal;
layout(location = 4) in vec3 inTangent;
layout(location = 5) in vec3 inBiTangent;

out vec4 vertexColour;
out vec2 uv;

void main() {
    gl_Position = vec4(inPosition, 1.0);
    vertexColour = inColour;
    uv = inUV;
    //vertexColour = vec4(inUV, 0.0, 1.0);
}