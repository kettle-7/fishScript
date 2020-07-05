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

Now you need to run it. Download the fishScript compiler from [here](https://kettle3d.github.io/fishScript/win.exe).  
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
> *Note: Instead of using the `swim away` command, you can also use [`exit`](https://kettle3d.github.io/fishScript/#exit). When using either command, the program prints out those last two lines above and then closes. 'Exit Code 90' in fishScript just means that the developer used the `swim away` command, and fishScript closed the program by raising a special error.*

Return values in fishScript
---
In fishScript, commands sometimes have something called a *return value*. This is where the command is asked to do something with a value, for example add 5 to it, and it returns that number plus five. You often want to use that return value, so that's why the `to` keyword is there.  

The `to` command tells fishScript to assing te return value to a variable. For example, the `input` command returns a value entered by the user. So
```
input to myVar
```
would set the variable `myVar` to whatever the user entered. With this information, you could make a simple script like this:
```
do friendly
log What do you think of my new shoes?
input to opinion
sub _ {
 too.
}
add opinion to _ as sentence
log Yeah.
log sentence
```
This would produce the following output:
```
|\  __|\___     ==========================================================
| \/  _  o \    fishScript Compiler v0.1
| /\  \  ___|   For more info visit https://kettle3d.github.io/fishScript/
|/  \______/    ==========================================================

What do you think of my new shoes?
> I think they look great
Yeah.
I think they look great too.
```

Note the use of the `sub` command instead of using `set`. This is because `set` removes all of the spaces at the start of a string, so if you used `set` it would say this:
```
I think they look greattoo.
```
And that's not nice.

fishScript Exit Codes
---
Note how when your program is finished running, it prints this out:
```
Process ended with exit code 0.
```
This is an *exit code*, a number that fishScript generates to tell you why your program stopped. Exit code 0 means that the program did everything you told it to, and finished with no problems. Sometimes, however, it might give you a different exit code. Here are a list of exit codes that fishScript generates:

***
*Exit code 0: Your program finished successfully.  
Exit code 1: Your program found an unknown error. It should print out an error message.  
Exit code 5: Your program found a command, variable, file or submodule that doesn't exist. Try putting the [`debug`](https://kettle3d.github.io/fishScript/#debug) command just before the line that's causing you trouble. This will list all of the variables and submodules that have been defined.  
Exit code 90: You put `swim away` or [`exit`](https://kettle3d.github.io/fishScript/#exit) at the bottom of your code and fishScript did exactly that.*  
***

Even more fishScript
---
You can find more info about fishScript and all of it's commands at the [fishScript Website](https://kettle3d.github.io/fishScript/).
