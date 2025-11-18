namespace HackerSpace.Shared.Models
{
    /// <summary>
    /// Represents a Hackerspace Badge that a person can earn.
    /// </summary>
    public class Badge
    {
        /// <summary>
        /// Unique id for the badge.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title for the badge.
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Short paragraph long description of the badge.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Detailed insructions on how to turn in a badge
        /// evaluation request. Should include all 
        /// instructions needed so assessor can evaluate
        /// whether or not to award the badge.
        /// </summary>
        public string? TurnInInstructions { get; set; }
        /// <summary>
        /// Keep the badge invisible until it has been 
        /// approved for display.
        /// </summary>
        public bool? IsVisible { get; set; }
    }
}
