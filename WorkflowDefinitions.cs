using System.Text.Json;

namespace RPAStudio
{
    // Define a class to represent an action from the JSON
    public class WorkflowAction
    {
        public string? Type { get; set; }
        public JsonElement Params { get; set; }
    }

    // Define parameter classes for deserialization
    public class MouseClickActionParams
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class KeyboardInputParams
    {
        public string? text { get; set; }
    }
}