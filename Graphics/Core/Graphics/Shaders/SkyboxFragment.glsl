
#version 330 core

out vec4 fragColour;

in vec3 fragPosition;
in vec4 vertexColour;
in vec2 uv;
in vec3 normal;
in vec3 tangent;
in vec3 biTangent;

struct Material {

    vec4 colour;

    float ambientStrength;
    float specularStrength;
    float shininess;

    sampler2D diffuseMap;
    sampler2D specularMap;

};
uniform Material material;

struct DirLight {
    vec4 colour;
    vec3 direction;
};
#define MAX_DIR_LIGHTS 5
uniform DirLight dirLights[MAX_DIR_LIGHTS];
uniform int dirLightNum;

struct PointLight {
    vec4 colour;
    vec3 position;

    float constant;
    float linear;
    float quadratic;
};
#define MAX_POINT_LIGHTS 10
uniform PointLight pointLights[MAX_POINT_LIGHTS];
uniform int pointLightNum;

uniform vec3 viewPosition;

uniform vec4 topColour;
uniform vec4 botColour;

float map(float value, float min1, float max1, float min2, float max2) {
    return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
}

void main() {

    vec3 viewDirection = normalize(fragPosition - viewPosition);

    vec4 lightResult;

    for(int i = 0; i < MAX_DIR_LIGHTS; i++) {
        if(i >= dirLightNum) {
            break;
        }
        lightResult += dirLights[i].colour * max(pow(dot(-viewDirection, dirLights[i].direction), 128), 0.0);
    }

    fragColour = mix(botColour, topColour, abs(viewDirection.y)) + lightResult;

}