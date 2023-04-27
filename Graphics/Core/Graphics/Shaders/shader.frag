#version 330 core

uniform vec4 colour;
out vec4 fragColour;

in vec4 vertexColour;

void main() {
    fragColour = vertexColour;
}