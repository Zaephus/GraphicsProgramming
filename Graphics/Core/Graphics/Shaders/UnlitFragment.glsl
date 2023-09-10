
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

uniform vec3 viewPosition;

void main() {

    vec3 norm = normalize(normal);
    vec3 viewDirection = normalize(viewPosition - fragPosition);

    fragColour = material.colour * texture(material.diffuseMap, uv);

}