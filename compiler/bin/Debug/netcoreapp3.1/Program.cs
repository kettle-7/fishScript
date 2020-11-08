using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace fS_Compiler
{
    static class Program
    {
        static List<string> files = new List<string>();

        static string output = "Program.fbi";

        #region Entry Point
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(@"===================================
=    fishScript Compiler v0.1     =
= Compile fishScript to fishBytes =
===================================

Usage:
    fsc [OPTIONS] [FILE]...

Available Options:
    -o    --output  [FILE]
                Use output file FILE. Not currently in use because
                we're not sure how to make it work with multiple
                source files.

    -?    --help
                Display this help and exit.");
                return;
            }

            for (int i = 0; i < args.Length; i++) {
                switch (args[i]) {
                    case "-o": case "--output":
                        output = args[++i];
                        break;

                    case "-?": case "--help":
                        Console.WriteLine(@"===================================
=    fishScript Compiler v0.1     =
= Compile fishScript to fishBytes =
===================================

Usage:
    fsc [OPTIONS] [FILE]...

Available Options:
    -o    --output  [FILE]
                Use output file FILE. Not currently in use because
                we're not sure how to make it work with multiple
                source files.

    -?    --help
                Display this help and exit.");
                        break;

                    default:
                        files.Add(args[i]);
                        break;
                }
            }

            // if (File.Exists(output)) {
            //     File.Delete(output);
            // }

            foreach (string file in files) {
                string[] lines;

                if (File.Exists(file)) {
                    lines = File.ReadAllLines(file);
                }
                else if (File.Exists(Environment.CurrentDirectory + "/" + file)) {
                    lines = File.ReadAllLines(Environment.CurrentDirectory + "/" + file);
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine($"Error: {file}: file not found");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                var o = Path.GetDirectoryName(file) + "/" + Path.GetFileNameWithoutExtension(file) + ".fbi";
                if (File.Exists(o))
                {
                    File.Delete(o);
                }
                new Compiler().Compile(lines, o);
            }
        }
        #endregion
    }

    /// <summary>
    /// A class for handling the compilation of code
    /// </summary>
    public class Compiler {
        /// <summary>
        /// The integers that represent the names of all the variables
        /// </summary>
        public Dictionary<string, int> VariableNames = new Dictionary<string, int>();
        /// <summary>
        /// The last variable name used.
        /// </summary>
        public int LastVariableName;

        string StartCommand = "";

        #region Main public function
        /// <summary>
        /// Compile the specified lines and write to the output file.
        /// </summary>
        /// <returns>The exit code.</returns>
        /// <param name="lines">The lines of fishScript code to compile.</param>
        /// <param name="output">The file to write output data to.</param>
        public byte Compile(string[] lines, string output) {
            byte errorlevel = 0;
            int indentlevel = 0;
            bool ii = false;
            ulong linenumber = 1;
            foreach (string line in lines)
            {
                if (line.Contains("{") && !line.StartsWith("#") && !line.StartsWith("//"))
                {
                    indentlevel += 1;
                    StartCommand = line;
                    ii = true;
                }
                if ((line.Contains("}") || line.Contains("endif")) && !line.StartsWith("#") && !line.StartsWith("//"))
                {
                    indentlevel -= 1;
                    ii = true;
                }
                if (indentlevel < 1)
                {
                    File.AppendAllText(output, CompileLine(line));
                }
                else if (ii) {
                    if (indentlevel == 0) // end of braces argument
                    {
                        File.AppendAllText(output, CompileLine(line));
                    }
                }
                else
                {
                    if (stack == "")
                        stack = line;
                    else
                        stack += "\n" + line;
                }

                linenumber += 1;
                ii = false;
            }
            return errorlevel;
        }
        #endregion

        string stack = "";

        string CompileLine(string line)
        {
            #region Init
            if (line.StartsWith("#") || line.StartsWith("//")) { return ""; }
            //Console.WriteLine("___________________________________"); // <~ For debugging purposes
            if (line.Trim() == "")
                return "";
            string[] script = line.Trim().Split(' ');
            string Command = script[0];
            string ReturnVar = "";
            string GetArgs = "";
            string CompiledCommand = "";
            bool isString = false;
            var str = "";
            var tmp = script.ToList();

            #region Calculation of return variables and argument divides
            tmp.RemoveAt(0);
            if (tmp.Count > 1)
                if (tmp[tmp.Count - 2] == "as")
                {
                    ReturnVar = tmp[tmp.Count - 1];
                    tmp.RemoveRange(tmp.Count - 2, 2);
                }
            for (int count = 0; count < tmp.Count; count++)
            {
                var p = tmp[count];
                if (p.StartsWith("\"") && p.EndsWith("\""))
                {
                    if (isString)
                    {
                        if (p == "\"")
                        {
                            isString = false;
                            GetArgs += GetString(str + " ");
                            str = "";
                        }
                        else
                        {
                            GetArgs += ParseObject(p);
                        }
                    }
                    else
                    {
                        var sl = "";
                        for (int c = 1; c < p.Length - 1; c++) // No, c#!!!
                        {
                            sl += p[c];
                        }
                        GetArgs += GetString(sl);
                        str = "";
                    }
                }
                else if (p.StartsWith("\""))
                {
                    isString = true;
                    var sl = "";
                    for (int c = 1; c < p.Length; c++) // No, c#!!!
                    {
                        sl += p[c];
                    }
                    str = sl;
                }
                else if (p.EndsWith("\""))
                {
                    isString = false;
                    var sl = "";
                    for (int c = 0; c < p.Length - 1; c++) // No, c#!!!
                    {
                        sl += p[c];
                    }
                    str += " " + sl;
                    GetArgs += GetString(str);
                    str = "";
                }
                else if (isString)
                {
                    str += " " + p;
                }
                else if (p.StartsWith("//") || p.StartsWith("#"))
                    break;
                else
                {
                    GetArgs += ParseObject(p);
                }
            }
            //Console.WriteLine($"Command: {Command}\nArguments: {GetArgs}\nReturn: {ReturnVar}\n________________");
            #endregion
            #endregion

            #region Command definitions
            string throwNotFoundError(string cmd)
            {
                Console.Error.WriteLine("The name " + cmd + " does not exist in the current context (maybe you need to import a header file?)");
                throw new Exception("Invalid command: " + cmd);
            }

            CompiledCommand = Command switch
            {
                #region fishScript Commands
                "help" => "h",
                "log"  => "E",
                "do"   => "+",
                "}"    => "-",
                "input"=> "l",
                "wait" => "_",
                "exit" => "iX",
                "debug"=> "d",
                "fish" => "i124'ti92'tai32'tai32'tai95'tai95'tai124'tai92'tai95'tai95'tai95'tai32'tai32" +
                                      "'taEi124'ti32'tai92'tai47'tai32'tai32'tai95'tai32'tai32'tai111'tai32'tai92't" +
                                      "ai32'taEi124'ti32'tai47'tai92'tai32'tai32'tai92'tai32'tai32'tai95'tai95'tai9" +
                                      "5'tai124'taEi124'ti47'tai32'tai32'tai92'tai95'tai95'tai95'tai95'tai95'tai95'" +
                                      "tai47'tai32'taE", // The Unicode string for our wonderful fish
                "getenv" => "~",
                "add"    => "a",
                "subtract"=>"s",
                "multiply"=>"M",
                "divide" => "D",
                "readkey"=> "e",
                #endregion

                #region fishBytes Words
                "ADD"    => "a",
                "BYTE"   => "c",
                "DEBUG"  => "d",
                "INPUT"  => "e",
                "LOAD"   => "g",
                "UINT"   => "i",
                "LINE"   => "l",
                "SUBTRACT"=>"s",
                "CAST"   => "t",
                "ARGS"   => "A",
                "BOOL"   => "B",
                "SBYTE"  => "C",
                "DIVIDE" => "D",
                "ECHO"   => "E",
                "FLOAT"  => "F",
                "GOTO"   => "G",
                "INTEGER"=> "I",
                "DOUBLE" => "L",
                "MULTIPLY"=>"M",
                "NULL"   => "N",
                "POP"    => "P",
                "SAVE"   => "Q",
                "RESET"  => "R",
                "SQRT"   => "S",
                "VARIABLE"=>"V",
                "WIPE"   => "W",
                "EXIT"   => "X",
                "NOT"    => "!",
                "MODULO" => "%",
                "EXPONENT"=>"^",
                "AND"    => "&",
                "DUPLICATE"=>"*",
                "COPY"   => "*",
                "ZERO"   => "0",
                "ONE"    => "1",
                "TWO"    => "2",
                "THREE"  => "3",
                "FOUR"   => "4",
                "FIVE"   => "5",
                "SIX"    => "6",
                "SEVEN"  => "7",
                "EIGHT"  => "8",
                "NINE"   => "9",
                "OR"     => "|",
                "IF"     => "?",
                "LT"     => "<",
                "LESS_THAN"=>"<",
                "GT"     => ">",
                "GREATER_THAN"=>">",
                "CHAR"   => "'",
                "STRING" => "\"",
                "DELAY"  => "_",
                "WAIT"   => "_",
                "EQUALS" => "=",
                "EQ"     => "=",
                "IS_EQUAL_TO"=>"=",
                "ENV"    => "~",
                "GETENV" => "~",
                "DO"     => "+",
                "END"    => "-",
                "RUN"    => "$",
                "RUNCOM" => "$",
                "NOP"    => " ",
                "NOPE"   => " ",
                "SC"     => "{",
                "EC"     => "}",
                #endregion
                ""       => "",
                _ => throwNotFoundError(Command)
            };
            #endregion

            if (ReturnVar != "")
            {
                if (VariableNames.ContainsKey(ReturnVar))
                {
                    CompiledCommand += "I" + VariableNames[ReturnVar].ToString() + "ItV";
                }
                else
                {
                    VariableNames.Add(ReturnVar, ++LastVariableName);
                    CompiledCommand += "I" + VariableNames[ReturnVar].ToString() + "ItV";
                }
            }

            return GetArgs + CompiledCommand;
        }

        #region Compiling Data
        string GetString(string str) {
            var output = "i";
            if (str.Length == 0)
                return "\"";
            output += Convert.ToUInt32(str[0]).ToString();
            if (str.Length == 1)
                return output + "'t\"t";
            output += "'ti" + Convert.ToUInt16(str[1]).ToString();
            for (int i = 2; i < str.Length; i++) {
                output += "'tai" + Convert.ToUInt16(str[i]).ToString();
            }
            output += "'ta";
            return output;
        }

        string ParseObject(string definition)
        {
            if (VariableNames.ContainsKey(definition))
                return "I" + VariableNames[definition].ToString() + "Itg";
            //if (int.TryParse(definition, out int Int))
            //    return Int;
            if (float.TryParse(definition, out float Float))
                return "F" + Float.ToString()/* + "Ft"*/;
            if (long.TryParse(definition, out long Long))
                return "I" + Long.ToString() + "It";
            if (double.TryParse(definition, out double Double))
                return "L" + Double + "Lt";
            if (ulong.TryParse(definition, out ulong ULong))
                return "i" + ULong + "it";
            if (definition == "true")
                return "B!";
            if (definition == "false")
                return "B";
            if (definition == "null")
                return "N";

            Console.Error.WriteLine("The name '" + definition + "' does not exist in the current context (maybe you need to import a header file?)");
            throw new Exception("The name '"+definition+"' does not exist in the current context (maybe you need to import a header file?)");
        }
        #endregion
    }
}
