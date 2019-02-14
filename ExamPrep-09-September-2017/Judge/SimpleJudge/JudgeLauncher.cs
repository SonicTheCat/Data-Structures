using System.Collections.Generic;

public static class JudgeLauncher
{
    public static void Main()
    {
        SortedSet<Submission> submissions = new SortedSet<Submission>();

        var submission = new Submission(3, 20, SubmissionType.CSharpCode, 10, 5);
        var submission2 = new Submission(2, 23, SubmissionType.CSharpCode, 10, 5);
        var submission3 = new Submission(1, 20, SubmissionType.CSharpCode, 10, 5);

        submissions.Add(submission);
        submissions.Add(submission2);
        submissions.Add(submission3);

        System.Console.WriteLine();
    }
}

