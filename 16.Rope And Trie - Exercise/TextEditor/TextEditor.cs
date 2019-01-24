using System.Collections.Generic;
using Wintellect.PowerCollections;

public class TextEditor : ITextEditor
{
    private readonly Trie<BigList<char>> usersStrings;
    private readonly Trie<Stack<string>> oldStrings;
    private readonly HashSet<string> allUsers;

    public TextEditor()
    {
        this.usersStrings = new Trie<BigList<char>>();
        this.oldStrings = new Trie<Stack<string>>();
        this.allUsers = new HashSet<string>();
    }

    public void Clear(string username)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }

        var bigList = this.usersStrings.GetValue(username);

        this.PutOldStringInStack(bigList, username);

        bigList.Clear();
    }

    public void Delete(string username, int startIndex, int length)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }

        var bigList = this.usersStrings.GetValue(username);

        this.PutOldStringInStack(bigList, username);

        bigList.RemoveRange(startIndex, length);
    }

    public void Insert(string username, int index, string str)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }

        var bigList = this.usersStrings.GetValue(username);

        var oldString = string.Join("", bigList);
        if (oldString.Length >= index)
        {
            this.PutOldStringInStack(bigList, username);

            bigList.InsertRange(index, str);
        }
    }

    public int Length(string username)
    {
        if (!this.usersStrings.Contains(username))
        {
            return 0;
        }

        return this.usersStrings.GetValue(username).Count;
    }

    public void Login(string username)
    {
        this.usersStrings.Insert(username, new BigList<char>());
        this.oldStrings.Insert(username, new Stack<string>());
        this.allUsers.Add(username);
    }

    public void Logout(string username)
    {
        this.usersStrings.Delete(username);
        this.oldStrings.Delete(username);
        this.allUsers.Remove(username);
    }

    public void Prepend(string username, string str)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }

        var bigList = this.usersStrings.GetValue(username);

        this.PutOldStringInStack(bigList, username);

        bigList.InsertRange(0, str);
    }

    public string Print(string username)
    {
        if (!this.usersStrings.Contains(username))
        {
            return null;
        }

        var value = this.usersStrings.GetValue(username);

        return string.Join("", value);
    }

    public void Substring(string username, int startIndex, int length)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }

        var bigList = this.usersStrings.GetValue(username);

        this.PutOldStringInStack(bigList, username);

        var subStr = bigList.GetRange(startIndex, length);
        bigList.Clear();
        bigList.AddRange(subStr);
    }

    public void Undo(string username)
    {
        if (!this.usersStrings.Contains(username))
        {
            return;
        }

        var stack = this.oldStrings.GetValue(username);
        if (stack.Count == 0)
        {
            return;
        }

        var oldString = stack.Pop();

        var bigList = this.usersStrings.GetValue(username);

        bigList.Clear();
        bigList.AddRange(oldString);
    }

    public IEnumerable<string> Users(string prefix = "")
    {
        if (prefix != string.Empty)
        {
            foreach (var user in this.usersStrings.GetByPrefix(prefix))
            {
                yield return user;
            }
        }
        else
        {
            foreach (var user in this.allUsers)
            {
                yield return user;
            }
        }
    }

    private void PutOldStringInStack(BigList<char> bigList, string username)
    {
        var oldString = string.Join("", bigList);

        this.oldStrings.GetValue(username).Push(oldString);
    }
}