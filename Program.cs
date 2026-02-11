class TodoListApp
{
    private TodoList _tasks;
    private bool _showHelp = true;
    private bool _insertMode = true;
    private bool _quit = false;

    // Constructor wants a TodoList passed
    public TodoListApp(TodoList tasks)
    {
        _tasks = tasks;
    }

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
        Task task = _tasks.GetTask(i);
        string arrow = "  ";
        // If the task we're drawing matches the CurrentTask variable, add an arrow to the draw
        if (task == _tasks.CurrentTask) arrow = "->";
        string check = " ";
        if (task.Status == Task.CompletionStatus.Done) check = "X";     // Needed to added Task for the enumeration to work
        return $"{arrow} [{check}] {task.Title}";
    }

    // Calls MakeRow on every task in the list, passing the task's index to MakeRow
    public void DisplayTasks()
    {
        DisplayBar();
        Console.WriteLine("Tasks:");
        for (int i = 0; i < _tasks.Length; i++)
        {
            Console.WriteLine(MakeRow(i));
        }
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

    // Collects user input for the task title
    private string GetTitle()
    {
        Console.WriteLine("Please enter task title (or [enter] for none): ");
        return Console.ReadLine()!;
    }

    // Handles all user input collection, uses _insertMode to determine what kind of input we should get
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
                _tasks.Insert(taskTitle);
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
                case ConsoleKey.UpArrow:
                    _tasks.SelectPrevious();
                    break;
                case ConsoleKey.DownArrow:
                    _tasks.SelectNext();
                    break;
                case ConsoleKey.LeftArrow:
                    _tasks.SwapWithPrevious();
                    break;
                case ConsoleKey.RightArrow:
                    _tasks.SwapWithNext();
                    break;
                case ConsoleKey.I:
                    _insertMode = true;
                    break;
                case ConsoleKey.E:
                    _tasks.CurrentTask.Title = GetTitle();
                    break;
                // case ConsoleKey.H:
                //     _showHelp = !_showHelp;
                //     break;
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    if (_tasks.Length == 0)     // Need to check if _tasks has a Task before calling ToggleStatus on it
                        break;
                    _tasks.CurrentTask.ToggleStatus();
                    break;
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

// Barebones Task class
class Task
{
    // Properties
    private string _title;  // Field
    public string Title     // Property for _title field, Replaced Title() and SetTitle() methods to match TodoListApp code
    {
        get => _title;
        set => _title = value;
    }
    CompletionStatus _status;
    public CompletionStatus Status => _status;


    // Methods
    public void ToggleStatus()
    {
        if (_status == CompletionStatus.NotDone) _status = CompletionStatus.Done;
        else _status = CompletionStatus.NotDone;
    }

    // Constructor
    public Task(string title)
    {
        _title = title;
        _status = CompletionStatus.NotDone;
    }

    // Enumeration
    public enum CompletionStatus {NotDone, Done}
}

// Barebones TodoList class
class TodoList
{
    // Properties
    List<Task> _tasks = [];
    public int _selectedIndex = 0;
    public Task CurrentTask => GetTask(_selectedIndex);  
    public int Length => _tasks.Count;

    // Methods
    void SwapTasksAt(int i, int j)
    {
        if (Length < 2)
            return;
        (_tasks[i], _tasks[j]) = (_tasks[j], _tasks[i]);
    }

    // Returns an index that may or may not be wrapped depending on the index we ask for
    int WrappedIndex(int index)
    {
        int count = _tasks.Count;
        // Check if our list is empty first
        if (count == 0) 
            return 0;
        // If our intended index is greater or equal to count, wrap back to 0
        if (index >= count)
            return 0;
        // If our intended index is less than 0, wrap to the end of the list
        else if (index < 0)
            return count - 1;
        // Otherwise we can return the intended index unmodified
        else
            return index;
    }

    // These two pass an intended index to WrappedIndex to return a potentially wrapped index
    int PreviousIndex() => WrappedIndex(_selectedIndex - 1);

    int NextIndex() => WrappedIndex(_selectedIndex + 1);

    // Changes _selectedIndex to a target index
    public void SelectPrevious() => _selectedIndex = PreviousIndex();
    public void SelectNext() => _selectedIndex = NextIndex();

    // Calls SwapTasksAt with _selectedIndex and either the previous or the next index
    public void SwapWithPrevious() => SwapTasksAt(_selectedIndex, PreviousIndex());
    public void SwapWithNext() => SwapTasksAt(_selectedIndex, NextIndex());

    public void Insert(string title) => _tasks.Insert(_selectedIndex, new Task(title));

    void UpdateSelectedTitle(string title)
    {
        
    }

    void DeleteSelected()
    {
        
    }


    public Task GetTask(int index) => _tasks[index];
}

class Program
{
    static void Main()
    {
        new TodoListApp(new TodoList()).Run();
    }
}