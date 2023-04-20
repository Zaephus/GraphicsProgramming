
#include <GLFW/glfw3.h>
#include <iostream>
using namespace std;

int main()
{
    cout<<"A nondescript C++ program using the GLFW library, coded and built using VSCode"<<endl;
    cout<<"Initializing Graphics Library FramWork (GLFW)"<<endl;
    
    if(!glfwInit())
    {
        return -1;
    }

    GLFWwindow* gameWindow = glfwCreateWindow(640, 480, "GLFW Test App", NULL, NULL);
    if(!gameWindow)
    {
        glfwTerminate();
        return -1;
    }

    glfwMakeContextCurrent(gameWindow);

    // Application (Game) loop
    while(!glfwWindowShouldClose(gameWindow))
    {
        glClearColor(0, 1, 0, 1);
        glClear(GL_COLOR_BUFFER_BIT);
        glfwSwapBuffers(gameWindow);
        glfwPollEvents();
    }

    glfwTerminate();    
    return 0;
}