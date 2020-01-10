﻿using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models;

namespace IssueTrackingSystemApi.Services
{
    public interface IIssueService
    {
        List<Issue> GetAllIssues();

        Issue GetIssueById(int id);

        int UpdateIssue(Issue issue);

        int CreateIssue(Issue issue);

        Issue GetIssueByNumber(string number);

        int DeleteIssue(int id);
    }
}
