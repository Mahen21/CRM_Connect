// Function to fetch communication logs from the API (for initial page load)
function fetchCommunicationLogs() {
    fetch('http://localhost:5146/api/communicationlogs')  // Ensure the API URL is correct
        .then(response => response.json())  // Parse the JSON response
        .then(data => {
            console.log(data);  // Log the response to check its structure
            // Check if the data is an array
            if (Array.isArray(data)) {
                populateLogTable(data);  // Populate the table with the fetched logs
            } else {
                console.error("Expected an array but received:", data);
                alert("Error: Unable to fetch communication logs.");
            }
        })
        .catch(error => {
            console.error('Error fetching communication logs:', error);  // Handle any errors
            alert("Error fetching communication logs.");
        });
}

// Function to populate the communication log table with fetched data
function populateLogTable(logs) {
    const logList = document.getElementById("logList");  // The table body where logs will be inserted
    logList.innerHTML = "";  // Clear any existing logs in the table

    // Loop through each log and create a new row for each entry
    logs.forEach(log => {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${log.id}</td>
            <td>${log.customerId}</td>
            <td>${log.userId}</td>
            <td>${log.communicationType}</td>
            <td>${log.notes || "N/A"}</td>
            <td>${new Date(log.date).toLocaleString()}</td>
            <td>
                <a href="/Communication/Edit/${log.id}" class="btn btn-warning btn-sm">Edit</a>
                <a href="/Communication/Delete/${log.id}" class="btn btn-danger btn-sm">Delete</a>
            </td>
        `;

        // Append the newly created row to the table
        logList.appendChild(row);
    });
}

// Event listener for form submission to create a new communication log
document.getElementById("communicationForm")?.addEventListener("submit", function (event) {
    event.preventDefault();  // Prevent the form from submitting the traditional way

    // Get the form values
    const customerId = document.getElementById("customerId").value;
    const userId = document.getElementById("userId").value;
    const communicationType = document.getElementById("communicationType").value;
    const notes = document.getElementById("notes").value;
    const date = document.getElementById("date").value;

    // Validate the form fields (make sure required fields are filled out)
    if (!customerId || !userId || !communicationType || !date) {
        alert("All fields are required.");
        return;
    }

    // Create a new communication log object
    const communicationLog = {
        customerId: parseInt(customerId),
        userId: parseInt(userId),
        communicationType: communicationType,
        notes: notes,
        date: date,
    };

    // Send the new log data to the API via a POST request
    createCommunicationLog(communicationLog);
});

// Function to create a new communication log via the API
function createCommunicationLog(log) {
    fetch('http://localhost:5146/api/communicationlogs', {
        method: 'POST',  // POST method to create a new log
        headers: {
            'Content-Type': 'application/json',  // Sending data as JSON
        },
        body: JSON.stringify(log),  // Convert the log object to a JSON string
    })
    .then(response => response.json())  // Parse the response as JSON
    .then(data => {
        console.log(data); // Log the response to check its structure
        // Check if the response is an object (successful log creation response)
        if (data && data.id) {
            alert("Log created successfully!");
            window.location.href = "/Communication";  // Redirect to the communication list page
        } else {
            alert("Error creating communication log.");
        }
    })
    .catch(error => {
        console.error('Error creating communication log:', error);  // Handle any errors
        alert("Error creating communication log.");
    });
}

// Initial function to fetch and display the communication logs when the page is loaded
window.onload = function () {
    fetchCommunicationLogs();  // Fetch and display the logs when the page loads
};
