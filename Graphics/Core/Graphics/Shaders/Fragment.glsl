#version 330 core

out vec4 fragColour;

in vec4 vertexColour;
in vec2 uv;

uniform vec4 colour;
uniform sampler2D texture0;

void main() {
    // fragColour = vertexColour;
    fragColour = texture(texture0, uv);
}