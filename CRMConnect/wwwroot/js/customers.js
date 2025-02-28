document.addEventListener("DOMContentLoaded", function () {
    fetchCustomers(); // Load customers on page load
});

// Function to fetch customers and populate the table
function fetchCustomers() {
    fetch('/api/customers')
        .then(response => response.json())
        .then(data => {
            const tableBody = document.getElementById("customerTableBody");
            tableBody.innerHTML = ""; // Clear the table before populating it
            data.forEach(customer => {
                tableBody.innerHTML += `
                    <tr id="customer-${customer.id}">
                        <td>${customer.id}</td>
                        <td>${customer.name}</td>
                        <td>${customer.email}</td>
                        <td>${customer.phone}</td>
                        <td>${customer.address || "N/A"}</td>
                        <td>${customer.company}</td>
                        <td>
                            <button class="btn btn-danger btn-sm" onclick="deleteCustomer(${customer.id})">Delete</button>
                        </td>
                    </tr>
                `;
            });
        })
        .catch(error => console.error("Error fetching customers:", error));
}

// Function to open the modal to add a customer
function openCustomerModal() {
    const customerModal = new bootstrap.Modal(document.getElementById('customerModal'));
    customerModal.show();
}

// Function to handle form submission and add customer
function addCustomer() {
    const form = document.getElementById("customerForm");

    // Check if the form is valid using HTML5 form validation
    if (!form.checkValidity()) {
        form.classList.add("was-validated");
        return;
    }

    const newCustomer = {
        name: document.getElementById("name").value,
        email: document.getElementById("email").value,
        phone: document.getElementById("phone").value,
        address: document.getElementById("address").value,
        company: document.getElementById("company").value
    };

    // Send the data to the backend API
    fetch("/api/customers", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newCustomer)
    })
    .then(response => response.json())
    .then(customer => {
        // Clear the form fields
        document.getElementById("customerForm").reset();

        // Hide the modal after submission
        const customerModal = bootstrap.Modal.getInstance(document.getElementById('customerModal'));
        customerModal.hide();

        // Reload the customer list to reflect the new customer
        fetchCustomers();
    })
    .catch(error => {
        console.error("Error adding customer:", error);
        alert("Failed to add customer. Please try again.");
    });
}

// Function to delete a customer
function deleteCustomer(id) {
    if (confirm("Are you sure you want to delete this customer?")) {
        fetch(`/api/customers/${id}`, { method: "DELETE" })
        .then(() => {
            alert("Customer deleted successfully!");
            fetchCustomers(); // Refresh the list
        })
        .catch(error => {
            console.error("Error deleting customer:", error);
            alert("Failed to delete customer. Please try again.");
        });
    }
}
