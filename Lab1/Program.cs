namespace Lexer;

public class Program
{
    static void Main(string[] args)
    {
        var input = @"
                // Scala Program to illustrate the keywords 
                  
                // Here class keyword is used to create a new class
                // def keyword is used to create Function
                // var keyword is used to create a variable 
                class GFG
                {
                    val name = ""Priyanka""
                    val age = 20
                    val branch = ""Computer Science""
                    def show()
                    {
                        println(""Hello! my name is "" + name + ""and my age is""+age);
                        println(""My branch name is "" + branch);
                    }
                }
                  
                /*
                  object keyword is used to define 
                  an object new keyword is used to 
                  create an object of the given class
                */
                object Main 
                {
                    def main(args: Array[String])
                    {
                        val ob = new GFG();
                        ob.show();
                    }
                }
            ";

        using var reader = new StringReader(input);
        Lexer lexer = new ScalaLexer(reader);
        var tokens = lexer.Tokenize();

        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
    }
}