# RPAStudio

RPAStudio is a simple Robotic Process Automation (RPA) tool built with .NET. It allows you to define workflows using JSON files to automate tasks like keyboard input and mouse clicks.

## Project Structure

- `Actions/`: Contains definitions for different RPA actions.
- `workflows/`: Contains example workflow definition files.
- `Program.cs`: The main entry point of the application.
- `RpaAction.cs`: Base class for RPA actions.
- `RpaEngine.cs`: Core engine for executing workflows.
- `RpaWorkflow.cs`: Represents a workflow definition.
- `WorkflowDefinitions.cs`: Handles loading workflow definitions.

## Setup

1. **Clone the repository:**

   ```bash
   git clone <repository_url>
   cd RPAStudio
   ```

2. **Ensure you have .NET SDK installed:**

   You can download it from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).

3. **Restore dependencies:**

   ```bash
   dotnet restore
   ```

## Running the Project

1. **Build the project:**

   ```bash
   dotnet build
   ```

2. **Run a workflow:**

   ```bash
   dotnet run -- workflows/sample1.workflow.json
   ```
   Replace `workflows/sample1.workflow.json` with the path to the workflow file you want to run.

## Adding New Actions

To add a new RPA action:

1. Create a new class in the `Actions/` directory that inherits from `RpaAction`.
2. Implement the `Execute()` method with the logic for your new action.
3. Add a corresponding entry in the workflow JSON schema (if applicable) and update the `RpaEngine` to recognize and execute the new action type.

## Adding New Workflows

To create a new workflow:

1. Create a new JSON file in the `workflows/` directory (or any preferred location).
2. Define the sequence of actions in the JSON file according to the expected schema. Refer to the existing examples in `workflows/` for guidance.
3. Run the new workflow using `dotnet run -- <path_to_your_workflow_file>.json`.