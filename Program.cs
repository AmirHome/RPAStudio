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
                                try
                                {
                                    // Dynamically find and create the action type using reflection
                                    Type? actionType = Type.GetType($"RPAStudio.Actions.{actionData.Type}");

                                    // If the exact type is not found, try removing the 'Action' suffix
                                    if (actionType == null && actionData.Type != null && actionData.Type.EndsWith("Action"))
                                    {
                                        string potentialActionTypeName = actionData.Type.Substring(0, actionData.Type.Length - "Action".Length);
                                        actionType = Type.GetType($"RPAStudio.Actions.{potentialActionTypeName}");
                                    }

                                    if (actionType != null && typeof(RpaAction).IsAssignableFrom(actionType))
                                    {
                                        // Find the appropriate parameter type based on the action type name
                                        Type? paramType = Type.GetType($"RPAStudio.{actionData.Type}Params");

                                        // If the exact parameter type is not found, and the action type name ends with 'Action',
                                        // try finding the parameter type by removing the 'Action' suffix from the action type name.
                                        if (paramType == null && actionData.Type != null && actionData.Type.EndsWith("Action"))
                                        {
                                            string potentialParamTypeName = actionData.Type.Substring(0, actionData.Type.Length - "Action".Length) + "Params";
                                            paramType = Type.GetType($"RPAStudio.{potentialParamTypeName}");
                                        }

                                        object? parameters = null;
                                        if (paramType != null)
                                        {
                                            // Deserialize the parameters from the JSON element
                                            try
                                            {
                                                parameters = JsonSerializer.Deserialize(actionData.Params.GetRawText(), paramType, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                                            }
                                            catch (Exception deserializeEx)
                                            {
                                                Console.WriteLine($"Error deserializing parameters for action type {actionData.Type}: {deserializeEx.Message}");
                                                // If deserialization fails, we cannot create the action with parameters.
                                                // We should not attempt to create it without parameters if paramType was expected.
                                                actionType = null; // Prevent action creation
                                            }
                                        }

                                        // Create an instance of the action type
                                        if (actionType != null) // Check if actionType is still valid after potential deserialization error
                                        {
                                            if (paramType != null) // If a parameter type was found, attempt to create with parameters
                                            {
                                                if (parameters != null)
                                                {
                                                    action = (RpaAction?)Activator.CreateInstance(actionType, new object[] { parameters });
                                                }
                                                else
                                                {
                                                    // Deserialization failed or parameters were null/empty when expected
                                                    Console.WriteLine($"Parameters expected for action type {actionData.Type} but none were provided or deserialized successfully.");
                                                    // actionType is already set to null in the catch block or the previous if block
                                                }
                                            }
                                            else // If no parameter type was found, attempt to create without parameters
                                            {
                                                action = (RpaAction?)Activator.CreateInstance(actionType);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Unknown or invalid action type: {actionData.Type}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error creating action of type {actionData.Type}: {ex.Message}");
                                }

                                if (action != null)
                                {
                                    workflow.AddAction(action);
                                }
                                else
                                {
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
