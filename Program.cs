using RPAStudio.Actions;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace RPAStudio
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load and execute workflows from the 'workflows' directory
            string workflowsDirectory = "workflows";

            if (Directory.Exists(workflowsDirectory))
            {
                string[] workflowFiles = Directory.GetFiles(workflowsDirectory, "*.workflow.json");

                if (workflowFiles.Length == 0)
                {
                    Console.WriteLine($"No workflow files found in '{workflowsDirectory}'.");
                }

                foreach (string workflowFile in workflowFiles)
                {
                    Console.WriteLine($"Executing workflow from: {workflowFile}");
                    try
                    {
                        string jsonContent = File.ReadAllText(workflowFile);
                        List<WorkflowAction>? workflowActions = JsonSerializer.Deserialize<List<WorkflowAction>>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        RpaWorkflow workflow = new RpaWorkflow();

                        if (workflowActions != null)
                        {
                            foreach (var actionData in workflowActions)
                            {
                                RpaAction? action = null;
                                switch (actionData.Type)
                                {
                                    case "MouseClickAction":
                                        var mouseParams = JsonSerializer.Deserialize<MouseClickActionParams>(actionData.Params.GetRawText());
                                        if (mouseParams != null) action = new MouseClickAction(mouseParams.x, mouseParams.y);
                                        break;
                                    case "KeyboardInputAction":
                                        var keyboardParams = JsonSerializer.Deserialize<KeyboardInputParams>(actionData.Params.GetRawText());
                                        if (keyboardParams != null) action = new KeyboardInput(keyboardParams.text!);
                            break;
                                    // Add cases for other action types here
                                }

                                if (action != null)
                                {
                                    workflow.AddAction(action);
                                } else {
                                    Console.WriteLine($"Failed to create action for type: {actionData.Type}");
                                }
                            }
                        }

                        // Create an engine and execute the workflow
                        RpaEngine engine = new RpaEngine();
                        engine.ExecuteWorkflow(workflow);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error executing workflow '{workflowFile}': {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Workflows directory '{workflowsDirectory}' not found.");
            }
        }
    }
}
