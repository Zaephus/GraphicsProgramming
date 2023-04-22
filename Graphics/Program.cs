
using ZaephusEngine;

Vector2 test1 = new Vector2(1, 1);
Vector2 test2 = new Vector2(1, 1);

if(test1.Equals(test2)) {
    Console.WriteLine(true);
    Console.WriteLine(test1.ToString());
}

using(Window window = new Window(800, 600, "ZaephusEngine")) {
    window.Run();
    test1 += new Vector2(0.1f, 0.1f);
    Console.WriteLine(test1.ToString());
}