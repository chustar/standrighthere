using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Runtime.Serialization;

[DataTable (Name = "user_points")]
public class UserPoints
{
    [DataMember(Name = "user_id")]
    public MobileServiceUser UserId { get; set; }

    [DataMember(Name = "point_id")]
    public int PointId { get; set; }
}

[DataTable(Name = "user_challenges_submitted")]
public class UserChallengesSubmitted
{
    [DataMember(Name = "user_id")]
    public MobileServiceUser UserId { get; set; }

    [DataMember(Name = "challege_id")]
    public int ChallengeId { get; set; }
}

[DataTable(Name = "user_challenges_solved")]
public class UserChallengesSolved
{
    [DataMember(Name = "user_id")]
    public int UserId { get; set; }

    [DataMember(Name = "challege_id")]
    public int ChallengeId { get; set; }

    [DataMember(Name = "solved_date")]
    public DateTime SolvedDate { get; set; }

}

[DataTable(Name = "user_challenges_missed")]
public class UserChallengesMissed
{
    [DataMember(Name = "user_id")]
    public MobileServiceUser UserId { get; set; }

    [DataMember(Name = "challege_id")]
    public int ChallengeId { get; set; }

    [DataMember(Name = "missed_date")]
    public DateTime MissedDate { get; set; }
}