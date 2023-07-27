
#version 330 core

layout(location = 0) in vec3 inPosition;
layout(location = 1) in vec4 inVertexColour;
layout(location = 2) in vec2 inUV;
layout(location = 3) in vec3 inNormal;
layout(location = 4) in vec3 inTangent;
layout(location = 5) in vec3 inBiTangent;

out vec3 fragPosition;
out vec4 vertexColour;
out vec2 uv;
out vec3 normal;
out vec3 tangent;
out vec3 biTangent;

uniform mat4 modelMatrix;
uniform mat4 viewMatrix;
uniform mat4 projectionMatrix;

uniform mat4 normalMatrix;

void main() {
    vec4 pos = projectionMatrix * viewMatrix * modelMatrix * vec4(inPosition, 1.0);
    gl_Position = pos.xyww;
    fragPosition = vec3(modelMatrix * vec4(inPosition.x, inPosition.y, 1.0, 1.0));

    vertexColour = inVertexColour;
    uv = inUV;
    normal = vec3(normalMatrix * vec4(inNormal, 1.0));
    tangent = inTangent;
    biTangent = inBiTangent;    
}