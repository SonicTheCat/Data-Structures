using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Judge : IJudge
{
    private readonly OrderedBag<int> byUserId;
    private readonly OrderedBag<int> byContestId;
    private readonly Dictionary<int, Submission> submissions;

    public Judge()
    {
        this.byUserId = new OrderedBag<int>();
        this.byContestId = new OrderedBag<int>();
        this.submissions = new Dictionary<int, Submission>();
    }

    public void AddContest(int contestId)
    {
        this.byContestId.Add(contestId);
    }

    public void AddSubmission(Submission submission)
    {
        var userId = submission.UserId;
        var contestId = submission.ContestId;
        if (!this.byUserId.Contains(userId) || !this.byContestId.Contains(contestId))
        {
            throw new InvalidOperationException();
        }

        if (this.submissions.ContainsKey(submission.Id))
        {
            return;
        }
        this.submissions[submission.Id] = submission;
    }

    public void AddUser(int userId)
    {
        this.byUserId.Add(userId);
    }

    public void DeleteSubmission(int submissionId)
    {
        if (!this.submissions.ContainsKey(submissionId))
        {
            throw new InvalidOperationException();
        }

        this.submissions.Remove(submissionId);
    }

    public IEnumerable<int> GetUsers() => this.byUserId;

    public IEnumerable<int> GetContests() => this.byContestId;

    public IEnumerable<Submission> GetSubmissions()
    {
        foreach (var sub in this.submissions.OrderBy(x => x.Key))
        {
            yield return sub.Value;
        }
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        return this.submissions
               .Values
               .Where(x => x.Type == submissionType &&
               x.Points >= minPoints &&
               x.Points <= maxPoints);
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {
        return this.submissions
            .Values
            .Where(x => x.UserId == userId)
            .GroupBy(x => x.ContestId)
            .Select(x =>
                     x.OrderByDescending(s => s.Points).ThenBy(s => s.Id).First())
            .OrderByDescending(x => x.Points)
            .ThenBy(x => x.Id)
            .Select(x => x.ContestId);
    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        var values = this.submissions
          .Values
          .Where(x => x.UserId == userId &&
                 x.ContestId == contestId &&
                 x.Points == points)
          .ToList();

        if (values.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return values;
    }

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        return this.submissions
           .Values
           .Where(x => x.Type == submissionType)
           .Select(x => x.ContestId);
    }
}
