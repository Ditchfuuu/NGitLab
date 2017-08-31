﻿using System.Linq;
using NGitLab.Models;
using NUnit.Framework;

namespace NGitLab.Tests.MergeRequest
{
    public class MergeRequestCommentsClientTests
    {
        private IMergeRequestClient _mergeRequestClient;
        private Project _project;

        private Models.MergeRequest _mergeRequest;

        private Models.MergeRequest MergeRequest
        {
            get
            {
                if (_mergeRequest == null)
                {
                    var branch = CreateBranch();
                    _mergeRequest = _mergeRequestClient.Create(new MergeRequestCreate
                    {
                        Title = "Test merge request comments",
                        SourceBranch = branch.Name,
                        TargetBranch = "master"
                    });
                }

                return _mergeRequest;
            }
        }

        private static Branch CreateBranch()
        {
            var branch = Initialize.Repository.Branches.Create(new BranchCreate
            {
                Name = "mr-comments-test",
                Ref = "master"
            });

            Initialize.Repository.Files.Create(new FileUpsert
            {
                RawContent = "test content",
                CommitMessage = "commit to merge",
                Branch = branch.Name,
                Path = "mr-comments-test.md",
            });

            return branch;
        }

        [SetUp]
        public void Setup()
        {
            _project = Initialize.UnitTestProject;
            _mergeRequestClient = Initialize.GitLabClient.GetMergeRequest(_project.Id);
        }

        [Test]
        [Order(1)]
        public void AddCommentToMergeRequest()
        {
            var mergeRequestComments = _mergeRequestClient.Comments(MergeRequest.Iid);
            const string commentMessage = "Comment for MR";
            var newComment = new MergeRequestComment
            {
                Body = commentMessage,
            };
            var comment = mergeRequestComments.Add(newComment);
            Assert.That(comment.Body, Is.EqualTo(commentMessage));
        }

        [Test]
        [Order(2)]
        public void GetAllComments()
        {
            var mergeRequestComments = _mergeRequestClient.Comments(MergeRequest.Iid);
            var comments = mergeRequestComments.All.ToArray();
            CollectionAssert.IsNotEmpty(comments);
        }
    }
}