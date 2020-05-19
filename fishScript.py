"""fishScript.py - The python file for the fS shell."""


def parseInt(string=''):
    output = ''
    for p in range(0, len(string)):
        if string[p].isnumeric():
            output += str(string[p])
        elif (string[p] == '.') and ('.' not in output):
            output += '.'
            pass
        pass
    return int(float(output))

def parseFloat(string=''):
    output = ''
    for p in range(0, len(string)):
        if string[p].isnumeric():
            output += str(string[p])
        elif (string[p] == '.') and ('.' not in output):
            output += '.'
            pass
        pass
    return float(output)

def parseRawInt(string=''):
    output = ''
    for p in range(0, len(string)):
        if string[p].isnumeric():
            output += str(string[p])
        elif (string[p] == '.') and ('.' not in output):
            output += ''
            pass
        pass
    return int(float(output))

# Everything below this line is an experiment I'm working on

v_defs = ['$n', '$s', '$f', '$i', '$a', '$t', '$d', '$b', '$x', '$o', '$c', '$l', '$m']
from os import getenv
from sys import platform
if platform.startswith('win32') or platform.startswith('cygwin'):
    libdir = getenv('appdata') + "\\fishScript\\wrapper\\"

def delPartOfString(script, index=0):
    output = ''
    for p in range(0, len(script)):
        if index == p:
            continue
        else:
            output = output + script[p]
    return output

def replaceWord(script=[], word='then', rep=':\n    ', start='', include_spaces=False, does_break=True):
    output = []
    if start == '':
        has_started = True
    else:
        has_started = False
    for p in script:
        if p == word and has_started:
            output.append(rep)
            if does_break:
                break
        elif has_started:
            output.append(p)
        elif p == start:
            has_started = True
    return output

def findLines(script='', char=';', rep='\n'):
    output = ''
    for p in range(0, len(script)):
        if script[p] == char:
            output = output + rep
        else:
            output = output + script[p]
    return output

def subVariables(script=[]):
    for p in range(0, len(script)):
        if script[p].startswith('$'):
            if not script[p] in v_defs:
                try:
                    script[p] = '"%s"' % _varmap[delPartOfString(script[p])]
                except KeyError:
                    print("Name Error: Variable '%s' is not defined." % script[p])
    return script

_varmap = {}
_vararray = []

def runFishScript(script='log Hello World', *args):
    for line in script.splitlines():
        fishScriptLine(line, args)
        pass
    pass

def fishScript(script='-shell', *args):
    if '-shell' not in args and script != '-shell':
        runFishScript(script, *args)
    else:
        while True:
            prompt = input('>>> ')
            if prompt == 'exit' or prompt == 'sys.exit':
                break
            fishScriptLine(prompt)

def fishScriptLine(script, args=()):
    script = script.split()
    if not script:
        return
    if script[0] == 'log':
        sl = ''
        for kw in script[1:len(script)]:
            if sl == '':
                sl = kw
            else:
                sl = sl + ' ' + kw
            pass
        if not script[1].startswith('$'):
            print(sl)
        else:
            try:
                print(_varmap[delPartOfString(script[1])])
            except KeyError:
                print("Name Error: Variable '%s' is not defined." % script[1])
        pass
    elif script[0] == '@' or script[0] == 'py':
        sl = ''
        del script[0]
        for kw in script:
            if sl == '':
                sl = kw
            else:
                sl = sl + ' ' + kw
            pass
        exec(sl)
        pass
    elif script[0] == '$s' or script[0] == 'string':
        sl = ''
        for kw in script[2:len(script)]:
            if not sl == '':
                sl = sl + ' ' + kw
            else:
                sl = kw
            pass
        try:
            if not script[2].startswith('$'):
                _varmap[script[1]] = sl
            else:
                try:
                    _varmap[script[1]] = _varmap[delPartOfString(script[2])]
                except KeyError:
                    print("Name Error: Variable '%s' is not defined." % script[1])
        except IndexError:
            print('Syntax Error: invalid syntax for command $s')
        pass
    elif script[0] == '$i' or script[0] == 'int':
        _varname = script[1]
        del script[0:2]
        sl = ''
        for kw in script:
            if sl == '':
                sl = kw
            else:
                sl = sl + ' ' + kw
        try:
            _varmap[_varname] = int(sl)
        except ValueError:
            print("Type Error: That's not an integer. Try using $f if your number has a decimal point.")
        pass
    elif script[0] == '$f' or script[0] == 'float':
        _varname = script[1]
        del script[0]
        del script[1]
        sl = ''
        for kw in script:
            if sl == '':
                sl = kw
            else:
                sl = sl + ' ' + kw
        try:
            _varmap[_varname] = float(sl)
        except ValueError:
            print("That's not a number. If you want to define a string variable, use $s.")
        pass
    elif script[0] == '$b' or script[0] == 'bool':
        _varname = script[1]
        del script[0]
        del script[1]
        sl = ''
        for kw in script:
            if sl == '':
                sl = kw
            else:
                sl = sl + ' ' + kw
        _varmap[_varname] = bool(sl)
        pass
    elif script[0] == '$n' or script[0] == 'bool':
        _varname = script[1]
        del script[0]
        del script[1]
        _varmap[_varname] = None
        pass
    elif script[0] == '$m' or script[0] == 'function' or script[0] == 'method':
        _varname = script[1]
        del script[0:2]
        code = ''
        for kw in script:
            if code == '':
                code = kw
            else:
                code = code + ' ' + kw
        _varmap[_varname] = 'fishScript("exe %s")' % code
        _vararray.append(_varname)
    elif script[0] == 'exe':
        del script[0]
        sl = ''
        for kw in script:
            if sl == '':
                sl = kw
            else:
                sl = sl + ' ' + kw
        fishScript(findLines(sl))
    elif script[0] == 'if':
        con = subVariables(replaceWord(script))
        _code = replaceWord(script, start='then')
        code = ''
        for kw in _code:
            if code == '':
                code = kw
            else:
                code = code + ' ' + kw
        ye = ''
        for kw in con:
            if ye == '':
                ye = kw
            else:
                ye = ye + ' ' + kw
        eval(ye + 'fishScript("exe %s")' % code)
    elif script[0] == '//' or script[0] == '#':
        pass
    elif script[0] in _vararray:
        exec(_varmap[script[0]])
    else:
        print("CommandError: Unknown command: %s" % script[0])
    pass

fishScript()