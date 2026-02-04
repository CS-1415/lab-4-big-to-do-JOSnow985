class TodoListApp
{
    // private TodoList _tasks;
    private bool _showHelp = true;
    private bool _insertMode = true;
    private bool _quit = false;

    // Constructor wants a TodoList passed
    // public TodoListApp(TodoList tasks)
    // {
    //     _tasks = tasks;
    // }

    // Method to loop the program until we're quitting
    public void Run()
    {
        // While we're not quitting, clear console, display tasks, gather input
        while (!_quit)
        {
            Console.Clear();
            Display();
            ProcessUserInput();
        }
    }

    // Runs DisplayTasks and also DisplayHelp if needed
    public void Display()
    {
        DisplayTasks();
        if (_showHelp)
        {
            DisplayHelp();
        }
    }

    public void DisplayBar()
    {
        Console.WriteLine("----------------------------");
    }

    // Creates a string with variable components for a task in the list using a provided index
    public string MakeRow(int i)
    {
        // Task task = _tasks.GetTask(i);
        string arrow = "  ";
        // If the task we're drawing matches the CurrentTask variable, add an arrow to the draw
        // if (task == _tasks.CurrentTask) arrow = "->";
        string check = " ";
        // if (task.Status == CompletionStatus.Done) check = "X";
        // return $"{arrow} [{check}] {task.Title}";
        return $"{arrow} [{check}]";
    }

    // Calls MakeRow on every task in the list, passing the task's index to MakeRow
    public void DisplayTasks()
    {
        DisplayBar();
        Console.WriteLine("Tasks:");
        // for (int i = 0; i < _tasks.Length; i++)
        // {
        //     Console.WriteLine(MakeRow(i));
        // }
        DisplayBar();
    }

    public void DisplayHelp()
    {
        Console.WriteLine(
@"Instructions:
   h: show/hide instructions
   ↕: select previous or next task (wrapping around at the top and bottom)
   ↔: reorder task (swap selected task with previous or next task)
   space: toggle completion of selected task
   e: edit title
   i: insert new tasks
   delete/backspace: delete task");
        DisplayBar();
    }

    // Collects user input for the task title, validated in ProcessUserInput()
    private string GetTitle()
    {
        Console.WriteLine("Please enter task title (or [enter] for none): ");
        return Console.ReadLine()!;
    }

    public void ProcessUserInput()
    {
        // If we're in insert mode, we get a string from the user
        if (_insertMode)
        {
            string taskTitle = GetTitle();
            // If the title we get back from the user is empty, we just turn off insertMode
            if (taskTitle.Length == 0)
            {
                _insertMode = false;
            }
            else
            {
                // _tasks.Insert(taskTitle);
            }
        }
        else
        {
            // Switch for user input to perform the various operations on the task list
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Escape:
                    _quit = true;
                    break;
                // case ConsoleKey.UpArrow:
                //     _tasks.SelectPrevious();
                //     break;
                // case ConsoleKey.DownArrow:
                //     _tasks.SelectNext();
                //     break;
                // case ConsoleKey.LeftArrow:
                //     _tasks.SwapWithPrevious();
                //     break;
                // case ConsoleKey.RightArrow:
                //     _tasks.SwapWithNext();
                //     break;
                // case ConsoleKey.I:
                //     _insertMode = true;
                //     break;
                // case ConsoleKey.E:
                //     _tasks.CurrentTask.Title = GetTitle();
                //     break;
                // case ConsoleKey.H:
                //     _showHelp = !_showHelp;
                //     break;
                // case ConsoleKey.Enter:
                // case ConsoleKey.Spacebar:
                //     _tasks.CurrentTask.ToggleStatus();
                //     break;
                // case ConsoleKey.Delete:
                // case ConsoleKey.Backspace:
                //     _tasks.DeleteSelected();
                //     break;
                default:
                    break;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // new TodoListApp(new TodoList()).Run();
    }
}