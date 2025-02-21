// Fetch existing tasks and display them when the page loads
async function fetchTasks() {
    try {
        const response = await fetch('http://localhost:5146/api/customertasks');
        if (response.ok) {
            const tasks = await response.json();
            tasks.forEach(task => {
                addTaskToTable(task);
            });
        } else {
            console.error("Failed to fetch tasks");
        }
    } catch (error) {
        console.error("Error fetching tasks: ", error);
    }
}

// Add a task to the table
function addTaskToTable(task) {
    const taskList = document.getElementById("taskList");

    const row = document.createElement("tr");
    row.innerHTML = `
        <td>${task.id}</td>
        <td>${task.customerId}</td>
        <td>${task.description}</td>
        <td>${task.assignedTo}</td>
        <td>${new Date(task.dueDate).toLocaleDateString()}</td>
        <td>${task.status}</td>
        <td>
            <button class="btn btn-warning btn-sm" onclick="editTask(${task.id})">Edit</button>
            <button class="btn btn-danger btn-sm" onclick="deleteTask(${task.id})">Delete</button>
        </td>
    `;

    taskList.appendChild(row);
}

// Handle form submission to create a new task
document.getElementById("taskForm").addEventListener("submit", function (event) {
    event.preventDefault(); // Prevent the default form submission

    // Get form values
    const customerId = document.getElementById("customerId").value;
    const description = document.getElementById("description").value;
    const assignedTo = document.getElementById("assignedTo").value;
    const dueDate = document.getElementById("dueDate").value;
    const status = document.getElementById("status").value;

    // Validate form fields
    if (!customerId || !description || !assignedTo || !dueDate || !status) {
        alert("All fields are required.");
        return;
    }

    // Create a new task object
    const task = {
        customerId: parseInt(customerId),
        description: description,
        assignedTo: assignedTo,
        dueDate: new Date(dueDate),
        status: status,
    };

    // Call API to create a new task (replace with your backend API)
    createTask(task);
});

// Create a task by sending a POST request to the backend
function createTask(task) {
    // Post the task to the API
    fetch('http://localhost:5146/api/customertasks', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(task),
    })
    .then(response => response.json())
    .then(data => {
        console.log("Task created:", data);
        addTaskToTable(data); // Add the new task to the table
        document.getElementById("taskForm").reset(); // Reset the form
    })
    .catch(error => {
        console.error("Error creating task:", error);
    });
}

// Edit a task (currently just a placeholder)
function editTask(taskId) {
    alert("Edit task functionality will be implemented.");
}

// Delete a task from the table and backend
function deleteTask(taskId) {
    if (confirm("Are you sure you want to delete this task?")) {
        fetch(`http://localhost:5146/api/customertasks/${taskId}`, {
            method: 'DELETE',
        })
        .then(() => {
            const taskList = document.getElementById("taskList");
            const rows = taskList.getElementsByTagName("tr");

            for (let row of rows) {
                if (row.cells[0].innerText == taskId) {
                    taskList.deleteRow(row.rowIndex);
                    break;
                }
            }
        })
        .catch(error => {
            console.error("Error deleting task:", error);
        });
    }
}

// Call fetchTasks when the page loads
window.onload = fetchTasks;
