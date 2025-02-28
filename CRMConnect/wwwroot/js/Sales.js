document.addEventListener("DOMContentLoaded", function () {
    loadSalesActivities();
});

function loadSalesActivities() {
    fetch('http://localhost:5146/api/salesactivities')
        .then(response => response.json())
        .then(data => {
            const tableBody = document.getElementById("salesActivityTableBody");
            tableBody.innerHTML = ""; // Clear previous table content
            data.forEach(activity => {
                const row = `
                    <tr>
                        <td>${activity.salesActivityId}</td>
                        <td>${activity.customerId}</td>
                        <td>${activity.product}</td>
                        <td>$${activity.amount.toFixed(2)}</td>
                        <td>${activity.status}</td>
                        <td>${new Date(activity.date).toLocaleDateString()}</td>
                        <td>
                            <a class="btn btn-sm btn-danger" href="/SalesActivities/Delete/${activity.salesActivityId}">Delete</a>
                        </td>
                    </tr>
                `;
                tableBody.innerHTML += row;
            });
        })
        .catch(error => console.error('Error fetching sales activities:', error));
}
