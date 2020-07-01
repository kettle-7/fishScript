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

Now you need to run it. Download the fishScript compiler from [here](https://github.com/Kettle3D/fishScript/releases/download/v0.1.2/FS.exe).  
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

A close look at submodules in fishScript
---

Okay, as exciting as a Hello World project is, I think you'd like to learn some more. Type the following code at the start of `HelloWorld.fS`:
```
do friendly
```
When you run this, it will say something roughly along the lines of
```
|\  __|\___     ==========================================================
| \/  _  o \    fishScript Compiler v0.1
| /\  \  ___|   For more info visit https://kettle3d.github.io/fishScript/
|/  \______/    ==========================================================

Hi, Kettle!
Hello, World!
Process ended with exit code 0.

Press any key to close this window...
```
The `do` command tells fishScript to run a *submodule*, and the `friendly` submodule prints out 'Hi, *your windows username*!'.  
A submodule is a piece of code that can be run again and again.  
If you add a number after the name of the submodule, it will run it that many times. If you like, you can define your own submodule and run it like a loop:
```
do friendly

sub chips {
  log Here's an accurate picture of a fish:
  fish
  wait 1
}

do chips 2
wait 5
swim away
```
Note that we've added some new commands here. The `sub` command makes a submodule called `chips`, and the `do` command runs it.  
The `fish` command draws a fish on the screen, and the `wait` command waits the specified number of seconds before continuing.  
The `swim away` command tells fishScript to stop the program and close the window.  
This should print out the following:
```
|\  __|\___     ==========================================================
| \/  _  o \    fishScript Compiler v0.1
| /\  \  ___|   For more info visit https://kettle3d.github.io/fishScript/
|/  \______/    ==========================================================


Hi, Kettle!

Here's an accurate picture of a fish:

|\  __|\___
| \/  _  o \
| /\  \  ___|
|/  \______/


Here's an accurate picture of a fish:

|\  __|\___
| \/  _  o \
| /\  \  ___|
|/  \______/

The program encountered a fatal error on line 11.
Process ended with exit code 90.
```
> *Note: Instead of using the `swim away` command, you can also use `exit`. When using either command, the program prints out those last two lines above and then closes. 'Exit Code 90' in fishScript just means that the developer used the `swim away` command, and fishScript closed the program by raising a special error.*
