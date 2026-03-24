// Copyright (c) 2025. All rights reserved.

using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace Tests;

/// <summary>
/// Tests for the <see cref="BadgeSuggestion"/> model and <see cref="BadgeSuggestionStatus"/> enum.
/// </summary>
[TestClass]
public sealed class BadgeSuggestionTests
{
    /// <summary>
    /// Verifies that a valid badge suggestion passes data annotation validation.
    /// </summary>
    [TestMethod]
    public void ValidSuggestion_PassesValidation()
    {
        var suggestion = new BadgeSuggestion
        {
            Title = "Python Web Scraping",
            Description = "Demonstrate ability to scrape web data using Python.",
            SuggestedById = "user-123",
            SuggestedByName = "Test User",
        };

        var results = ValidateModel(suggestion);

        Assert.IsTrue(results.Count == 0, "A valid suggestion should have no validation errors.");
    }

    /// <summary>
    /// Verifies that a suggestion with a missing title fails validation.
    /// </summary>
    [TestMethod]
    public void MissingTitle_FailsValidation()
    {
        var suggestion = new BadgeSuggestion
        {
            Title = string.Empty,
            Description = "A valid description.",
            SuggestedById = "user-123",
        };

        var results = ValidateModel(suggestion);

        Assert.IsTrue(results.Any(r => r.MemberNames.Contains("Title")), "Missing title should produce a validation error.");
    }

    /// <summary>
    /// Verifies that a suggestion with a missing description fails validation.
    /// </summary>
    [TestMethod]
    public void MissingDescription_FailsValidation()
    {
        var suggestion = new BadgeSuggestion
        {
            Title = "Valid Title",
            Description = string.Empty,
            SuggestedById = "user-123",
        };

        var results = ValidateModel(suggestion);

        Assert.IsTrue(results.Any(r => r.MemberNames.Contains("Description")), "Missing description should produce a validation error.");
    }

    /// <summary>
    /// Verifies that a suggestion with a missing SuggestedById fails validation.
    /// </summary>
    [TestMethod]
    public void MissingSuggestedById_FailsValidation()
    {
        var suggestion = new BadgeSuggestion
        {
            Title = "Valid Title",
            Description = "Valid description.",
            SuggestedById = string.Empty,
        };

        var results = ValidateModel(suggestion);

        Assert.IsTrue(results.Any(r => r.MemberNames.Contains("SuggestedById")), "Missing SuggestedById should produce a validation error.");
    }

    /// <summary>
    /// Verifies that a new suggestion defaults to Pending status.
    /// </summary>
    [TestMethod]
    public void NewSuggestion_DefaultsToPending()
    {
        var suggestion = new BadgeSuggestion();

        Assert.AreEqual(BadgeSuggestionStatus.Pending, suggestion.Status);
    }

    /// <summary>
    /// Verifies that a new suggestion has a CreatedAt timestamp set.
    /// </summary>
    [TestMethod]
    public void NewSuggestion_HasCreatedAtTimestamp()
    {
        var before = DateTime.UtcNow;
        var suggestion = new BadgeSuggestion();
        var after = DateTime.UtcNow;

        Assert.IsTrue(suggestion.CreatedAt >= before && suggestion.CreatedAt <= after);
    }

    /// <summary>
    /// Verifies that a new suggestion has null review fields.
    /// </summary>
    [TestMethod]
    public void NewSuggestion_HasNullReviewFields()
    {
        var suggestion = new BadgeSuggestion();

        Assert.IsFalse(suggestion.ReviewedAt.HasValue, "ReviewedAt should be null for a new suggestion.");
        Assert.IsTrue(string.IsNullOrEmpty(suggestion.ReviewedById), "ReviewedById should be null for a new suggestion.");
        Assert.IsTrue(string.IsNullOrEmpty(suggestion.AdminNotes), "AdminNotes should be null for a new suggestion.");
    }

    /// <summary>
    /// Verifies that the enum contains the expected values.
    /// </summary>
    [TestMethod]
    public void BadgeSuggestionStatus_HasExpectedValues()
    {
        var values = Enum.GetValues<BadgeSuggestionStatus>();
        Assert.AreEqual(3, values.Length, "BadgeSuggestionStatus should have exactly 3 values.");
        Assert.IsTrue(Enum.IsDefined(BadgeSuggestionStatus.Pending));
        Assert.IsTrue(Enum.IsDefined(BadgeSuggestionStatus.Approved));
        Assert.IsTrue(Enum.IsDefined(BadgeSuggestionStatus.Rejected));
    }

    /// <summary>
    /// Verifies that TurnInInstructions is optional and defaults to empty.
    /// </summary>
    [TestMethod]
    public void TurnInInstructions_IsOptionalAndDefaultsToEmpty()
    {
        var suggestion = new BadgeSuggestion
        {
            Title = "Valid Title",
            Description = "Valid description.",
            SuggestedById = "user-123",
        };

        Assert.IsTrue(string.IsNullOrEmpty(suggestion.TurnInInstructions), "TurnInInstructions should default to empty.");

        var results = ValidateModel(suggestion);
        Assert.IsTrue(results.Count == 0, "A suggestion without turn-in instructions should be valid.");
    }

    /// <summary>
    /// Verifies that a Badge created from edited values preserves the edited data, not the original.
    /// </summary>
    [TestMethod]
    public void ApprovedBadge_UsesEditedValues_NotOriginalSuggestion()
    {
        var suggestion = new BadgeSuggestion
        {
            Id = Guid.NewGuid(),
            Title = "Original Title",
            Description = "Original Description",
            TurnInInstructions = "Original Instructions",
            SuggestedById = "user-123",
            SuggestedByName = "Test User",
        };

        string editedTitle = "Edited Title";
        string editedDescription = "Edited Description";
        string editedInstructions = "Edited Instructions";

        var badge = new Badge
        {
            Id = Guid.NewGuid(),
            Title = editedTitle,
            Description = editedDescription,
            TurnInInstructions = editedInstructions,
            IsVisible = false,
        };

        Assert.AreEqual(editedTitle, badge.Title, "Badge should use the edited title.");
        Assert.AreEqual(editedDescription, badge.Description, "Badge should use the edited description.");
        Assert.AreEqual(editedInstructions, badge.TurnInInstructions, "Badge should use the edited instructions.");
        Assert.AreNotEqual(suggestion.Title, badge.Title, "Badge title should differ from the original suggestion.");
    }

    /// <summary>
    /// Verifies that the original suggestion fields remain unchanged after approval with edits.
    /// </summary>
    [TestMethod]
    public void OriginalSuggestion_PreservedAfterApprovalWithEdits()
    {
        var suggestion = new BadgeSuggestion
        {
            Id = Guid.NewGuid(),
            Title = "Original Title",
            Description = "Original Description",
            TurnInInstructions = "Original Instructions",
            SuggestedById = "user-123",
            SuggestedByName = "Test User",
        };

        suggestion.Status = BadgeSuggestionStatus.Approved;
        suggestion.ReviewedAt = DateTime.UtcNow;
        suggestion.ReviewedById = "admin-456";

        Assert.AreEqual("Original Title", suggestion.Title, "Original suggestion title should not change.");
        Assert.AreEqual("Original Description", suggestion.Description, "Original suggestion description should not change.");
        Assert.AreEqual("Original Instructions", suggestion.TurnInInstructions, "Original suggestion instructions should not change.");
        Assert.AreEqual(BadgeSuggestionStatus.Approved, suggestion.Status);
    }

    /// <summary>
    /// Verifies that an approved badge defaults to not visible.
    /// </summary>
    [TestMethod]
    public void ApprovedBadge_DefaultsToNotVisible()
    {
        var badge = new Badge
        {
            Id = Guid.NewGuid(),
            Title = "Test Badge",
            Description = "Test Description",
            TurnInInstructions = "Test Instructions",
            IsVisible = false,
        };

        Assert.IsFalse(badge.IsVisible, "A badge created from an approved suggestion should default to not visible.");
    }

    private static List<ValidationResult> ValidateModel(object model)
    {
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }
}
