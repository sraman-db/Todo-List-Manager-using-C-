using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using System.Drawing;

namespace TodoListManager
{
    public partial class MainForm : Form
    {
        // Data structures
        private List<TodoItem> todos = new List<TodoItem>();
        private string currentUsername = "";
        private string dataFilePath = "todos.json";
        private string userFilePath = "users.json";
        private Dictionary<string, string> users = new Dictionary<string, string>();

        // UI Controls
        private Panel loginPanel;
        private Panel todoPanel;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Label welcomeLabel;
        private Button logoutButton;
        private TextBox todoTitleTextBox;
        private TextBox todoDescriptionTextBox;
        private ComboBox priorityComboBox;
        private Button addTodoButton;
        private FlowLayoutPanel todosFlowLayoutPanel;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            LoadUsers();
        }

        private void InitializeComponent()
        {
            this.Text = "Todo List Manager";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void InitializeCustomComponents()
        {
            // Create login panel
            loginPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = true
            };

            Label loginTitleLabel = new Label
            {
                Text = "Todo List Manager",
                Font = new Font("Arial", 20, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50
            };

            Label usernameLabel = new Label
            {
                Text = "Username:",
                Location = new Point(250, 150),
                Size = new Size(100, 20)
            };

            usernameTextBox = new TextBox
            {
                Location = new Point(350, 150),
                Size = new Size(200, 20)
            };

            Label passwordLabel = new Label
            {
                Text = "Password:",
                Location = new Point(250, 180),
                Size = new Size(100, 20)
            };

            passwordTextBox = new TextBox
            {
                Location = new Point(350, 180),
                Size = new Size(200, 20),
                PasswordChar = '*'
            };

            loginButton = new Button
            {
                Text = "Login",
                Location = new Point(350, 220),
                Size = new Size(100, 30)
            };
            loginButton.Click += LoginButton_Click;

            Label noteLabel = new Label
            {
                Text = "Note: If this is your first time, a new account will be created.",
                ForeColor = Color.Gray,
                Location = new Point(250, 260),
                Size = new Size(300, 20)
            };

            loginPanel.Controls.Add(loginTitleLabel);
            loginPanel.Controls.Add(usernameLabel);
            loginPanel.Controls.Add(usernameTextBox);
            loginPanel.Controls.Add(passwordLabel);
            loginPanel.Controls.Add(passwordTextBox);
            loginPanel.Controls.Add(loginButton);
            loginPanel.Controls.Add(noteLabel);

            // Create todo panel
            todoPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false
            };

            // Header panel with welcome and logout
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.WhiteSmoke
            };

            welcomeLabel = new Label
            {
                Text = "Welcome!",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };

            logoutButton = new Button
            {
                Text = "Logout",
                Location = new Point(700, 10),
                Size = new Size(80, 30)
            };
            logoutButton.Click += LogoutButton_Click;

            headerPanel.Controls.Add(welcomeLabel);
            headerPanel.Controls.Add(logoutButton);

            // Add task panel
            Panel addTaskPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            Label addTaskLabel = new Label
            {
                Text = "Add New Task",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            Label titleLabel = new Label
            {
                Text = "Title:",
                Location = new Point(10, 40),
                Size = new Size(50, 20)
            };

            todoTitleTextBox = new TextBox
            {
                Location = new Point(70, 40),
                Size = new Size(700, 20)
            };

            Label descriptionLabel = new Label
            {
                Text = "Description:",
                Location = new Point(10, 70),
                Size = new Size(70, 20)
            };

            todoDescriptionTextBox = new TextBox
            {
                Location = new Point(90, 70),
                Size = new Size(680, 20)
            };

            Label priorityLabel = new Label
            {
                Text = "Priority:",
                Location = new Point(10, 100),
                Size = new Size(60, 20)
            };

            priorityComboBox = new ComboBox
            {
                Location = new Point(70, 100),
                Size = new Size(100, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            priorityComboBox.Items.AddRange(new object[] { "Low", "Medium", "High" });
            priorityComboBox.SelectedIndex = 1; // Default to Medium

            addTodoButton = new Button
            {
                Text = "Add Task",
                Location = new Point(680, 100),
                Size = new Size(90, 30)
            };
            addTodoButton.Click += AddTodoButton_Click;

            addTaskPanel.Controls.Add(addTaskLabel);
            addTaskPanel.Controls.Add(titleLabel);
            addTaskPanel.Controls.Add(todoTitleTextBox);
            addTaskPanel.Controls.Add(descriptionLabel);
            addTaskPanel.Controls.Add(todoDescriptionTextBox);
            addTaskPanel.Controls.Add(priorityLabel);
            addTaskPanel.Controls.Add(priorityComboBox);
            addTaskPanel.Controls.Add(addTodoButton);

            // Tasks list panel
            Panel tasksPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(10)
            };

            Label tasksLabel = new Label
            {
                Text = "Your Tasks",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            todosFlowLayoutPanel = new FlowLayoutPanel
            {
                Location = new Point(10, 40),
                Size = new Size(760, 340),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BorderStyle = BorderStyle.FixedSingle
            };

            tasksPanel.Controls.Add(tasksLabel);
            tasksPanel.Controls.Add(todosFlowLayoutPanel);

            todoPanel.Controls.Add(tasksPanel);
            todoPanel.Controls.Add(addTaskPanel);
            todoPanel.Controls.Add(headerPanel);

            // Add panels to form
            this.Controls.Add(todoPanel);
            this.Controls.Add(loginPanel);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if user exists, if not create new user
            if (!users.ContainsKey(username))
            {
                users.Add(username, password);
                SaveUsers();
            }
            else if (users[username] != password)
            {
                MessageBox.Show("Incorrect password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            currentUsername = username;
            welcomeLabel.Text = $"Welcome, {currentUsername}!";
            
            // Load user's todos
            LoadTodos();
            
            // Switch to todo panel
            loginPanel.Visible = false;
            todoPanel.Visible = true;
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            currentUsername = "";
            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
            
            // Switch to login panel
            todoPanel.Visible = false;
            loginPanel.Visible = true;
        }

        private void AddTodoButton_Click(object sender, EventArgs e)
        {
            string title = todoTitleTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a task title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string description = todoDescriptionTextBox.Text.Trim();
            string priority = priorityComboBox.SelectedItem.ToString();

            TodoItem newTodo = new TodoItem
            {
                Id = Guid.NewGuid().ToString(),
                Title = title,
                Description = description,
                Priority = priority,
                Completed = false,
                Pinned = false,
                CreatedAt = DateTime.Now
            };

            todos.Add(newTodo);
            SaveTodos();
            RefreshTodosList();

            // Clear input fields
            todoTitleTextBox.Text = "";
            todoDescriptionTextBox.Text = "";
            priorityComboBox.SelectedIndex = 1; // Reset to Medium
        }

        private void LoadUsers()
        {
            if (File.Exists(userFilePath))
            {
                try
                {
                    string json = File.ReadAllText(userFilePath);
                    users = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    users = new Dictionary<string, string>();
                }
            }
        }

        private void SaveUsers()
        {
            try
            {
                string json = JsonSerializer.Serialize(users);
                File.WriteAllText(userFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTodos()
        {
            todos.Clear();
            string userTodoFilePath = $"{currentUsername}_{dataFilePath}";
            
            if (File.Exists(userTodoFilePath))
            {
                try
                {
                    string json = File.ReadAllText(userTodoFilePath);
                    todos = JsonSerializer.Deserialize<List<TodoItem>>(json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading todo data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    todos = new List<TodoItem>();
                }
            }
            
            RefreshTodosList();
        }

        private void SaveTodos()
        {
            try
            {
                string userTodoFilePath = $"{currentUsername}_{dataFilePath}";
                string json = JsonSerializer.Serialize(todos);
                File.WriteAllText(userTodoFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving todo data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshTodosList()
        {
            todosFlowLayoutPanel.Controls.Clear();

            // Sort todos: pinned first, then by completion status and creation date
            var sortedTodos = todos
                .OrderByDescending(t => t.Pinned)
                .ThenBy(t => t.Completed)
                .ThenByDescending(t => t.CreatedAt)
                .ToList();

            if (sortedTodos.Count == 0)
            {
                Label noTasksLabel = new Label
                {
                    Text = "No tasks yet. Add a task to get started!",
                    AutoSize = true,
                    ForeColor = Color.Gray,
                    Margin = new Padding(10)
                };
                todosFlowLayoutPanel.Controls.Add(noTasksLabel);
            }
            else
            {
                foreach (var todo in sortedTodos)
                {
                    Panel todoPanel = CreateTodoPanel(todo);
                    todosFlowLayoutPanel.Controls.Add(todoPanel);
                }
            }
        }

        private Panel CreateTodoPanel(TodoItem todo)
        {
            // Create task panel with border
            Panel panel = new Panel
            {
                Width = 740,
                Height = 80,
                Margin = new Padding(0, 0, 0, 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Set background color based on priority
            switch (todo.Priority.ToLower())
            {
                case "high":
                    panel.BackColor = Color.FromArgb(254, 226, 226); // Light red
                    break;
                case "medium":
                    panel.BackColor = Color.FromArgb(254, 249, 195); // Light yellow
                    break;
                case "low":
                    panel.BackColor = Color.FromArgb(220, 252, 231); // Light green
                    break;
                default:
                    panel.BackColor = Color.FromArgb(241, 245, 249); // Light gray
                    break;
            }

            // Checkbox for completion status
            CheckBox completedCheckBox = new CheckBox
            {
                Appearance = Appearance.Button,
                Image = todo.Completed ? Properties.Resources.checked_checkbox : Properties.Resources.unchecked_checkbox,
                Size = new Size(24, 24),
                Location = new Point(10, 10),
                FlatStyle = FlatStyle.Flat
            };
            completedCheckBox.FlatAppearance.BorderSize = 0;
            completedCheckBox.FlatAppearance.CheckedBackColor = Color.Transparent;
            completedCheckBox.Checked = todo.Completed;
            completedCheckBox.Tag = todo.Id;
            completedCheckBox.Click += CompletedCheckBox_Click;

            // Task title
            Label titleLabel = new Label
            {
                Text = todo.Title,
                Location = new Point(45, 10),
                Size = new Size(580, 20),
                Font = new Font(this.Font, todo.Completed ? FontStyle.Strikeout : FontStyle.Regular)
            };

            // Task description
            Label descriptionLabel = new Label
            {
                Text = todo.Description,
                Location = new Point(45, 35),
                Size = new Size(580, 35),
                ForeColor = Color.Gray
            };

            // Pin button
            Button pinButton = new Button
            {
                Image = todo.Pinned ? Properties.Resources.star_filled : Properties.Resources.star_outline,
                Size = new Size(32, 32),
                Location = new Point(655, 10),
                FlatStyle = FlatStyle.Flat,
                Tag = todo.Id
            };
            pinButton.FlatAppearance.BorderSize = 0;
            pinButton.Click += PinButton_Click;

            // Delete button
            Button deleteButton = new Button
            {
                Image = Properties.Resources.trash,
                Size = new Size(32, 32),
                Location = new Point(695, 10),
                FlatStyle = FlatStyle.Flat,
                Tag = todo.Id
            };
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.Click += DeleteButton_Click;

            panel.Controls.Add(completedCheckBox);
            panel.Controls.Add(titleLabel);
            panel.Controls.Add(descriptionLabel);
            panel.Controls.Add(pinButton);
            panel.Controls.Add(deleteButton);

            return panel;
        }

        private void CompletedCheckBox_Click(object sender, EventArgs e)
        {
            string todoId = (string)((CheckBox)sender).Tag;
            TodoItem todo = todos.FirstOrDefault(t => t.Id == todoId);
            
            if (todo != null)
            {
                todo.Completed = !todo.Completed;
                SaveTodos();
                RefreshTodosList();
            }
        }

        private void PinButton_Click(object sender, EventArgs e)
        {
            string todoId = (string)((Button)sender).Tag;
            TodoItem todo = todos.FirstOrDefault(t => t.Id == todoId);
            
            if (todo != null)
            {
                todo.Pinned = !todo.Pinned;
                SaveTodos();
                RefreshTodosList();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            string todoId = (string)((Button)sender).Tag;
            TodoItem todo = todos.FirstOrDefault(t => t.Id == todoId);
            
            if (todo != null)
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this task?", 
                    "Confirm Delete", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    todos.Remove(todo);
                    SaveTodos();
                    RefreshTodosList();
                }
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    // Todo data model
    public class TodoItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public bool Completed { get; set; }
        public bool Pinned { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}