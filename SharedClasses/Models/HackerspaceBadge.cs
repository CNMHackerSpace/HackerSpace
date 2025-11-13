namespace SharedClasses.Models
{
    public class HackerspaceBadge
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = "New badge";
        public string Description { get; set; } = "TODO: Add description of the new badge, how to learn this skill, and what needs to be done to demonstrate the skill.";
        public string TurnInInstructions { get; set; } = "TODO: Add instructions on what to turn in and how to notify an evaluator that they are ready to be evaluated. Examples could include providing a gihub repo url, url to a video where the badge candidate has recorded a walkthrough of the code or project, etc.";
        public string FileName { get; set; } = "Placeholder_view_vector.png";
    }
}
