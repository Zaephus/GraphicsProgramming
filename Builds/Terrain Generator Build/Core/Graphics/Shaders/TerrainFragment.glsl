
#version 330 core

out vec4 fragColour;

in vec3 fragPosition;
in vec4 vertexColour;
in vec2 uv;
in vec3 normal;
in vec3 tangent;
in vec3 biTangent;

uniform vec4 colourOne;
uniform vec4 colourTwo;
uniform vec4 colourThree;
uniform vec4 colourFour;
uniform vec4 colourFive;

uniform float maxHeight;

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
uniform DirLight dirLights [MAX_DIR_LIGHTS];
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

vec4 CalcTerrainColour();

vec4 CalcDirLight(DirLight _light, vec3 _normal, vec3 _viewDir);
vec4 CalcPointLight(PointLight _light, vec3 _normal, vec3 _fragPos, vec3 _viewDir);
// vec4 CalcSpotLight(Light _light, vec3 _normal, vec3 _fragPos, vec3 _viewDir);

void main() {

    vec3 norm = normalize(normal);
    vec3 viewDirection = normalize(viewPosition - fragPosition);

    vec4 lightResult;

    for(int i = 0; i < MAX_DIR_LIGHTS; i++) {
        if(i >= dirLightNum) {
            break;
        }
        lightResult += CalcDirLight(dirLights[i], norm, viewDirection);
    }

    for(int i = 0; i < MAX_POINT_LIGHTS; i++) {
        if(i >= pointLightNum) {
            break;
        }
        lightResult += CalcPointLight(pointLights[i], norm, fragPosition, viewDirection);
    }

    // fragColour = lightResult * material.colour * texture(material.diffuseMap, uv);
    fragColour = lightResult * CalcTerrainColour();
    // fragColour = vec4(maxHeight, maxHeight, maxHeight, 1.0);
    // fragColour = colourOne;
}

vec4 CalcTerrainColour() {
    float heightStep = maxHeight / 10;
    if(fragPosition.y < 2*heightStep) {
        if(fragPosition.y < heightStep) {
            return colourOne;
        }
        return mix(colourOne, colourTwo, (fragPosition.y-heightStep) / heightStep);
    }
    if(fragPosition.y < 4*heightStep) {
        if(fragPosition.y < 3*heightStep) {
            return colourTwo;
        }
        return mix(colourTwo, colourThree, (fragPosition.y-3*heightStep) / heightStep);
    }
    if(fragPosition.y < 6*heightStep) {
        if(fragPosition.y < 5*heightStep) {
            return colourThree;
        }
        return mix(colourThree, colourFour, (fragPosition.y-5*heightStep) / heightStep);
    }
    if(fragPosition.y < 8*heightStep) {
        if(fragPosition.y < 7*heightStep) {
            return colourFour;
        }
        return mix(colourFour, colourFive, (fragPosition.y-7*heightStep) / heightStep);
    }
    return colourFive;
}

vec4 CalcDirLight(DirLight _light, vec3 _normal, vec3 _viewDir) {

    vec3 lightDir = normalize(-_light.direction);
    vec3 reflectDir = reflect(-lightDir, _normal);

    float diffuseComponent = max(dot(_normal, lightDir), 0.0);
    float specularComponent = max(pow(max(dot(_viewDir, reflectDir), 0.0), material.shininess), 0.0);

    vec4 ambient = material.ambientStrength * _light.colour;
    vec4 diffuse = diffuseComponent * _light.colour;
    vec4 specular = material.specularStrength * specularComponent * _light.colour;

    return (ambient + diffuse + specular);

}

vec4 CalcPointLight(PointLight _light, vec3 _normal, vec3 _fragPos, vec3 _viewDir) {

    vec3 lightDir = normalize(_light.position - _fragPos);
    vec3 reflectDir = reflect(-lightDir, _normal);

    float diffuseComponent = max(dot(_normal, lightDir), 0.0);
    float specularComponent = max(pow(max(dot(_viewDir, reflectDir), 0.0), material.shininess), 0.0);
    
    vec4 ambient = material.ambientStrength * _light.colour;
    vec4 diffuse = diffuseComponent * _light.colour;
    vec4 specular = material.specularStrength * specularComponent * _light.colour;

    float dist = length(_light.position - _fragPos);
    float attenuation = 1.0 / (_light.constant + _light.linear * dist + _light.quadratic * dist * dist);

    ambient *= attenuation;
    diffuse *= attenuation;
    specular *= attenuation;

    return (ambient + diffuse + specular);

}