// Copyright (c) CNM. All rights reserved.

namespace HackerSpace.Server.Data.Mocks
{
    using HackerSpace.Shared.Interfaces;
    using HackerSpace.Shared.Models;

    /// <summary>
    /// Mock implementation of <see cref="IEvaluatorsPageDataService"/> for testing and development purposes.
    /// Provides in-memory storage and manipulation of <see cref="Evaluator"/> objects.
    /// </summary>
    public class EvaluatorsPageDataServiceMock : IEvaluatorsPageDataService
    {
        private List<ApplicationUser> users;
        private List<Evaluator> evaluators;

        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluatorsPageDataServiceMock"/> class.
        /// Sets up in-memory lists of <see cref="ApplicationUser"/> and <see cref="Evaluator"/> objects for testing and development.
        /// </summary>
        public EvaluatorsPageDataServiceMock()
        {
            this.users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "evaluator1@aserver.net",
                    Email = "evaluator1@aserver.net",
                    First = "Evaluator",
                    Last = "One",
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "evaluator2@aserver.net",
                    Email = "evaluator2@aserver.net",
                    First = "Evaluator",
                    Last = "Two",
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "evaluator3@aserver.net",
                    Email = "evaluator3@aserver.net",
                    First = "Evaluator",
                    Last = "Three",
                    EmailConfirmed = true,
                },
            };
            this.evaluators = new List<Evaluator>
                {
                    new Evaluator
                    {
                        Id = Guid.NewGuid(),
                        User = this.users[0],
                        NotificationEmail = this.users[0].Email,
                    },
                    new Evaluator
                    {
                        Id = Guid.NewGuid(),
                        User = this.users[1],
                        NotificationEmail = this.users[1].Email,
                    },
                };
        }

        /// <summary>
        /// Retrieves all <see cref="Evaluator"/> objects from the in-memory store.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Evaluator"/> objects.</returns>
        public Task<List<Evaluator>?> GetAllAsync()
        {
            return Task.FromResult(evaluators);
        }

        public Task AddAsync(Evaluator evaluator)
        {
            throw new NotImplementedException();
        }

        public Task<Evaluator> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Evaluator evaluator)
        {
            var current = evaluators.Where(e=>e.Id == evaluator.Id).FirstOrDefault();
            if (current == null)
            {
                throw new Exception("Evaluator not found");
            }
            current.NotificationEmail = evaluator.NotificationEmail;
            current.User = evaluator.User;
            current.UserId = evaluator.UserId;
            return Task.CompletedTask;
        }
    }
}
