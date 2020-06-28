How to use fishScript
===

A Hello World tutorial in fishScript
---

In order to start programming with fishScript, you'll need a text editor. I'm using VS Code, but Notepad will be fine for this.  
Create a new file called `HelloWorld.fS`, and open it in your text editor.  
The next thing you'll need to do is write the program. Type the following text into the file:  
```
log Hello, World!
```

Now you need to run it. Download the fishScript compiler from this GitHub repository.  
Then, once you've done that, right-click on `HelloWorld.fS`, click **Open With... > Choose another app > Look for another app on this PC.** and browse to where you downloaded the compiler. 

Ta-daa! You should see a black window with the following text:
```
|\  __|\___     ==========================================================
| \/  _  o \    fishScript Compiler v0.1
| /\  \  ___|   For more info visit https://kettle3d.github.io/fishScript/
|/  \______/    ==========================================================

Hello, World!
Process ended with exit code 0.

Press any key to close this window...
```

This is what your program does. The `log` command is used to print text to the screen, sort of like `Console.WriteLine()` in Basic.  
As a matter of fact, that's how I made the log command.
