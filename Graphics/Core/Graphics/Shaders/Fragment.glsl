
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

uniform vec4 lightColour;

uniform vec3 lightPosition;
uniform vec3 viewPosition;

void main() {

    vec4 ambient = material.ambientStrength * lightColour;

    vec3 norm = normalize(normal);
    vec3 lightDir = normalize(lightPosition - fragPosition);

    float diffuseComponent = max(dot(norm, lightDir), 0.0);
    vec4 diffuse = diffuseComponent * lightColour;

    vec3 viewDir = normalize(viewPosition - fragPosition);
    vec3 reflectDir = reflect(-lightDir, norm);

    float specularComponent = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec4 specular = (material.specularStrength * specularComponent * lightColour) * texture(material.specularMap, uv);

    vec4 colour = (ambient + diffuse + specular) * material.colour;

    fragColour = colour * texture(material.diffuseMap, uv);
}