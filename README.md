# How to use fishScript

## A hello world tutorial with fishScript
Here is how to make fishScript run a hello world program.
1) Start up the shell. It should be in a folder named fishScript in your Start Menu.
2) Type the following into the shell and press ENTER: `log Hello World`.
3) Congratulations! You've just made your first fishScript program!

## A closer look at fishScript syntax
Here is what your code just did:

`log` is a **command**. That a piece of code, like a function or a piece of code.  
Everything that follows gets printed to the shell.  
A command can have *parameters*, which are things that the code uses to know, in the case of `log`, what to write to the shell.  
`log` only takes one parameter. This means that it takes the rest of the line as it's parameter.

## How to use variables with fishScript
Now you know how to print a simple string, it's time to do more with fishScript.

*Variables* are like tiny containers that can hold information, such as letters and numbers. Now we're going to create your first variable.  
Open the shell and type the following: `string hello Hello World  
log $hello`. You'll notice that three greater-that symbols (`>>> `) will appear in the shell. This is fishScript asking you what you want it to do. You'll see the words `Hello World` printed to the shell.
