document.addEventListener("DOMContentLoaded", function () {
    loadSalesActivities();
});

// Function to load sales activities from the API
function loadSalesActivities() {
    fetch('http://localhost:5146/api/salesactivities')
        .then(response => {
            if (!response.ok) {
                throw new Error("Network response was not ok");
            }
            return response.json();
        })
        .then(data => {
            console.log("Sales Activities:", data); // Log the data to the console for debugging
            const tableBody = document.getElementById("salesActivityTableBody");
            tableBody.innerHTML = ""; // Clear table body before loading new data

            // Loop through the data and create rows for each sales activity
            data.forEach(activity => {
                // Handle the case where `customer` is null
                const customer = activity.customer ? activity.customer.name : "Unknown"; // Modify this based on actual customer field

                const row = `<tr>
                    <td>${activity.salesActivityId}</td>
                    <td>${customer}</td> <!-- Display customer name or 'Unknown' if null -->
                    <td>${activity.product}</td>
                    <td>$${activity.amount.toFixed(2)}</td>
                    <td>${activity.status}</td>
                    <td>${new Date(activity.date).toLocaleDateString()}</td> <!-- Format the date -->
                    <td>
                        <button class="btn btn-sm btn-warning" onclick="editSalesActivity(${activity.salesActivityId})">Edit</button>
                        <button class="btn btn-sm btn-danger" onclick="deleteSalesActivity(${activity.salesActivityId})">Delete</button>
                    </td>
                </tr>`;
                tableBody.innerHTML += row;
            });
        })
        .catch(error => {
            console.error('Error fetching sales activities:', error);
        });
}

// Function to open the Add Sales Activity modal
function openSalesActivityModal() {
    document.getElementById("salesActivityForm").reset();
    document.getElementById("salesActivityModal").classList.add("show");
    document.getElementById("salesActivityModal").style.display = "block";
}

// Function to add a new sales activity
function addSalesActivity() {
    const customerId = document.getElementById("customerId").value;
    const product = document.getElementById("product").value;
    const amount = document.getElementById("amount").value;
    const status = document.getElementById("status").value;
    const activityDate = document.getElementById("activityDate").value;

    // Validate form fields
    if (!customerId || !product || !amount || !status || !activityDate) {
        alert("All fields are required!");
        return;
    }

    // Create a new sales activity object
    const newActivity = {
        salesActivityId: Math.floor(Math.random() * 1000), // Simulated ID
        customerId: parseInt(customerId),
        product: product,
        amount: parseFloat(amount),
        status: status,
        date: activityDate
    };

    // Log the new activity for debugging
    console.log("New Sales Activity:", newActivity);

    // Close modal
    document.getElementById("salesActivityModal").classList.remove("show");
    document.getElementById("salesActivityModal").style.display = "none";

    // Reload table with new data (Simulated for now)
    loadSalesActivities();
}

// Function to edit an existing sales activity (Placeholder)
function editSalesActivity(id) {
    alert(`Edit Sales Activity ID: ${id} (Feature not implemented yet)`);
}

// Function to delete a sales activity (Placeholder)
function deleteSalesActivity(id) {
    const confirmDelete = confirm("Are you sure you want to delete this sales activity?");
    if (confirmDelete) {
        alert(`Deleted Sales Activity ID: ${id} (Feature not implemented yet)`);
        loadSalesActivities(); // Reload table (Replace with actual delete API call)
    }
}
