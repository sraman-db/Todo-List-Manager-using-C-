# Todo List Manager

A desktop application built with C# and Windows Forms for managing your daily tasks with multi-user support.

## Features

- **User Authentication**: Multi-user support with individual task lists for each user
- **Task Management**: Add, delete, and update tasks
- **Task Prioritization**: Set task priority levels (Low, Medium, High)
- **Visual Indicators**: Color-coded tasks based on priority
- **Organization Tools**: Pin important tasks to the top of your list
- **Task Status**: Mark tasks as complete or incomplete
- **Persistence**: All tasks and user data are saved locally

## Requirements

- Windows operating system
- .NET Framework 4.7.2 or higher
- Visual Studio 2019+ (for development)

## Installation

### Option 1: Run the compiled application
1. Download the latest release from the [Releases](https://github.com/yourusername/todo-list-manager/releases) page
2. Extract the ZIP file
3. Run `TodoListManager.exe`

### Option 2: Build from source
1. Clone this repository
   ```
   git clone https://github.com/sraman-db/todo-list-manager.git
   ```
2. Open the solution file `TodoListManager.sln` in Visual Studio
3. Build the solution (Ctrl+Shift+B)
4. Run the application (F5)

## Usage

### First Time Setup
1. Launch the application
2. Enter a username and password
3. Click "Login" to create a new account

### Adding Tasks
1. Enter a task title (required)
2. Add an optional description
3. Select a priority level (Low, Medium, High)
4. Click "Add Task"

### Managing Tasks
- **Complete/Incomplete**: Click the checkbox to toggle task completion status
- **Pin/Unpin**: Click the star icon to pin important tasks to the top
- **Delete**: Click the trash icon to remove a task

## Project Structure

- `MainForm.cs`: Main application form containing UI and logic
- `TodoItem.cs`: Data model for todo items
- `Resources.cs`: Application resources (icons, images)

## How Data is Stored

- User credentials are stored in `users.json`
- Todo items are stored in `{username}_todos.json`
- Files are saved in the application directory

## Future Enhancements

- [ ] Due dates for tasks
- [ ] Task categories/tags
- [ ] Task search and filtering
- [ ] Task sorting options
- [ ] Dark mode theme
- [ ] Task reminders and notifications
- [ ] Task sharing between users
- [ ] Cloud synchronization

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgements

- [.NET Framework](https://dotnet.microsoft.com/)
- [System.Text.Json](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview)
- Icons provided by [System.Drawing](https://docs.microsoft.com/en-us/dotnet/api/system.drawing)
