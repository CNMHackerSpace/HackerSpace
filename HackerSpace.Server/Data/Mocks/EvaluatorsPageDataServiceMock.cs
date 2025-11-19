// Copyright (c) CNM. All rights reserved.

namespace HackerSpace.Server.Data.Mocks
{
    using HackerSpace.Shared.Interfaces;
    using HackerSpace.Shared.Models;
    using HackerSpace.Shared.ViewModels;
    using System.Text.Json;

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
                    }
                };
        }

        /// <inheritdoc/>
        public Task<EvaluatorsPageVM> GetEvaluatorsPageVMAsync()
        {
            return Task.FromResult(new EvaluatorsPageVM()
            {
                Evaluators = JsonSerializer.Deserialize<List<Evaluator>>(JsonSerializer.Serialize(evaluators)),
                Users = JsonSerializer.Deserialize<List<ApplicationUser>>(JsonSerializer.Serialize(users)),
            });
        }

        /// <inheritdoc/>
        public Task<List<Evaluator>?> GetAllAsync()
        {
            return Task.FromResult(evaluators);
        }

        /// <inheritdoc/>
        public Task AddAsync(Evaluator evaluator)
        {
            this.evaluators.Add(evaluator);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<Evaluator?> GetAsync(Guid id)
        {
            return Task.FromResult(this.evaluators.Where(e => e.Id == id).FirstOrDefault());
        }

        /// <inheritdoc/>
        public Task RemoveAsync(Guid id)
        {
            if (this.evaluators == null)
            {
                throw new Exception("No evaluators list.");
            }

            var current = this.evaluators.Where(e => e.Id == id).FirstOrDefault();
            if (current == null)
            {
                throw new Exception("Evaluator not found.");
            }

            this.evaluators.Remove(current);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task UpdateAsync(Evaluator evaluator)
        {
            var current = this.evaluators.Where(e=>e.Id == evaluator.Id).FirstOrDefault();
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
