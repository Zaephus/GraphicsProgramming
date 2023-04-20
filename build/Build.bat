@echo off

@echo Building GLFW Application...

for /r %%a in (*.cpp) do (
    echo    Found file: %%~na%%~xa 
    g++ -g -I. -c -o bin/%%~na.o %%~pa%%~na%%~xa
)
for /r %%a in (*.c) do (
    echo    Found file: %%~na%%~xa
    g++ -g -I. -c -o bin/%%~na.o %%~pa%%~na%%~xa
)

@echo Compilation complete, proceeding to linking...

g++ -mwindows -o bin/main.exe bin/*.o -L. -lglfw3 -lopengl32 -lgdi32

@echo Disposing of temporary files...

for /r "bin" %%a in (*.o) do ( del %%~pa%%~na%%~xa )

@echo Linking complete!