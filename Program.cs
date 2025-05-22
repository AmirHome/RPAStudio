using RPAStudio;

// Create a new workflow
RpaWorkflow workflow = new RpaWorkflow();

// Add some actions to the workflow
workflow.AddAction(new MouseClickAction(100, 200));
workflow.AddAction(new KeyboardInput("Hello, RPA!"));
workflow.AddAction(new MouseClickAction(300, 400));

// Create an engine and execute the workflow
RpaEngine engine = new RpaEngine();
engine.ExecuteWorkflow(workflow);
