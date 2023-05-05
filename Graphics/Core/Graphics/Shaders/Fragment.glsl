#version 330 core

out vec4 fragColour;

in vec4 vertexColour;
in vec2 uv;

//uniform vec4 colour;

uniform sampler2D texture0;
uniform sampler2D texture1;

void main() {
    
    // fragColour = vertexColour;
    // fragColour = vec4(uv, 0.0, 1.0);
    // fragColour = texture(texture1, uv);
    fragColour = mix(texture(texture0, uv), texture(texture1, uv), 0.2);
}