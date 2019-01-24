using System;
using System.Linq;

public class StartUp
{
    public static void Main()
    {
        // 71/100

        TextEditor editor = new TextEditor();

        string input;
        while ((input = Console.ReadLine()) != "end")
        {
            var tokens = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var command = tokens[0];

            if (command == "login")
            {
                editor.Login(tokens[1]);
            }
            else if (command == "logout")
            {
                editor.Logout(tokens[1]);
            }
            else if (command == "users")
            {
                var prefix = tokens.Length == 2 ? tokens[2] : "";
                var users = editor.Users(prefix);

                foreach (var user in users)
                {
                    Print(user);
                }
            }
            else
            {
                OtherCommands(editor, tokens);
            }
        }
    }

    public static void OtherCommands(TextEditor editor, string[] tokens)
    {
        var username = tokens[0];
        var command = tokens[1];

        switch (command)
        {
            case "insert":
                {
                    var index = int.Parse(tokens[2]);
                    var text = string
                        .Join(" ", tokens
                        .Skip(3)
                        .ToArray())
                        .Trim('"');

                    editor.Insert(username, index, text);
                }
                break;
            case "prepend":
                {
                    var text = tokens[2].Trim('"');
                    editor.Prepend(username, text);
                }
                break;
            case "substring":
                {
                    var startIndex = int.Parse(tokens[2]);
                    var lenght = int.Parse(tokens[3]);
                    editor.Substring(username, startIndex, lenght);
                }
                break;
            case "delete":
                {
                    var startIndex = int.Parse(tokens[2]);
                    var lenght = int.Parse(tokens[3]);
                    editor.Delete(username, startIndex, lenght);
                }
                break;
            case "clear":
                {
                    editor.Clear(username);
                }
                break;
            case "length":
                {
                    var result = editor.Length(username);
                    Print(result.ToString());
                }
                break;
            case "print":
                {
                    var result = editor.Print(username);
                    if (result != null)
                    {
                        Print(result);
                    }
                }
                break;
            case "undo":
                {
                    editor.Undo(username);
                }
                break;
        }
    }

    public static void Print(string result)
    {
        Console.WriteLine(result);
    }
}