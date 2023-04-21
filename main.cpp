
#include <glad/glad.h>
#include <GLFW/glfw3.h>

#include <iostream>
using namespace std;

#include "Core/Math.h"

void framebuffer_size_callback(GLFWwindow* _window, int _width, int _height) {
    glViewport(0, 0, _width, _height);
}

void processInput(GLFWwindow* _window) {
    if(glfwGetKey(_window, GLFW_KEY_ESCAPE) == GLFW_PRESS) {
        glfwSetWindowShouldClose(_window, true);
    }
}

int main() {

    Vector2 vec = Vector2(1, 2);

    cout << "Vec.x = " << vec.x << endl;
    cout << "Magnitude = " << vec.GetMagnitude() << endl;

    vec.Normalize();

    cout << "New Vec.x = " << vec.x << endl;
    cout << "New Magnitude = " << vec.GetMagnitude() << endl;
    
    if(!glfwInit()) {
        return -1;
    }
    glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
    glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
    glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

    GLFWwindow* window = glfwCreateWindow(640, 480, "GLFW Test App", NULL, NULL);
    if(window == NULL) {
        cout << "Failed to create GLFW window." << endl;
        glfwTerminate();
        return -1;
    }

    glfwMakeContextCurrent(window);

    if(!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress)) {
        cout << "Failed to initialize GLAD" << endl;
        return -1;
    }

    glViewport(0, 0, 640, 480);
    glfwSetFramebufferSizeCallback(window, framebuffer_size_callback);

    while(!glfwWindowShouldClose(window)) {

        processInput(window);

        glClearColor(0, 1, 0, 1);
        glClear(GL_COLOR_BUFFER_BIT);
        
        glfwSwapBuffers(window);
        glfwPollEvents();

    }

    glfwTerminate();

    return 0;

}